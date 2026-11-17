# Order Times, Time Zones, and Daylight-Saving Transitions

[Open the interactive time-zone lab](order-time-zones-lab.html)

An order's time zone identifies a set of rules. Its UTC offset and display abbreviation depend on the particular date and time. Users in different zones can see different clock times and calendar dates for the same instant.

The examples use New York in 2026. This is a study note based on the PR discussion and the official sources below, not a review of the application's current implementation.

## The concepts and how they connect

| Concept | Meaning | Example |
| --- | --- | --- |
| Local date and time | What a clock and calendar show; incomplete without context | `2026-11-01 01:30` |
| Time zone | An identifier for regional time rules, including historical changes | `America/New_York` |
| UTC offset | The difference between local time and UTC at a particular instant | `−04:00` or `−05:00` |
| Instant | One unambiguous point on the timeline | `2026-11-01T06:30:00Z` |
| Abbreviation | A presentation label for a zone's time at that instant | `EDT` or `EST` |
| Generic zone label | A label that does not select standard or daylight time | `Eastern Time` / `ET` |

```text
Local date/time + schedule zone rules
               |
               v
      Resolve gap or overlap
               |
               v
          UTC instant
               |
               +-- schedule zone --> job-local date/time + offset + label
               |
               +-- viewer zone ----> user-local date/time + offset + label
```

Going from an instant to a zone's local time is unambiguous. Going from a local time to an instant can yield zero, one, or two answers around a daylight-saving transition.

An offset is not a time-zone identity: several zones can share today's offset but follow different rules later. Abbreviations are also unsuitable as storage keys because they are ambiguous across regions and can vary by locale or formatter.

## Same shift, different abbreviation

Consider an overnight job on November 1, 2026:

| Field | New York schedule time | UTC instant | Los Angeles viewer |
| --- | --- | --- | --- |
| Order start | Nov 1, 00:30 EDT (`−04:00`) | Nov 1, 04:30 UTC | Oct 31, 21:30 PDT |
| Anticipated start | Nov 1, 02:30 EST (`−05:00`) | Nov 1, 07:30 UTC | Nov 1, 00:30 PDT |

Both fields use `America/New_York`. They can belong to the same overnight shift, but the clock moves back between them. Their local clock readings differ by two hours while their instants differ by three hours.

The Los Angeles viewer also demonstrates why the date must travel with the time: the order start appears on the previous calendar day there.

### What this means for the PR discussion

The comment is correct that both fields use the schedule's zone. That does not make an abbreviation calculated from `displayedStartOn` correct for every value of `AnticipatedStartDateAndTime`.

The dependency should be:

```text
displayedStartOn + schedule zone
    --> order-start abbreviation

AnticipatedStartDateAndTime + schedule zone + resolved occurrence
    --> anticipated-start abbreviation
```

Recalculate the anticipated label when its value changes, when the schedule zone changes, and when the selected occurrence of a repeated time changes. A reactive property, computed binding, or converter with all relevant inputs can implement this. A second stored mutable value is not inherently required.

If the UI only needs to identify the zone, a generic label such as `Eastern Time` avoids promising a date-specific `EDT` or `EST` value. An offset is still useful when distinguishing repeated times.

Reusing the date-specific abbreviation is safe only when the allowed values are guaranteed to require the same label. “Same shift” alone does not enforce that guarantee. The frequency of the edge case is a prioritization decision, separate from whether the label is correct.

A wrong label proves a presentation defect. It does not, by itself, prove that the stored instant or conversion is wrong; those paths need separate inspection.

## The two daylight-saving input traps

### Spring: a local time that does not exist

In New York on March 8, 2026, the clock jumps from `01:59 EST` to `03:00 EDT` at minute precision. Local times from `02:00` through `02:59` do not occur.

`2026-03-08 02:30` therefore has no corresponding instant in that zone. Reject it with a useful validation message, or apply an explicitly defined adjustment policy and show the adjusted value. Do not silently let an incidental library default decide the business behavior.

### Autumn: a local time that occurs twice

On November 1, 2026, the clock moves from `01:59 EDT` back to `01:00 EST` at minute precision.

| Entered local time | Occurrence | Corresponding instant |
| --- | --- | --- |
| Nov 1, 01:30 | First, EDT (`−04:00`) | `2026-11-01T05:30:00Z` |
| Nov 1, 01:30 | Second, EST (`−05:00`) | `2026-11-01T06:30:00Z` |

Ask for an occurrence, or enforce a documented domain rule. Until resolved, a wall-clock value and zone alone cannot identify the intended instant or a single correct abbreviation.

