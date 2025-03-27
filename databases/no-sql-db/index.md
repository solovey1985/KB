---
title: No-SQL DataBases
tags: [no-sql, database, basic]
---

# NoSQL Databases

## Principles
NoSQL databases are designed to handle large volumes of unstructured or semi-structured data. They prioritize scalability, flexibility, and performance over strict adherence to relational database principles. Common principles include:
- **Schema-less Data Models**: NoSQL databases do not require a predefined schema, allowing for dynamic and flexible data structures.
- **Horizontal Scaling**: They scale out by adding more servers to distribute data and workload, making them suitable for handling massive datasets.
- **Eventual Consistency**: Instead of strong consistency, NoSQL databases often provide eventual consistency, ensuring data synchronization over time.
- **High Availability**: They are designed to remain operational even in the event of hardware failures.

## NoSQL vs SQL 
- **Data Model**: 
  - SQL databases use structured schemas with tables, rows, and columns.
  - NoSQL databases support diverse data models such as key-value pairs, documents, wide-column stores, and graphs.
- **Scalability**: 
  - SQL databases scale vertically by upgrading hardware.
  - NoSQL databases scale horizontally by adding more nodes to the cluster.
- **Consistency**: 
  - SQL databases follow ACID (Atomicity, Consistency, Isolation, Durability) properties for transactions.
  - NoSQL databases often follow BASE (Basically Available, Soft state, Eventual consistency) principles.
- **Query Language**: 
  - SQL databases use structured query language (SQL).
  - NoSQL databases use APIs or query languages specific to their data model.
- **Use Cases**: 
  - SQL is ideal for applications requiring complex queries and transactions, such as financial systems.
  - NoSQL is better suited for big data, real-time analytics, IoT, and applications with rapidly changing data.

## Examples
- **Key-Value Stores**: 
  - **Redis**: An in-memory data structure store used for caching, real-time analytics, and message brokering.
  - **DynamoDB**: A fully managed key-value and document database by AWS, designed for high availability and scalability.
- **Document Stores**: 
  - **MongoDB**: A popular NoSQL database that stores data in JSON-like documents, ideal for flexible and hierarchical data.
  - **CouchDB**: A database that uses JSON for documents, JavaScript for queries, and HTTP for an API.
- **Column-Family Stores**: 
  - **Cassandra**: A distributed database designed for high availability and scalability, often used for time-series data.
  - **HBase**: A Hadoop-based database for handling large datasets with sparse data.
- **Graph Databases**: 
  - **Neo4j**: A graph database optimized for managing and querying relationships between data points.
  - **ArangoDB**: A multi-model database that supports graph, document, and key-value data models.
