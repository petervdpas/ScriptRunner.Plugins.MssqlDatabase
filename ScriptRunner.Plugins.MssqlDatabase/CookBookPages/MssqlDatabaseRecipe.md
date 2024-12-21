---
Title: Working with the MssqlDatabase Plugin  
Category: Cookbook  
Author: Peter van de Pas  
keywords: [CookBook, MssqlDatabase, SQL, ScriptRunner]  
table-use-row-colors: true  
table-row-color: "D3D3D3"  
toc: true  
toc-title: Table of Content  
toc-own-page: true
---

# Recipe: Working with the MssqlDatabase Plugin

## Goal

The **MssqlDatabase Plugin** enables seamless interaction with MSSQL databases in ScriptRunner.  
This recipe provides an overview of the plugin's capabilities, including setting up connections,  
executing queries, managing schema, and working with relationships.

---

## Features of the MssqlDatabase Plugin

1. **Connection Management**  
   Easily sets up and manages connections to MSSQL databases with robust error handling.

2. **Query Execution**
    - Perform non-query operations like **INSERT**, **UPDATE**, and **DELETE**.
    - Execute scalar queries (e.g., "SELECT COUNT(*)") to retrieve single values.
    - Execute full queries to retrieve tabular data.

3. **Schema Management**
    - Load details about database tables and columns.
    - Explore schema relationships, such as foreign keys, dynamically.

4. **Parameterized Queries**  
   Ensure security and prevent SQL injection by using parameterized queries.

---

## Steps to Use the Plugin

### 1. Load the Plugin

Ensure the **MssqlDatabase Plugin** is installed in your ScriptRunner environment.  
It will be automatically registered, making its methods and features available for use in scripts.

### 2. Write a Script

Develop a script using the plugin's features.  
Start by setting up a connection, then perform operations like querying data, updating records,  
or exploring the database schema.

---

## Example Script: General Plugin Workflow

The following script demonstrates the general workflow for using the **MssqlDatabase Plugin**:

```csharp
/*
{
    "TaskCategory": "MssqlDatabase",
    "TaskName": "GeneralMssqlWorkflow",
    "TaskDetail": "Demonstrates the basic functionality of the MssqlDatabase Plugin"
}
*/

var realm = "local";
var connectionString = await GetSettingPickerAsync(realm, 460, 130);
var logger = GetLogger("MssqlDatabase");

if (string.IsNullOrEmpty(connectionString))
{
    Dump("No valid connection string provided. Exiting...");
    return "Script terminated due to missing connection string.";
}

var db = new MssqlDatabase(logger: logger);
db.Setup(connectionString);

// Example 1: Execute a Non-Query (Insert)
string insertQuery = "INSERT INTO ExampleTable (Column1, Column2) VALUES (@Value1, @Value2)";
var insertParams = new Dictionary<string, object>
{
    { "@Value1", "Sample Data" },
    { "@Value2", 12345 }
};
db.ExecuteNonQuery(insertQuery, insertParams);
Dump("Inserted a new record into ExampleTable.");

// Example 2: Execute a Scalar Query
string scalarQuery = "SELECT COUNT(*) FROM ExampleTable";
var recordCount = db.ExecuteScalar(scalarQuery);
Dump($"Total records in ExampleTable: {recordCount}");

// Example 3: Execute a Full Query
string selectQuery = "SELECT * FROM ExampleTable";
var results = db.ExecuteQuery(selectQuery);
DumpTable("ExampleTable Data:", results);

// Example 4: Load Schema Details
var entities = db.LoadEntities("dbo");
foreach (var entity in entities)
{
    Dump($"Table: {entity.Name}");
    foreach (var column in entity.Attributes)
    {
        Dump($"  - Column: {column.Key}, Type: {column.Value["Type"]}, Nullable: {column.Value["IsNullable"]}");
    }
}

// Example 5: Load Relationships
var relationships = db.LoadRelationships("dbo");
foreach (var relationship in relationships)
{
    Dump($"Relationship: {relationship.FromEntity} -> {relationship.ToEntity} via {relationship.Key}");
}

return "MssqlDatabase Plugin script completed.";
```

---

## Explanation of Key Features

### Connection Setup

Use the `Setup` method to configure a database connection string.  
The connection string must be valid and include details like server name, database name, and credentials.

### Query Execution

- **Non-Query**:  
  Use **ExecuteNonQuery** to perform **INSERT**, **UPDATE**, or **DELETE** operations.  
  Example:
  ```csharp
  db.ExecuteNonQuery("INSERT INTO TableName (Column1) VALUES (@Value)", new Dictionary<string, object> { { "@Value", "Data" } });
  ```

- **Scalar Query**:  
  Use **ExecuteScalar** to retrieve single values from a query (e.g., a count).  
  Example:
  ```csharp
  var count = db.ExecuteScalar("SELECT COUNT(*) FROM TableName");
  ```

- **Full Query**:  
  Use **ExecuteQuery** to retrieve a dataset as a **DataTable**.  
  Example:
  ```csharp
  var data = db.ExecuteQuery("SELECT * FROM TableName");
  ```

### Schema and Relationships

- **Load Entities**:  
  Use **LoadEntities** to retrieve schema information about tables and their columns.  
  Example:
  ```csharp
  var entities = db.LoadEntities("dbo");
  ```

- **Load Relationships**:  
  Use **LoadRelationships** to explore foreign key relationships.  
  Example:
  ```csharp
  var relationships = db.LoadRelationships("dbo");
  ```

---

## What You Can Do Next

1. **Build Complex Workflows**:  
   Combine querying, schema exploration, and updates for dynamic workflows.

2. **Implement Custom Automation**:  
   Use the schema and relationship methods to automate database documentation or analysis.

3. **Secure Your Queries**:  
   Use parameterized queries to prevent SQL injection.

---

This recipe provides a comprehensive overview of the **MssqlDatabase Plugin** and its capabilities.  
Adapt and extend these examples to suit your database needs in ScriptRunner.
