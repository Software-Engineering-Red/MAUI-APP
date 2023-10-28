using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

/// <summary>
/// Initializes a SQLite database by creating the necessary tables.
/// This class is responsible for executing the SQL commands to create tables if they don't exist.
/// </summary>
public class DatabaseInitialiser
{
    // The connection to the SQLite database.
    private readonly SqliteConnection _connection;

    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseInitialiser"/> class.
    /// </summary>
    /// <param name="connection">The SQLite connection to use for database initialization.</param>
    /// <exception cref="ArgumentNullException">Thrown if the provided connection is null.</exception>
    public DatabaseInitialiser(SqliteConnection connection)
    {
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        Console.WriteLine($"Database Initialiser constructed with connection to database: {_connection.DataSource}. Connection state: {_connection.State}");
    }

    /// <summary>
    /// Creates the necessary tables in the SQLite database.
    /// This method will ensure the connection is open, execute the table creation commands, and handle any exceptions that might occur.
    /// </summary>
    public void InitialiseTables()
    {
        Console.WriteLine("InitialiseTables method started.");

        if (_connection.State != System.Data.ConnectionState.Open)
        {
            Console.WriteLine("Connection is not open. Trying to open the connection...");
            _connection.Open();
            Console.WriteLine($"Connection state after trying to open: {_connection.State}");
        }

        try
        {
            var tableCreationCommands = GetTableCreationCommands();

            foreach (var commandText in tableCreationCommands)
            {
                Console.WriteLine($"Executing command: {commandText}");

                using (var command = _connection.CreateCommand())
                {
                    command.CommandText = commandText;
                    command.ExecuteNonQuery();
                    Console.WriteLine($"Command executed successfully: {commandText}");
                }
            }

            Console.WriteLine("All table creation commands executed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}. StackTrace: {ex.StackTrace}");
            // If there's an issue initializing the tables, wrap the original exception in a custom exception for clarity.
            throw new Exception("Error initialising database tables.", ex);
        }
        finally
        {
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                Console.WriteLine("Closing the database connection...");
                _connection.Close();
                Console.WriteLine("Database connection closed.");
            }
        }
    }

    /// <summary>
    /// Retrieves the list of SQL commands to create the necessary tables.
    /// This method defines the schema of the tables that should exist in the SQLite database.
    /// </summary>
    /// <returns>A list of SQL commands for table creation.</returns>
    private static List<string> GetTableCreationCommands()
    {
        return new List<string>
        {
            "CREATE TABLE IF NOT EXISTS continent (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL)",
            "CREATE TABLE IF NOT EXISTS rota_type (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL)",
            "CREATE TABLE IF NOT EXISTS equipment_type (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL)",
            "CREATE TABLE IF NOT EXISTS organisation_type (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL)",
            "CREATE TABLE IF NOT EXISTS order_status (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL)",
            "CREATE TABLE IF NOT EXISTS resource_types (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL)",
            "CREATE TABLE IF NOT EXISTS room_type (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL)",
            "CREATE TABLE IF NOT EXISTS room_use_type (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL)",
            "CREATE TABLE IF NOT EXISTS building_type (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL)",
            "CREATE TABLE IF NOT EXISTS system_type (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL)",
            "CREATE TABLE IF NOT EXISTS system_privilege_level (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL)",
            "CREATE TABLE IF NOT EXISTS operational_authorisation_status (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL)",
            "CREATE TABLE IF NOT EXISTS alert_type (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL)",
            "CREATE TABLE IF NOT EXISTS operational_team_status (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL)",
            "CREATE TABLE IF NOT EXISTS assignment_status (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL)",
            "CREATE TABLE IF NOT EXISTS position_status (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL)",
            "CREATE TABLE IF NOT EXISTS role (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL)",
            "CREATE TABLE IF NOT EXISTS skill (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL)",
            "CREATE TABLE IF NOT EXISTS team_member (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL)",
            "CREATE TABLE IF NOT EXISTS partner_agencies (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL)",
            "CREATE TABLE IF NOT EXISTS vehicle (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL, Health TEXT NOT NULL, Location TEXT NOT NULL, CurrentDeployments TEXT NOT NULL)"
        };
    }
}
