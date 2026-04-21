---
title: LINQ Core Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise the LINQ distinctions that matter most when reading and writing queries in C#:

- query syntax versus method syntax
- deferred execution
- projection and flattening
- grouping, joining, and paging

## Foundations

```interview-question
What is deferred execution in LINQ?
---
answer:
Deferred execution means a LINQ query is usually not executed when it is defined, but when it is enumerated.

This allows queries to remain composable and lets them operate on the latest state of the source collection at enumeration time.
hints:
- Think about `foreach`.
- Query creation and query execution are not always the same moment.
- This behaviour is often useful, but it can also surprise people.
```

```interview-question
What is the difference between query syntax and method syntax in LINQ?
---
answer:
Query syntax is the SQL-like `from`, `where`, and `select` form.

Method syntax uses extension methods such as `Where`, `Select`, and `OrderBy`.

Both express LINQ queries, but method syntax exposes the full operator surface and is often used for more advanced chains.
hints:
- One looks more declarative and SQL-like.
- The other is built from chained extension methods.
- They are two ways to express the same querying model.
```

```interview-question
How does `SelectMany` differ from `Select`?
---
answer:
`Select` projects each source element into a new value.

`SelectMany` projects each source element into another sequence and then flattens those nested sequences into one output sequence.
hints:
- One keeps one output element per input element.
- The other flattens nested collections.
- Think of customers and their orders.
```

```interview-question
When would you use `Skip` and `Take` together?
---
answer:
`Skip` and `Take` are commonly used together for paging or slicing a sequence.

For example, page 2 of a 10-item page size skips the first 10 items and then takes the next 10.
hints:
- Think pagination.
- One operator discards a prefix, the other limits the result size.
- They are usually paired.
```

## Multiple Choice Questions

```interview-choice
Which LINQ operator flattens nested sequences into a single sequence?
---
options:
- Select
- SelectMany
- GroupBy
correct: 1
explanation:
`SelectMany` is the flattening projection operator.
It is the right choice when each source item produces another sequence.
```

```interview-choice
Which operator checks whether any element satisfies a condition?
---
options:
- All
- Any
- Contains
correct: 1
explanation:
`Any` returns `true` when at least one element matches the predicate.
```

```interview-choice
Which statement about deferred execution is correct?
---
options:
- LINQ queries always execute as soon as they are declared.
- Deferred execution means the query usually runs when enumerated.
- Deferred execution only exists in query syntax, not method syntax.
correct: 1
explanation:
Many LINQ operators build an execution pipeline that runs later, when the result is actually enumerated.
```

## Code Completion Questions

```interview-code
language: cs
prompt: Complete the query so it returns the squares of even numbers.
starter:
var result = numbers
  .Where(n => n % 2 == 0)
  .
solution:
var result = numbers
  .Where(n => n % 2 == 0)
  .Select(n => n * n);
checks:
- includes: Select
- includes: n * n
```

```interview-code
language: cs
prompt: Complete the query so it groups numbers into "Even" and "Odd" buckets.
starter:
var groups = numbers.
solution:
var groups = numbers.GroupBy(n => n % 2 == 0 ? "Even" : "Odd");
checks:
- includes: GroupBy
- includes: Even
- includes: Odd
```

```interview-code
language: cs
prompt: Complete the query so it joins products to categories by `CategoryID`.
starter:
var productCategories = from p in products
                        join c in categories on p.CategoryID equals c.CategoryID
                        
solution:
var productCategories = from p in products
                        join c in categories on p.CategoryID equals c.CategoryID
                        select new { p.ProductName, c.CategoryName };
checks:
- includes: select new
- includes: p.ProductName
- includes: c.CategoryName
```

## Study Notes

Use these notes for the longer walkthroughs and operator inventory:

- [LINQ Index](index.md)
- [Operators](operators.md)
