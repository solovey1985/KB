---
title: PostgreSQL Core Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes PostgreSQL-specific core concepts.

Study pages: [Section Index](index.md) | [Core Questions](postgresql-core.interview.md) | [Performance Questions](postgresql-performance.interview.md)

## Core Map

```concept-card
id: postgresql
term: PostgreSQL
children:
- identity-columns
- sequences
- returning-clause
- json-versus-jsonb
- identifier-case-rules
summary:
PostgreSQL is an advanced relational database with strong standards support plus powerful engine-specific features.
details:
It supports rich SQL, strong transactional behavior, extensibility, advanced indexing, and provider-specific conveniences that matter in both application and operations work.
example:
PostgreSQL combines standard SQL querying with engine-specific features like `RETURNING`, `jsonb`, and multiple specialized index types.
mnemonic:
Standards first, powerful extensions where they help.
recall:
- Which features make PostgreSQL feel different from generic SQL alone?
- Why should PostgreSQL-specific knowledge sit on top of general SQL, not replace it?
```

```concept-card
id: identity-columns
term: Identity Columns
parents:
- postgresql
related:
- sequences
summary:
Identity columns are the modern SQL-standard way to define auto-generated numeric identifiers in PostgreSQL.
details:
They are the clearer modern alternative to older `SERIAL` shorthand.
example:
`id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY`
mnemonic:
Modern generated IDs, standard style.
recall:
- Why are identity columns preferred over `SERIAL` in newer designs?
- What problem do they solve?
```

```concept-card
id: sequences
term: Sequences
parents:
- postgresql
related:
- identity-columns
summary:
Sequences generate numeric values independently of transaction rollback reuse semantics.
details:
They are the mechanism behind serial-style ID generation and can have gaps when transactions roll back or numbers are consumed early.
example:
A failed insert can still consume a sequence value, leaving a visible gap in later IDs.
mnemonic:
Generate forward, do not promise gapless numbers.
recall:
- Why do PostgreSQL sequences often have gaps?
- Why is gaplessness usually the wrong expectation?
```

```concept-card
id: returning-clause
term: RETURNING Clause
parents:
- postgresql
summary:
The `RETURNING` clause lets write statements immediately return changed row data.
details:
It removes the need for many follow-up queries after inserts, updates, and deletes.
example:
`INSERT INTO users(name) VALUES ('Ana') RETURNING id, created_at;`
mnemonic:
Write once, read the result immediately.
recall:
- Why is `RETURNING` especially useful after inserts?
- What extra round trip can it remove?
```

```concept-card
id: json-versus-jsonb
term: JSON Versus JSONB
parents:
- postgresql
summary:
`json` stores text-form JSON, while `jsonb` stores a binary-processed format optimized for search and indexing.
details:
`jsonb` is usually better for query-heavy workloads, while `json` is more literal and preservation-oriented.
example:
Use `jsonb` for product metadata you filter and index, not just for opaque storage.
mnemonic:
Text for preservation, binary for querying.
recall:
- Why is `jsonb` usually preferred for application queries?
- When might plain `json` still be acceptable?
```

```concept-card
id: identifier-case-rules
term: Identifier Case Rules
parents:
- postgresql
summary:
PostgreSQL folds unquoted identifiers to lowercase but preserves quoted identifiers exactly.
details:
This makes mixed-case quoted names a common source of confusion and avoidable query errors.
example:
`SELECT * FROM Users;` looks for `users`, but `SELECT * FROM "Users";` looks for the quoted mixed-case name.
mnemonic:
Unquoted goes lowercase, quoted stays exact.
recall:
- Why do quoted identifiers often cause friction?
- What naming convention avoids most of that trouble?
```
