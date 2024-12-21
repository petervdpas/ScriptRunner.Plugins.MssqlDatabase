# MssqlDatabase Plugin for ScriptRunner

![License](https://img.shields.io/badge/license-MIT-green)  
![Version](https://img.shields.io/badge/version-1.0.0-blue)

The **MssqlDatabase Plugin** extends ScriptRunner with robust connectivity to MSSQL databases, enabling efficient execution of SQL queries, schema exploration, and database operations. This plugin simplifies integration with MSSQL databases using features like parameterized queries and schema relationship mapping.

## Features

- **Database Connection Management**: Easily set up and manage MSSQL database connections.
- **SQL Query Execution**:
    - Execute non-query commands (`INSERT`, `UPDATE`, `DELETE`).
    - Execute scalar queries (e.g., `SELECT COUNT`).
    - Retrieve datasets with parameterized queries.
- **Schema Management**:
    - Load entities (tables and columns) from the database schema.
    - Load relationships (foreign key constraints) between entities.
- **Parameter Safety**: Prevent SQL injection with parameterized queries.
- **Azure AD Authentication Support**: Use Azure Active Directory Interactive Authentication for secure access.

## Installation

1. Clone the repository or download the plugin files.
2. Build the project to generate the plugin DLL.
3. Place the compiled DLL into the `Plugins` directory of your ScriptRunner installation.

## Configuration

To use the plugin, provide the following configuration options in your ScriptRunner setup:

```json
{
  "DefaultConnectionString": "Server=myServer;Database=myDatabase;User Id=myUsername;Password=myPassword;"
}
```

## Usage

### Registering the Plugin

The plugin automatically registers itself with ScriptRunner. Ensure your ScriptRunner instance is configured to load this plugin.

### Setting Up the Connection

Before executing queries, set up the database connection using a valid connection string:

```csharp
MssqlDatabase database = new MssqlDatabase(logger);
database.Setup("Server=myServer;Database=myDatabase;User Id=myUsername;Password=myPassword;");
```

### Executing Queries

#### Non-Query Command
```csharp
var rowsAffected = database.ExecuteNonQuery("DELETE FROM Users WHERE IsActive = 0");
```

#### Scalar Query
```csharp
var userCount = database.ExecuteScalar("SELECT COUNT(*) FROM Users");
```

#### Query with Parameters
```csharp
var parameters = new Dictionary<string, object> { { "@UserId", 1 } };
var resultTable = database.ExecuteQuery("SELECT * FROM Users WHERE UserId = @UserId", parameters);
```

### Loading Entities and Relationships

#### Load Entities
```csharp
var entities = database.LoadEntities("dbo");
foreach (var entity in entities)
{
    Console.WriteLine($"Table: {entity.Name}");
    foreach (var attribute in entity.Attributes)
    {
        Console.WriteLine($"- Column: {attribute.Key}, Type: {attribute.Value["Type"]}");
    }
}
```

#### Load Relationships
```csharp
var relationships = database.LoadRelationships("dbo");
foreach (var relationship in relationships)
{
    Console.WriteLine($"{relationship.FromEntity} -> {relationship.ToEntity} via {relationship.Key}");
}
```

## Development

### Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [ScriptRunner Framework](https://www.scriptrunner.com/)

### Building the Plugin

Run the following command to build the project:

```bash
dotnet build
```

### Running Tests

```bash
dotnet test
```

## Contributing

Contributions are welcome! Please submit a pull request or open an issue for suggestions or bug reports.

## License

This project is licensed under the [MIT License](LICENSE).
