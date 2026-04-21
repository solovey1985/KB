T-SQL (Transact-SQL) is Microsoft's proprietary extension to SQL, primarily used with Microsoft SQL Server. It offers a rich set of features beyond standard SQL, and while we've covered several key aspects, there's a lot more to T-SQL. Here are some additional features and concepts:

1. **Variables**: You can declare and use variables in T-SQL.
   ```sql
   DECLARE @MyVariable INT;
   SET @MyVariable = 10;
   ```

2. **Control-of-Flow Language**: T-SQL includes statements for conditional execution (`IF...ELSE`) and loops (`WHILE`).

3. **Error Handling**: T-SQL provides error handling capabilities using `TRY...CATCH` blocks.

4. **Temporary Tables and Table Variables**: You can create temporary tables (`#TableName`) and table variables (`@TableName`) for intermediate computations.

5. **Dynamic SQL**: Using the `EXEC` statement or the `sp_executesql` stored procedure, you can construct and execute SQL statements dynamically.

6. **Functions**: T-SQL supports user-defined functions, both scalar and table-valued.

7. **Stored Procedures**: These are precompiled collections of one or more SQL statements that can be executed as a single call.

8. **Triggers**: Special kind of stored procedures that automatically execute in response to certain events on a particular table or view.

9. **Cursors**: Allow row-by-row processing of the result sets.

10. **Indexes**: While standard to most SQL flavors, it's worth noting that T-SQL offers a variety of indexing options, including clustered, non-clustered, filtered, and full-text indexes.

11. **Transactions**: T-SQL supports transaction processing using `BEGIN TRANSACTION`, `COMMIT`, and `ROLLBACK` statements.

12. **System Functions and Views**: T-SQL provides a plethora of system functions (e.g., `GETDATE()`, `SYSTEM_USER`) and system views (e.g., `sys.tables`, `sys.indexes`) to retrieve information about the database and server.

13. **Common Language Runtime (CLR) Integration**: You can write stored procedures, triggers, user-defined types, and functions using .NET languages and integrate them into your SQL Server environment.

14. **Data Types**: T-SQL has a variety of data types, including some specific to SQL Server like `datetimeoffset`, `hierarchyid`, and `sql_variant`.

15. **Partitioning**: SQL Server supports table partitioning to manage large tables by splitting them into smaller, more manageable pieces.

16. **Security**: T-SQL provides a robust security model with statements for managing logins, roles, users, and permissions.

17. **XML and JSON Support**: T-SQL has built-in support for querying XML-formatted data using XQuery and JSON-formatted data.

18. **Full-Text Search**: A specialized component that allows for full-text querying capabilities on SQL Server data.

19. **Service Broker**: A feature that provides native support for messaging and queuing in the SQL Server database engine.

20. **Spatial Data Types**: T-SQL supports spatial geometry and geography data types for storing and querying spatial data.

This is by no means an exhaustive list, but it gives an overview of the breadth of features and capabilities T-SQL offers. Each of these topics can be a deep dive on its own. If you're interested in any specific area, I'd be happy to provide more detailed information!