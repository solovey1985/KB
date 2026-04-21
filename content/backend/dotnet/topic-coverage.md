# .NET Topic Coverage and Authoring Guide

This document explains how the `content/backend/dotnet` study pages are organized and how to add new topic coverage consistently.

## Current Coverage

Implemented topic folders:

- `api-design-rest`
- `aspnet-core-internals-middleware`
- `ef-core-data-access`
- `authentication-security`
- `performance-caching`
- `architecture-system-design`
- `testing`
- `production-readiness`
- `modern-dotnet-csharp-features`

There are no remaining planned folders from the current Web API interview source set.

## Folder Structure

Each interview-driven topic should follow this structure:

```text
topic-name/
  index.md
  junior.interview.md        # only when the source topic has junior questions
  middle.interview.md        # or mid.interview.md if the team standard changes later
  senior.interview.md        # only when the source topic has senior questions
  topic-part-one.concept.md
  topic-part-two.concept.md  # only when concept cards need splitting
```

## What Goes In `index.md`

Each topic index should contain:

1. A short topic description.
2. Links to interview pages by level.
3. Links to concept maps.
4. A `Study Flow` section.
5. A `Related Topics` section.
6. A concise `Topic Coverage` list.

## Interview Page Rules

Interview pages should:

1. Split questions by level based on the source material.
2. Keep questions grouped by subtopic sections.
3. Link to relevant concept maps near the top.
4. Add short `Related concepts:` lines after important questions when those links improve study flow.
5. Prefer concise, interview-focused answers rather than copying the full PDF prose.

Recommended block mix:

- `interview-question` for open recall
- `interview-choice` for distinctions and trap answers
- `interview-code` for small implementation recall

## Concept Page Rules

Concept pages should:

1. Stay under roughly 15 cards per page.
2. Move from general to specific.
3. Use explicit `id:` values for cross-linking.
4. Add `mnemonic:` lines for every card.
5. Add `example:` values whenever the concept is easier to remember through a concrete API, code shape, or command.
6. Prefer compact inline code examples inside `example:`.
7. Use `related:` when a concept is commonly confused with or paired with another concept.

## Quality Checklist

Before considering a topic complete, review these points:

### Examples

- Does each important or abstract concept have a concrete example?
- Are examples short enough to stay readable inside `concept-card` fields?
- Do examples reflect realistic .NET Web API usage instead of toy placeholders?

### Mnemonics

- Does each card have a short mnemonic?
- Is the mnemonic memorable rather than just a reworded definition?
- Does it reinforce the distinction that matters in interviews?

### Links

- Does the topic index link to all study pages in the folder?
- Do interview pages link to their concept maps?
- Do concept maps connect related cards with `parents`, `children`, and `related`?
- Does the topic index link to adjacent topics that naturally follow it?

## Suggested Topic Sequence

Use this order when building or studying new material:

1. API Design and REST
2. ASP.NET Core Internals and Middleware
3. EF Core and Data Access
4. Authentication and Security
5. Performance and Caching
6. Architecture and System Design
7. Testing
8. Production Readiness
9. Modern .NET and C# Features

## Source Conversion Strategy

When the source is a large PDF topic:

1. Split the topic into one folder.
2. Split interview questions by level.
3. Extract recurring concepts from the answers.
4. Group those concepts into one or more concept pages with no more than 15 cards each.
5. Add cross-links from interview sections to the most relevant concept cards.
6. Add study-flow links in the topic `index.md` so readers know what to study next.

## Related Index

- [.NET Backend Study Index](index.md)
