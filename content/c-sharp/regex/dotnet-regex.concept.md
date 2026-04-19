---
title: .NET Regex Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes the regex section around the .NET API surface, pattern building blocks, and the most important capture concepts.

## Core Map

```concept-card
id: dotnet-regex
term: .NET Regex
aliases:
- System.Text.RegularExpressions
children:
- regex-class
- character-class
- capturing-group
- named-group
- lookaround
- regex-options
- regex-timeout
summary:
.NET regex is the pattern-matching engine and API surface centered on the `Regex` class and related match types.
details:
It supports searching, matching, replacing, splitting, grouping, lookarounds, backreferences, and a set of performance and safety options.
recall:
- Which core class anchors regex usage in .NET?
- Which ideas matter most for matching, extraction, and performance?
```

```concept-card
id: regex-class
term: Regex Class
parents:
- dotnet-regex
children:
- regex-match
related:
- regex-options
summary:
The `Regex` class is the main .NET type for matching, searching, replacing, splitting, and validating text with patterns.
details:
Its most common entry points are `IsMatch`, `Match`, `Matches`, `Replace`, and `Split`.
recall:
- Which `Regex` methods answer yes or no, return one match, and return all matches?
```

```concept-card
id: regex-match
term: Match
parents:
- regex-class
related:
- capturing-group
summary:
A `Match` object contains the text and metadata for one regex match result.
details:
It also exposes the `Groups` collection, which is how captures are retrieved after a successful match.
recall:
- Which object exposes the matched groups after `Regex.Match` succeeds?
```

```concept-card
id: character-class
term: Character Class
parents:
- dotnet-regex
related:
- capturing-group
summary:
Character classes define sets of characters that can match at a given position, such as `[abc]`, `\d`, `\w`, or Unicode categories.
details:
They are the basic building block for many patterns because they constrain what characters can appear without spelling out each full alternative.
recall:
- What problem do character classes solve in pattern design?
```

```concept-card
id: capturing-group
term: Capturing Group
parents:
- dotnet-regex
children:
- captures-collection
related:
- named-group
summary:
A capturing group isolates part of a regex match so it can be retrieved later from the match result.
details:
Groups are defined with parentheses and are used both for extraction and for advanced features such as backreferences.
recall:
- Why are capturing groups useful beyond simple whole-string matching?
```

```concept-card
id: named-group
term: Named Group
parents:
- dotnet-regex
related:
- capturing-group
summary:
A named group is a capturing group identified by a readable name using syntax such as `(?<Phone>...)`.
details:
Named groups make extraction easier to read than relying on numeric group indexes.
recall:
- Why are named groups usually easier to maintain than numbered groups?
```

```concept-card
id: captures-collection
term: Captures Collection
parents:
- capturing-group
related:
- named-group
summary:
The `Captures` collection stores every capture made by a repeated group, not just the final one.
details:
This matters because `Group.Value` returns only the last capture when the same group matches multiple times.
recall:
- Why can `Group.Value` be misleading for repeated groups?
- Which property exposes every capture instead?
```

```concept-card
id: lookaround
term: Lookaround
parents:
- dotnet-regex
related:
- capturing-group
summary:
Lookarounds are zero-width assertions that check whether a pattern appears ahead of or behind the current position.
details:
They are useful when a condition must be enforced without consuming the surrounding text into the main match.
recall:
- What does it mean that a lookaround is zero-width?
- Why is that useful in pattern design?
```

```concept-card
id: regex-options
term: Regex Options
parents:
- dotnet-regex
related:
- regex-timeout
summary:
Regex options modify engine behaviour, such as case sensitivity, multiline anchors, singleline dot behaviour, and compilation.
details:
They let the same pattern language operate under different matching modes depending on context and performance needs.
recall:
- Which option is commonly used for repeated execution of the same regex?
- How do multiline and singleline affect pattern semantics?
```

```concept-card
id: regex-timeout
term: Regex Timeout
parents:
- dotnet-regex
related:
- regex-options
summary:
A regex timeout limits how long a regex operation can run before it is aborted.
details:
Timeouts are a safeguard against runaway execution caused by malicious or pathological input patterns.
recall:
- Why are timeouts part of regex safety, not just performance tuning?
```
