using Microsoft.Data.Sqlite;
using System;
using System.IO;

/// <summary>
/// Represents a database connection to a SQLite database.
/// This class is responsible for establishing a connection to the database and provides a way to retrieve the connection.
/// It also implements IDisposable to ensure resources are cleaned up properly.
/// </summary>
public class DatabaseConnection : IDisposable
{
    // The connection to the SQLite database.
    private readonly SqliteConnection _connection;

    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseConnection"/> class and establishes a connection to the database.
    /// </summary>
    /// <exception cref="Exception">Thrown when there's an error establishing a database connection.</exception>
    public DatabaseConnection()
    {
        try
        {
            // Determine the path where the SQLite database is located.
            // It uses the local application data folder to store the SQLite file.
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "reference_values5.sqlite");

            // Create a new connection using the determined path.
            _connection = new SqliteConnection($"Data Source={dbPath}");

            // Open the connection.
            _connection.Open();
        }
        catch (Exception ex)
        {
            // If there's an issue connecting to the database, wrap the original exception in a custom exception for clarity.
            throw new Exception("Error establishing database connection.", ex);
        }
    }

    /// <summary>
    /// Gets the SQLite connection associated with this instance.
    /// </summary>
    public SqliteConnection Connection => _connection;

    /// <summary>
    /// Disposes the database connection, releasing all its resources.
    /// </summary>
    public void Dispose()
    {
        // Ensure the connection is properly disposed of to free up resources.
        _connection?.Dispose();
    }
}
