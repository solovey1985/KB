T-SQL, being the extension of SQL used in Microsoft SQL Server, offers a wide range of data types. These data types help define the nature of data that can be stored in a database table. Here's a list of the primary data types in T-SQL, grouped by their general category:

### 1. Exact Numerics:

- **bigint**: Integer (whole number) data from -2^63 (-9,223,372,036,854,775,808) through 2^63-1 (9,223,372,036,854,775,807).
- **bit**: Integer data with either a 1 or 0 value.
- **decimal and numeric**: Fixed precision and scale numbers. Allows you to specify the total number of digits and the number of digits to the right of the decimal point.
- **int**: Integer (whole number) data from -2^31 (-2,147,483,648) through 2^31 - 1 (2,147,483,647).
- **money and smallmoney**: Currency values ranging from -922,337,203,685,477.5808 to 922,337,203,685,477.5807 for `money`, and -214,748.3648 to 214,748.3647 for `smallmoney`.
- **smallint**: Integer data from -2^15 (-32,768) to 2^15-1 (32,767).
- **tinyint**: Integer data from 0 to 255.

### 2. Approximate Numerics:

- **float**: Floating precision number data from -1.79E + 308 through 1.79E + 308.
- **real**: Floating precision number data from -3.40E + 38 through 3.40E + 38.

### 3. Date and Time:

- **date**: Date data, ranging from January 1, 0001 through December 31, 9999.
- **datetime and smalldatetime**: Date and time data. `datetime` ranges from January 1, 1753, through December 31, 9999, with an accuracy of 3.33 milliseconds. `smalldatetime` ranges from January 1, 1900, through June 6, 2079, with an accuracy of one minute.
- **datetime2**: Date and time data from January 1, 0001 through December 31, 9999, with a fractional seconds precision that lets you define fractions of a second.
- **datetimeoffset**: Date and time data with time zone awareness.
- **time**: Time data based on a 24-hour clock.

### 4. Character Strings:

- **char and varchar**: Fixed-length (`char`) or variable-length (`varchar`) non-Unicode character data.
- **text**: Variable-length non-Unicode data with a maximum length of 2^31-1 (2,147,483,647) characters.

### 5. Unicode Character Strings:

- **nchar and nvarchar**: Fixed-length (`nchar`) or variable-length (`nvarchar`) Unicode data.
- **ntext**: Variable-length Unicode data with a maximum length of 2^30-1 (1,073,741,823) characters.

### 6. Binary Strings:

- **binary and varbinary**: Fixed-length (`binary`) or variable-length (`varbinary`) binary data.
- **image**: Variable-length binary data with a maximum length of 2^31-1 (2,147,483,647) bytes.

### 7. Other Data Types:

- **cursor**: A reference to a cursor.
- **hierarchyid**: A system data type for representing hierarchical tree structures.
- **sql_variant**: A data type that can store values of various data types, except text, ntext, and timestamp.
- **table**: A special data type used to store a result set for later processing.
- **timestamp (or rowversion)**: A unique binary number within a database, generally used as a mechanism for version-stamping table rows.
- **uniqueidentifier**: A globally unique identifier (GUID).
- **xml**: Stores XML formatted data.

### 8. Spatial Data Types:

SQL Server supports two spatial data types: `geometry` and `geography`.

1. **geometry**: Represents data in a flat (planar) form. It's useful for spatial data that's on a flat surface, like floor plans or circuit designs.

2. **geography**: Represents data on a round earth, which means it considers the earth's curvature. It's useful for global spatial data, like GPS coordinates.

Both data types support a set of methods that allow for spatial operations like calculating distance, area, or checking if one shape intersects with another.

**Example**:

```sql
-- Create a table to store spatial data
CREATE TABLE SpatialTable (
    ID INT PRIMARY KEY,
    GeoCol1 geography,
    GeoCol2 geometry
);

-- Insert points into the table
INSERT INTO SpatialTable (ID, GeoCol1, GeoCol2)
VALUES (1, geography::STGeomFromText('POINT(-122.34900 47.65100)', 4326), 
           geometry::STGeomFromText('POINT(47.65100 -122.34900)', 0));

-- Calculate the distance between two points
DECLARE @g1 geography;
DECLARE @g2 geography;
SET @g1 = geography::STGeomFromText('POINT(-122.34820 47.65100)', 4326);
SET @g2 = geography::STGeomFromText('POINT(-122.34900 47.65100)', 4326);
SELECT @g1.STDistance(@g2);
```

In this example, we're using the `STGeomFromText` method to create spatial objects from Well-Known Text (WKT) representations. The `STDistance` method calculates the distance between two points.

When working with spatial data, it's also common to use spatial indexes to speed up queries, especially when dealing with complex shapes or large datasets.

### 9. Special-Purpose:

 #### FILESTREAM:

`FILESTREAM` was introduced in SQL Server 2008 to store and manage unstructured data externally in the file system, while still maintaining a link to the data within the database. This approach combines the benefits of accessing LOB data directly from the file system with the referential integrity and ease of access offered by the relational database.

**Advantages**:
- Large objects can be stored outside the database file, reducing database size and backup time.
- Provides transactional consistency between the database engine and the NTFS file system.
- Allows for efficient streaming of large objects.

**Example**:
To use `FILESTREAM`, you need to:
1. Enable `FILESTREAM` at the instance level.
2. Create a filegroup for `FILESTREAM` in the database.
3. Define a table with a `FILESTREAM` column.

```sql
-- Assuming FILESTREAM is enabled and a FILESTREAM filegroup is added to the database

CREATE TABLE DocumentStore (
    DocumentID INT PRIMARY KEY,
    Document VARBINARY(MAX) FILESTREAM NULL,
    DocGUID UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL UNIQUE DEFAULT NEWID()
);
```

In this example, `Document` is a `FILESTREAM` column that stores the actual file data in the file system, while `DocGUID` provides a unique link to the data.