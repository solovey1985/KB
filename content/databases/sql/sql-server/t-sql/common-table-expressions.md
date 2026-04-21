Common Table Expressions (CTEs) are a powerful feature in SQL that allow you to create temporary result sets that can be easily referenced within your main query. They can be thought of as temporary views or inline views for a single query.
- [Syntax:](#syntax)
- [Advantages of CTEs:](#advantages-of-ctes)
- [Examples using AdventureWorks:](#examples-using-adventureworks)
- [Tips:](#tips)

### Syntax:

```sql
WITH cte_name (column_name1, column_name2, ...)
AS (
    -- SQL query that produces the result set
)
-- Main query referencing the CTE
SELECT ...
FROM cte_name ...
```

### Advantages of CTEs:

1. **Readability**: CTEs can make complex SQL queries more readable.
2. **Maintainability**: They can break down complicated queries into simpler parts.
3. **Recursion**: CTEs can be recursive, which means they can reference themselves. This is particularly useful for hierarchical data.

### Examples using AdventureWorks:

1. **Basic CTE**:
   
   Let's say you want to get the top 10 products by the total quantity sold.

   ```sql
   WITH TopProducts AS (
       SELECT ProductID, SUM(OrderQty) as TotalQuantity
       FROM Sales.SalesOrderDetail
       GROUP BY ProductID
       ORDER BY TotalQuantity DESC
       LIMIT 10
   )
   SELECT p.ProductID, p.Name, tp.TotalQuantity
   FROM TopProducts tp
   JOIN Production.Product p ON tp.ProductID = p.ProductID;
   ```

2. **Recursive CTE**:

   AdventureWorks has an `Employee` table with a self-referencing column (`ManagerID`) to indicate who manages each employee. If you want to retrieve the hierarchy of employees:

   ```sql
   WITH EmployeeHierarchy AS (
       -- Base case
       SELECT EmployeeID, ManagerID, FirstName, LastName, 0 as Level
       FROM HumanResources.Employee
       WHERE ManagerID IS NULL

       UNION ALL

       -- Recursive case
       SELECT e.EmployeeID, e.ManagerID, e.FirstName, e.LastName, eh.Level + 1
       FROM HumanResources.Employee e
       JOIN EmployeeHierarchy eh ON e.ManagerID = eh.EmployeeID
   )
   SELECT * FROM EmployeeHierarchy
   ORDER BY Level, LastName, FirstName;
   ```

   This CTE will return all employees, with a `Level` column indicating their depth in the management hierarchy (0 for top-level managers).

3. **Using Multiple CTEs**:

   You can define multiple CTEs in a single query by separating them with commas.

   ```sql
   WITH CTE1 AS (
       -- First CTE query
   ),
   CTE2 AS (
       -- Second CTE query
   )
   -- Main query referencing both CTEs
   SELECT ...
   ```

### Tips:

- Always remember to end the CTE definition with a comma if you're defining multiple CTEs.
- Ensure recursive CTEs have a termination condition to avoid infinite loops.
- While CTEs can make queries more readable, they don't necessarily improve performance. The SQL engine doesn't store the CTE result; it integrates the CTE query into the main query.

CTEs, especially when combined with other SQL features, can be a powerful tool in your SQL toolkit, allowing for more modular, readable, and maintainable queries.