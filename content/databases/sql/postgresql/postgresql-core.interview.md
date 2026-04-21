---
title: PostgreSQL Core Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise PostgreSQL-specific querying and schema concepts.

Relevant concept maps:

- [PostgreSQL Core Concept Map](postgresql-core.concept.md)
- [PostgreSQL Performance Concept Map](postgresql-performance.concept.md)

## PostgreSQL Basics

```interview-question
What is the difference between `SERIAL` and `GENERATED ... AS IDENTITY` in PostgreSQL?
---
answer:
`SERIAL` is a convenience shorthand that creates an integer column plus an associated sequence.

`GENERATED ... AS IDENTITY` is the SQL-standard way to define an auto-generated identity column and is generally the modern preferred form.

Both rely on sequence-like behavior, but identity columns are clearer and more standards-aligned.
hints:
- One is older shorthand.
- One is the standards-based modern form.
- Both are about generated numeric identifiers.
```

Related concepts: [Identity Columns](postgresql-core.concept.md#identity-columns), [Sequences](postgresql-core.concept.md#sequences)

```interview-question
Why is `RETURNING` useful in PostgreSQL?
---
answer:
`RETURNING` lets an `INSERT`, `UPDATE`, or `DELETE` return row data directly from the modified statement.

It is useful for reading generated IDs, timestamps, or changed values without issuing a second query.

This is especially convenient for insert workflows and audit-aware updates.
hints:
- It returns data from a write statement.
- It avoids a follow-up select.
- Generated IDs are a common use case.
```

Related concepts: [RETURNING Clause](postgresql-core.concept.md#returning-clause)

```interview-question
What is the difference between `json` and `jsonb` in PostgreSQL?
---
answer:
`json` stores the original JSON text representation, while `jsonb` stores a binary-parsed format optimized for querying and indexing.

`jsonb` is usually the better choice when the data will be searched, indexed, or manipulated.

`json` is more appropriate when exact textual preservation matters more than query performance.
hints:
- One is text-oriented.
- One is binary and query-oriented.
- Indexing is the big clue.
```

Related concepts: [JSON Versus JSONB](postgresql-core.concept.md#json-versus-jsonb)

```interview-choice
Which PostgreSQL type is usually the better fit for indexed semi-structured data?
---
options:
- `json`
- `jsonb`
- `text[]`
correct: 1
explanation:
`jsonb` is designed for efficient querying and indexing of semi-structured JSON data.
```

```interview-question
Why are quoted identifiers and unquoted identifiers important in PostgreSQL?
---
answer:
Unquoted identifiers are folded to lowercase in PostgreSQL.

Quoted identifiers preserve case and special characters, which can create confusing naming if used inconsistently.

The safest convention is usually to use simple lowercase unquoted names.
hints:
- Case folding is the key behavior.
- Quoted names preserve case.
- Naming consistency matters.
```

Related concepts: [Identifier Case Rules](postgresql-core.concept.md#identifier-case-rules)