These examples follow the [NIST 2026 U.S. transition rules](https://www.nist.gov/pml/time-and-frequency-division/popular-links/daylight-saving-time-dst). Other regions may use different dates or no seasonal clock changes. Never assume the difference between two zones remains constant throughout the year.

## An order-time development contract

1. **Define the input zone.** A schedule-local editor interprets the entered value in the schedule's zone, even when the user is elsewhere. A viewer-local editor needs an equally explicit contract.
2. **Resolve local input.** Apply that zone's rules for the selected date. Handle missing and repeated times before creating the instant.
3. **Persist the meaning.** Store a resolved instant and the schedule zone ID. For future commitments anchored to a local clock, also preserve the intended local date/time and decide what happens if zone rules change.
4. **Transmit unambiguous timestamps.** Use an ISO 8601 value with `Z` or an explicit offset, plus a zone ID where scheduling requires it. Avoid interpreting an offset-free string using whichever zone the server or browser happens to use.
5. **Convert for display.** Use the same instant with the chosen display zone. Changing the viewer's preference should change presentation, not reschedule the order.
6. **Refresh dependent labels.** Derive the date, time, offset, and abbreviation together from the field's resolved value and display zone.

### Schedule view versus personal view

| View | Display zone | Intended use |
| --- | --- | --- |
| Schedule view | Order's schedule zone | Coordinate work at the job location |
| Personal view | User's selected zone | Understand when the job occurs relative to the user |
| Audit or integration view | UTC, or explicit offset | Compare and exchange instants across systems |

Label the active view. Two users can see different local values without disagreeing about when the order starts.

## Practical .NET and database choices

| Value | Suitable representation | Important limit |
| --- | --- | --- |
| Unresolved schedule-local input | `DateTime` with `Kind.Unspecified`, plus explicit zone ID | Must resolve gaps and overlaps |
| Resolved instant | `DateTimeOffset`, or a UTC `DateTime` with a consistent contract | An offset alone does not retain regional rules |
| Zone rules | `TimeZoneInfo` and an explicitly selected ID | Keep installed time-zone data current |
| Date-only business value | `DateOnly` where available, or SQL `date` | A calendar date is not automatically a UTC timestamp |
| SQL instant | `datetimeoffset`, or `datetime2` under an enforced UTC convention | Neither type stores a regional zone ID |

For local input, `TimeZoneInfo.IsInvalidTime` identifies a gap; `IsAmbiguousTime` identifies an overlap. Resolve those cases deliberately before conversion. Do not treat `DateTimeKind.Local` as “the schedule's zone”: it refers to the executing environment's local zone.

The lab uses IANA IDs such as `America/New_York`. Windows deployments may use IDs such as `Eastern Standard Time`; that Windows ID names a zone with seasonal rules, despite “Standard” in its name. Choose an identifier contract compatible with the deployed platform and map IDs explicitly when needed.

Microsoft documents the distinctions between these types in [Choose between .NET date and time types](https://learn.microsoft.com/en-us/dotnet/standard/datetime/choosing-between-datetime) and the [TimeZoneInfo API](https://learn.microsoft.com/en-us/dotnet/api/system.timezoneinfo).

## Durations, date filters, and recurring work

- **Elapsed duration:** subtract resolved instants. Local clock subtraction can produce a different number across a transition. Payroll or billing rules may define another measure; make that rule explicit.
- **Orders on a date:** first choose whose calendar date matters. Resolve the start of that local date and the start of the next local date, then filter instants using the half-open interval `[start, nextStart)`. Apply the zone's rules independently to both boundaries. A New York transition day can span 23 or 25 elapsed hours.
- **Recurring 09:00 shift:** generate each local occurrence using the zone's rules. Adding 24 elapsed hours is not always equivalent to “09:00 tomorrow.”
- **Future rule changes:** decide whether a booking preserves its instant or its intended local clock time. Store enough information to apply that policy and keep conversion data current.

## Focused regression scenarios

| Scenario | Expected result |
| --- | --- |
| Ordinary summer day | Matching labels; usual clock and elapsed differences |
| Fall transition: 00:30 to 02:30 | EDT to EST; two clock hours, three elapsed hours |
| Spring transition: 01:30 to 03:30 | EST to EDT; two clock hours, one elapsed hour |
| Spring input at 02:30 | Rejected or adjusted under the explicit policy |
| Fall input at 01:30 | Both possible instants handled deliberately |
| Anticipated value edited across a transition | Its label and conversion refresh |
| Schedule zone changed | Validation, conversion, and dependent labels refresh |
| Viewer zone changed | Display changes; stored instant remains unchanged |
| Viewer on the previous date | Date and time displayed consistently |
| Local-day filter on a transition date | Correct bounds, without a fixed 24-hour assumption |

## Recall questions

1. Why can two fields in the same zone require different abbreviations?
2. What information is missing from `2026-11-01 01:30` in New York?
3. Why does storing an offset not replace storing a zone ID?
4. Does a wrong EDT/EST label prove the database value is wrong?
5. Why should a local-day query resolve both boundaries separately?

### Answers

1. Zone rules select the offset and label for each field's date and instant; a transition can occur between them.
2. Which occurrence is intended: the earlier EDT occurrence or the later EST occurrence.
3. An offset describes a particular relationship to UTC; a zone identifies rules across dates.
4. No. Display logic and instant conversion/storage must be assessed separately.
5. The offset can change within the day, so its elapsed length need not be 24 hours.

## Sources and lab scope

- [NIST: Daylight Saving Time Rules](https://www.nist.gov/pml/time-and-frequency-division/popular-links/daylight-saving-time-dst) — U.S. rules and 2026 transition dates, checked October 29, 2026.
- [Microsoft: Choose between date and time types](https://learn.microsoft.com/en-us/dotnet/standard/datetime/choosing-between-datetime) — instants, offsets, dates, and zone context.
- [Microsoft: TimeZoneInfo.IsAmbiguousTime](https://learn.microsoft.com/en-us/dotnet/api/system.timezoneinfo.isambiguoustime) — detecting repeated local times.
- [Microsoft: TimeZoneInfo.IsInvalidTime](https://learn.microsoft.com/en-us/dotnet/api/system.timezoneinfo.isinvalidtime) — detecting skipped local times.
- [Microsoft: SQL Server datetimeoffset](https://learn.microsoft.com/en-us/sql/t-sql/data-types/datetimeoffset-transact-sql) — storage of dates, times, and explicit offsets.

The HTML lab mirrors the conversation canvas. It limits editable schedule input to New York in 2026 and explicitly considers that example's two possible offsets. Viewer conversions use the browser's time-zone data. This bounded teaching implementation is not a general production time-zone resolver. Zone abbreviation formatting can differ between runtimes.
