# LINQ

- [Simple Techniques](#simple-techniques)
  - [1. Basics of LINQ](#1-basics-of-linq)
    - [Using LINQ to Objects](#using-linq-to-objects)
    - [Query Syntax vs Method Syntax](#query-syntax-vs-method-syntax)
  - [2. Common LINQ Operations](#2-common-linq-operations)
    - [Filtering](#filtering)
    - [Selecting](#selecting)
    - [Ordering](#ordering)
    - [Aggregation](#aggregation)
- [Advanced Techniques](#advanced-techniques)
  - [Grouping](#grouping)
  - [Joins](#joins)
  - [Set Operations](#set-operations)
  - [Quantifiers](#quantifiers)
  - [Pagination](#pagination)
  - [Lazy Evaluation](#lazy-evaluation)
  - [Using LINQ with Other Data Sources](#using-linq-with-other-data-sources)
  - [Custom Extension Methods](#custom-extension-methods)
  - [Expression Trees](#expression-trees)

## Study Pages

- [Interview Practice](linq-core.interview.md)
- [Concept Map](linq-core.concept.md)

## Simple Techniques

### 1. Basics of LINQ

#### Using LINQ to Objects

This is the practice of querying in-memory collections like arrays or lists.

```csharp
var numbers = new List<int> { 1, 2, 3, 4, 5 };
var evenNumbers = numbers.Where(n => n % 2 == 0);
```

#### Query Syntax vs Method Syntax

- Query Syntax:

```csharp
var evens = from n in numbers
            where n % 2 == 0
            select n;
```

- Method Syntax:

```csharp
var evens = numbers.Where(n => n % 2 == 0);
```

### 2. Common LINQ Operations

#### Filtering

Using `Where` to filter collections based on a predicate.

```csharp
var evens = numbers.Where(n => n % 2 == 0);
```

#### Selecting

Using `Select` to project or transform data.

```csharp
var squares = numbers.Select(n => n * n);
```

#### Ordering

`OrderBy`, `OrderByDescending`, `ThenBy`, and `ThenByDescending` can be used for sorting.

```csharp
var sortedNumbers = numbers.OrderBy(n => n);
```

#### Aggregation

Functions like `Sum`, `Average`, `Min`, `Max` etc.

```csharp
var total = numbers.Sum();
```

## Advanced Techniques

### Grouping

`GroupBy` allows for grouping data based on key values.

```csharp
var groupedNumbers = numbers.GroupBy(n => n % 2 == 0 ? "Even" : "Odd");
```

### Joins

LINQ allows for inner, group, left outer, and cross joins.

```csharp
var products = new List<Product>();
var categories = new List<Category>();

var productCategories = from p in products
                        join c in categories on p.CategoryID equals c.CategoryID
                        select new { p.ProductName, c.CategoryName };
```

### Set Operations

Using methods like `Distinct`, `Union`, `Intersect`, and `Except`.

```csharp
var numbers1 = new List<int> { 1, 2, 3 };
var numbers2 = new List<int> { 3, 4, 5 };
var union = numbers1.Union(numbers2); // {1, 2, 3, 4, 5}
```

### Quantifiers

Methods like `Any`, `All`, and `Contains`.

```csharp
bool hasEvenNumbers = numbers.Any(n => n % 2 == 0);
```

### Pagination

Using `Skip` and `Take` for paging.

```csharp
var page2 = numbers.Skip(10).Take(10);
```

### Lazy Evaluation

One of the strengths of LINQ is deferred execution. This means that a query is not executed until you enumerate over the query, e.g., in a `foreach` loop. This can be leveraged for performance benefits.

### Using LINQ with Other Data Sources

LINQ is not limited to in-memory collections. There's also:

- LINQ to SQL: For querying relational databases.
- LINQ to XML: For querying XML data sources.

### Custom Extension Methods

You can extend LINQ by creating custom extension methods. For instance, you could create a method to filter only prime numbers.

### Expression Trees

When working with sources like databases, LINQ queries are represented as expression trees, which can be translated to other languages, such as SQL for databases.

---

In conclusion, LINQ brings the power and expressiveness of query languages to C#, making it easier to work with and transform data in a consistent way. Whether working with in-memory collections or databases, LINQ provides a uniform approach to data access.
