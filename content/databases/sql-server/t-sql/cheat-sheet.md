- [Intermediate:](#intermediate)
- [Subqueries:](#subqueries)
- [Common Table Expressions (CTEs):](#common-table-expressions-ctes)

### Intermediate:

1. **Selecting Data**: 
   ```sql
   SELECT column1, column2, ...
   FROM table_name;
   ```

2. **Filtering Data**:
   ```sql
   SELECT column1, column2, ...
   FROM table_name
   WHERE condition;
   ```

3. **Sorting Data**:
   ```sql
   SELECT column1, column2, ...
   FROM table_name
   ORDER BY column1 [ASC|DESC], column2 [ASC|DESC], ...;
   ```

4. **Joining Tables**:
   ```sql
   SELECT columns
   FROM table1
   INNER JOIN table2 ON table1.column = table2.column;
   ```

5. **Aggregate Functions**:
   ```sql
   SELECT COUNT(column_name), SUM(column_name), AVG(column_name), MIN(column_name), MAX(column_name)
   FROM table_name
   WHERE condition;
   ```

### Subqueries:

1. **Subquery in SELECT**:
   ```sql
   SELECT column1, (SELECT column_name FROM table_name WHERE condition) AS new_column
   FROM table_name;
   ```

2. **Subquery in WHERE**:
   ```sql
   SELECT column1, column2, ...
   FROM table_name
   WHERE column_name operator (SELECT column_name FROM table_name WHERE condition);
   ```

3. **EXISTS**:
   ```sql
   SELECT column1, column2, ...
   FROM table_name
   WHERE EXISTS (SELECT column_name FROM table_name WHERE condition);
   ```

4. **IN**:
   ```sql
   SELECT column1, column2, ...
   FROM table_name
   WHERE column_name IN (SELECT column_name FROM table_name WHERE condition);
   ```

### Common Table Expressions (CTEs):

1. **Basic CTE**:
   ```sql
   WITH CTE_Name AS (
       SELECT column1, column2, ...
       FROM table_name
       WHERE condition
   )
   SELECT * FROM CTE_Name;
   ```

2. **Recursive CTE**:
   ```sql
   WITH Recursive_CTE AS (
       -- Base case
       SELECT column1, column2, ...
       FROM table_name
       WHERE condition
       
       UNION ALL
       
       -- Recursive case
       SELECT column1, column2, ...
       FROM table_name
       JOIN Recursive_CTE ON condition
   )
   SELECT * FROM Recursive_CTE;
   ```

Remember, while cheat sheets are great for quick references, understanding the underlying concepts and practicing regularly will help you become proficient in T-SQL.