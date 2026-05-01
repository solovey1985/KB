---
title: SQL Server Performance And Indexing Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This map covers the core concepts behind SQL Server query tuning and index choices.

Study pages: [Performance And Indexing](performance-and-indexing.md) | [Interview Practice](performance-and-indexing.interview.md)

```concept-card
id: performance-and-indexing
term: Performance And Indexing
children:
- query-shape
- clustered-index
- nonclustered-index
- covering-index
- execution-plan
- statistics-io-time
summary:
Performance and indexing is the practice of matching access paths to real query shapes and measuring the result.
details:
Indexes can speed reads but add write overhead, so they should be designed around predicates, joins, sorting, grouping, and returned columns.
mnemonic:
Query first, index second, measure always.
recall:
- Why should query shape drive index design?
- What is the cost of too many indexes?
```

```concept-card
id: query-shape
term: Query Shape
parents:
- performance-and-indexing
summary:
Query shape is the combination of filters, joins, grouping, sorting, and selected columns in a query.
details:
Index design should follow this shape because different predicates and orderings need different access paths.
recall:
- Which query clauses affect index usefulness?
- Why is a generic index often less useful than a workload-specific one?
```

```concept-card
id: clustered-index
term: Clustered Index
parents:
- performance-and-indexing
related:
- nonclustered-index
summary:
A clustered index defines the table's row order at the leaf level.
details:
There can be only one clustered index per table. It is commonly placed on a stable, narrow, frequently joined key.
recall:
- Why can a table have only one clustered index?
- What qualities make a good clustered key?
```

```concept-card
id: nonclustered-index
term: Nonclustered Index
parents:
- performance-and-indexing
related:
- clustered-index
- covering-index
summary:
A nonclustered index is a separate access path that points back to base rows.
details:
It can support specific searches, joins, sorts, and grouping patterns without changing the table's clustered order.
recall:
- How is a nonclustered index different from a clustered index?
- Why can a table have many nonclustered indexes?
```

```concept-card
id: covering-index
term: Covering Index
parents:
- nonclustered-index
summary:
A covering index contains all columns needed by a query.
details:
It can avoid extra lookups to the base table, often by using key columns plus included columns.
recall:
- What does it mean for an index to cover a query?
- How can included columns reduce lookups?
```

```concept-card
id: execution-plan
term: Execution Plan
parents:
- performance-and-indexing
summary:
An execution plan is SQL Server's chosen strategy for running a query.
details:
Plans show operators such as scans, seeks, joins, sorts, and lookups. They help explain where work is happening.
recall:
- What plan operators often signal expensive work?
- Why should missing index suggestions be reviewed carefully?
```

```concept-card
id: statistics-io-time
term: STATISTICS IO/TIME
parents:
- performance-and-indexing
summary:
`STATISTICS IO` and `STATISTICS TIME` report reads and timing for a query.
details:
They provide practical measurements before and after query or index changes.
example:
SET STATISTICS IO ON; SET STATISTICS TIME ON;
recall:
- Why measure before and after tuning?
- What does logical reads help reveal?
```
