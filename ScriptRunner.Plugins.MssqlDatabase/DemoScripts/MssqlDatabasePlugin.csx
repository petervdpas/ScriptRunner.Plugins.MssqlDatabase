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

// Execute a Non-Query to insert data
var insertQuery = "INSERT INTO ExampleTable (Column1, Column2) VALUES (@Value1, @Value2)";
var insertParams = new Dictionary<string, object>
{
    { "@Value1", "Sample Data" },
    { "@Value2", 12345 }
};
db.ExecuteNonQuery(insertQuery, insertParams);
Dump("Inserted a new record into ExampleTable.");

// Execute a Scalar Query to count records
var scalarQuery = "SELECT COUNT(*) FROM ExampleTable";
var recordCount = db.ExecuteScalar(scalarQuery);
Dump($"Total records in ExampleTable: {recordCount}");

// Execute a Full Query to retrieve data
var selectQuery = "SELECT * FROM ExampleTable";
var results = db.ExecuteQuery(selectQuery);
DumpTable("ExampleTable Data:", results);

// Load Schema Details
var entities = db.LoadEntities("dbo");
foreach (var entity in entities)
{
    Dump($"Table: {entity.Name}");
    foreach (var column in entity.Attributes)
    {
        Dump($"  - Column: {column.Key}, Type: {column.Value["Type"]}, Nullable: {column.Value["IsNullable"]}");
    }
}

// Load Relationships
var relationships = db.LoadRelationships("dbo");
foreach (var relationship in relationships)
{
    Dump($"Relationship: {relationship.FromEntity} -> {relationship.ToEntity} via {relationship.Key}");
}

return "MssqlDatabase Plugin script completed.";
