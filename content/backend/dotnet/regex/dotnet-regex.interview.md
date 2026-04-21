---
title: .NET Regex Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to rehearse the .NET regex ideas that matter in practice:

- picking the right `Regex` API
- grouping and named captures
- lookarounds and backreferences
- performance and safety basics

## Foundations

```interview-question
What is the difference between `Regex.IsMatch`, `Regex.Match`, and `Regex.Matches`?
---
answer:
`Regex.IsMatch` answers whether a match exists.

`Regex.Match` returns the first match and its metadata.

`Regex.Matches` returns all matches as a collection.
hints:
- One is boolean only.
- One returns the first occurrence.
- One returns every occurrence.
```

```interview-question
Why are verbatim string literals commonly used with regex patterns in C#?
---
answer:
Verbatim string literals such as `@"\d+"` reduce escaping noise because backslashes are treated literally.

That makes regex patterns easier to read and maintain in C# source code.
hints:
- Backslashes matter in both regex and normal C# strings.
- Verbatim strings reduce doubled escaping.
- Readability is the main reason.
```

```interview-question
What is the difference between a named capturing group and a non-capturing group?
---
answer:
A named capturing group stores matched text and lets you retrieve it by name through `match.Groups["Name"]`.

A non-capturing group groups the pattern for precedence or repetition but does not store a capture.
hints:
- One is used for extraction.
- The other is used only for grouping structure.
- `(?<Name>...)` and `(?:...)` are the key syntaxes.
```

```interview-question
Why can catastrophic backtracking be a problem in regex code?
---
answer:
Poorly designed patterns can trigger excessive backtracking, causing regex evaluation to become extremely slow on certain inputs.

This is both a performance and security concern, which is why timeouts and careful pattern design matter.
hints:
- Think worst-case input, not average-case input.
- It can look like the program is hanging.
- Timeouts are one mitigation.
```

## Multiple Choice Questions

```interview-choice
Which syntax defines a named capturing group in .NET regex?
---
options:
- `(?:Name:pattern)`
- `(?<Name>pattern)`
- `(Name=pattern)`
correct: 1
explanation:
`(?<Name>pattern)` is the .NET syntax for a named capturing group.
```

```interview-choice
Which `Group` member gives you every capture when the same capturing group matches multiple times?
---
options:
- `Value`
- `Groups`
- `Captures`
correct: 2
explanation:
`Group.Captures` contains every capture made by that group, while `Value` returns only the final captured value.
```

```interview-choice
Which option commonly improves regex performance when the same fixed pattern is reused many times?
---
options:
- `RegexOptions.Multiline`
- `RegexOptions.Compiled`
- `RegexOptions.Singleline`
correct: 1
explanation:
`RegexOptions.Compiled` can reduce repeated execution overhead for a regex that is reused frequently.
```

## Code Completion Questions

```interview-code
language: cs
prompt: Complete the phone-number pattern so it captures the area code, prefix, and line number as named groups.
starter:
string pattern = @"^(?<AreaCode>\d{3})-(?<Prefix>\d{3})-";
solution:
string pattern = @"^(?<AreaCode>\d{3})-(?<Prefix>\d{3})-(?<LineNumber>\d{4})$";
checks:
- includes: AreaCode
- includes: Prefix
- includes: LineNumber
- includes: \d{4}
```

```interview-code
language: cs
prompt: Complete the code so it checks whether the input matches the regex pattern.
starter:
var regex = new Regex(pattern);
bool isValid = 
solution:
var regex = new Regex(pattern);
bool isValid = regex.IsMatch(input);
checks:
- includes: IsMatch
- includes: input
```

```interview-code
language: cs
prompt: Complete the code so it reads the captured first name from a named group.
starter:
Match match = regex.Match(input);
string firstName = 
solution:
Match match = regex.Match(input);
string firstName = match.Groups["FirstName"].Value;
checks:
- includes: Groups
- includes: FirstName
- includes: Value
```

## Study Notes

Use the longer notes in this section for explanations and examples:

- [Regex Basics](base.md)
- [Groups and Capturing](groups-capturing.md)
