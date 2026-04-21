# Databases Topic Coverage and Next Topics

This document describes the current database study coverage and the next topics that should be added to make the database section more complete and better balanced across SQL, provider-specific databases, and NoSQL.

## Current Coverage

Implemented structure:

- `content/databases/sql/general/`
- `content/databases/sql/sql-server/`
- `content/databases/sql/postgresql/`
- `content/databases/no-sql/`

Implemented topic groups:

### General SQL

- basic interview and concept pages
- intermediate interview and concept pages
- advanced interview and concept pages
- optimization interview and concept pages

### SQL Server

- existing SQL Server querying and indexing interview page
- existing SQL Server querying and indexing concept page
- T-SQL notes
- indexing notes

### PostgreSQL

- PostgreSQL core interview and concept pages
- PostgreSQL performance interview and concept pages

### NoSQL

- introductory index page only

## Main Gaps

The current database section is now well structured, but coverage is still uneven.

Biggest gaps:

1. SQL Server has notes and one interactive page set, but it does not yet have a full provider-specific study track like PostgreSQL.
2. PostgreSQL currently covers only a core page and a performance page; it can still be expanded in operations and modeling.
3. NoSQL has only a static overview page and no interview or concept pages.
4. Cross-topic pages for database design and distributed database trade-offs are still missing.

## Recommended Next Topics

## 1. SQL Server Deepening

Recommended files:

- `content/databases/sql/sql-server/sql-server-core.interview.md`
- `content/databases/sql/sql-server/sql-server-core.concept.md`
- `content/databases/sql/sql-server/sql-server-performance.interview.md`
- `content/databases/sql/sql-server/sql-server-performance.concept.md`

Recommended coverage:

- T-SQL language features
- temp tables versus table variables
- stored procedures and functions
- triggers
- SQL Server execution plans
- clustered versus non-clustered indexes
- included columns
- isolation and locking behavior in SQL Server terms
- SQL Server maintenance basics

Why this is next:

- SQL Server already has source notes in the repo.
- The provider-specific branch looks incomplete compared with PostgreSQL.
- It can reuse and reorganize existing material rather than requiring net new research only.

## 2. PostgreSQL Schema and Advanced Features

Recommended files:

- `content/databases/sql/postgresql/postgresql-schema-types.interview.md`
- `content/databases/sql/postgresql/postgresql-schema-types.concept.md`
- `content/databases/sql/postgresql/postgresql-advanced-features.interview.md`
- `content/databases/sql/postgresql/postgresql-advanced-features.concept.md`

Recommended coverage:

- arrays
- UUID
- enums
- range types
- `citext`
- expression indexes
- partial indexes
- `DISTINCT ON`
- regular expression matching
- `NULLS FIRST` / `NULLS LAST`
- functions and procedures
- PostgreSQL-specific constraints and modeling choices

Why this is next:

- The current PostgreSQL track is strong on basics and performance, but still light on schema and advanced query features.
- PostgreSQL-specific interview value often comes from these differences rather than only generic SQL.

## 3. PostgreSQL Operations and Replication

Recommended files:

- `content/databases/sql/postgresql/postgresql-operations.interview.md`
- `content/databases/sql/postgresql/postgresql-operations.concept.md`

Recommended coverage:

- `psql` usage
- roles and authentication basics
- `pg_hba.conf`
- backup and restore concepts
- WAL basics
- streaming replication
- logical replication
- connection limits and pooling
- vacuum and autovacuum operational concerns

Why this is next:

- PostgreSQL interviews often include practical operational questions.
- The official docs and FAQ are strong enough to support this material well.

## 4. NoSQL Interactive Track

Recommended files:

- `content/databases/no-sql/no-sql-basics.interview.md`
- `content/databases/no-sql/no-sql-basics.concept.md`
- `content/databases/no-sql/no-sql-systems.interview.md`
- `content/databases/no-sql/no-sql-systems.concept.md`

Recommended coverage:

- SQL versus NoSQL trade-offs
- CAP and consistency trade-offs
- document databases
- key-value stores
- wide-column stores
- graph databases
- when to choose each model
- common misconceptions about NoSQL

Why this is next:

- The NoSQL section is currently the weakest structured area.
- Even a two-page interview/concept track would make the database section feel much more balanced.

## 5. Database Design Topic

Recommended files:

- `content/databases/sql/general/database-design.interview.md`
- `content/databases/sql/general/database-design.concept.md`

Recommended coverage:

- entity modeling
- normalization versus denormalization
- surrogate versus natural keys
- many-to-many modeling
- audit columns
- soft delete at the database level
- schema evolution trade-offs

Why this is next:

- General SQL pages already touch normalization, but schema and relational design deserve a dedicated focused topic.
- It would connect well to both SQL interviews and EF Core design decisions.

## 6. Distributed Data and Scaling Topic

Recommended files:

- `content/databases/sql/general/distributed-data.interview.md`
- `content/databases/sql/general/distributed-data.concept.md`

Recommended coverage:

- replication versus sharding
- read replicas
- consistency trade-offs
- failover basics
- write scaling limits
- partitioning
- hot partitions
- cross-region data concerns

Why this is next:

- This topic sits naturally between databases and architecture/system-design.
- It becomes important once readers move beyond single-node query knowledge.

## Suggested Build Order

Recommended implementation order:

1. SQL Server provider-specific interactive pages
2. NoSQL interactive pages
3. PostgreSQL schema and advanced features
4. PostgreSQL operations and replication
5. Database design topic
6. Distributed data and scaling topic

## Authoring Rules For Next Topics

When adding the next database topics:

1. Keep provider-neutral material in `sql/general/`.
2. Keep engine-specific behavior in the provider folder only.
3. Avoid duplicating EF Core theory in databases; link to `content/backend/dotnet/ef-core-data-access/` instead.
4. Keep concept pages under roughly 15 cards each.
5. Add concrete examples for abstract database concepts.
6. Add `Study pages:` links to every concept page.
7. Add `Study Flow` and `Related Topics` sections to every topic index.

## Related Indexes

- [Databases Study Index](index.md)
- [SQL Study Index](sql/index.md)
- [.NET Backend Study Index](../backend/dotnet/index.md)
