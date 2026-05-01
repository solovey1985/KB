# Relational Design And Integrity

Good SQL starts with table design. Query skill matters less if the schema allows duplicates, invalid references, or ambiguous meaning.

## Keys And Relationships

A primary key identifies one row. A foreign key references a key in another table.

In AdventureWorksLT, `SalesLT.SalesOrderDetail.SalesOrderID` references `SalesLT.SalesOrderHeader.SalesOrderID`, and `SalesLT.SalesOrderDetail.ProductID` references `SalesLT.Product.ProductID`.

```sql
SELECT
    sod.SalesOrderID,
    sod.ProductID,
    p.Name,
    sod.OrderQty
FROM SalesLT.SalesOrderDetail AS sod
JOIN SalesLT.Product AS p
  ON p.ProductID = sod.ProductID;
```

Foreign keys protect the database from child rows that reference nonexistent parent rows.

## Normalization

Normalization separates facts so each fact is stored once. Product names live in `SalesLT.Product`; order quantities live in `SalesLT.SalesOrderDetail`.

This avoids update anomalies. If a product name changes, it should be updated in one product row, not repeated across every historical order detail.

## Denormalization

Denormalization intentionally repeats or precomputes data for read performance or reporting simplicity.

Example: a reporting table may store monthly sales totals instead of recalculating them from every order detail each time.

Use denormalization only when there is a measured read need and a clear maintenance strategy.

## Constraints As Data Quality

Constraints enforce rules close to the data. Common constraints include primary keys, foreign keys, unique constraints, check constraints, and `NOT NULL`.

```sql
SELECT name, type_desc
FROM sys.objects
WHERE parent_object_id = OBJECT_ID('SalesLT.SalesOrderDetail')
ORDER BY type_desc, name;
```

Applications should validate input, but the database should still enforce core integrity rules.
