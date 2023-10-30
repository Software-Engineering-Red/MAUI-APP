using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

/// <summary>
/// Manages CRUD operations on a SQLite database.
/// </summary>
public class DatabaseOperations
{
    private readonly SqliteConnection _connection;

    /// <summary>
    /// Creates a new database operation instance, initializing the connection to the SQLite database.
    /// </summary>
    /// <param name="connectionString">The SQLite database connection string.</param>
    public DatabaseOperations(string connectionString)
    {
        _connection = new SqliteConnection(connectionString);

        try
        {
            _connection.Open();
        }
        catch (Exception ex)
        {
            throw new Exception("Error establishing database connection.", ex);
        }
    }



   

    /// <summary>
    /// Inserts a new record into the specified table.
    /// </summary>
    /// <param name="tableName">The target table name.</param>
    /// <param name="name">The name of the record to insert.</param>
    public void AddRecord(string tableName, string name)
    {
        var commandText = $"INSERT INTO {tableName} (Name) VALUES (@name)";
        ExecuteNonQuery(commandText, ("@name", name));
    }
    /// <summary>
    /// Inserts data into the "media_agencies" table for Radio, TV, and Press.
    /// </summary>
    /// <param name="name">The name of the media agency.</param>
    /// <param name="radio">Radio data for the media agency.</param>
    /// <param name="tv">TV data for the media agency.</param>
    /// <param name="press">Press data for the media agency.</param>
    public void AddMediaAgencyRecord(string agencyName, string radio, string tv, string press)
    {
        
        var commandText = $"INSERT INTO media_request (Name, Radio, TV, Press) VALUES (@name, @radio, @tv, @press)";

        using var command = _connection.CreateCommand();
        command.CommandText = commandText;
        command.Parameters.AddWithValue("@name", agencyName);
        command.Parameters.AddWithValue("@radio", radio);
        command.Parameters.AddWithValue("@tv", tv);
        command.Parameters.AddWithValue("@press", press);

        command.ExecuteNonQuery();
    }


    /// <summary>
    /// Retrieves all records from the specified table's 'Name' column.
    /// </summary>
    /// <param name="tableName">Table from which to fetch records.</param>
    /// <returns>List of names from the table.</returns>
    public List<string> GetAllRecords(string tableName)
    {
        var records = new List<string>();
        var commandText = $"SELECT Name FROM {tableName}";

        using var command = _connection.CreateCommand();
        command.CommandText = commandText;

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            records.Add(reader.GetString(0));
        }

        return records;
    }

    /// <summary>
    /// Retrieves the ID of a record from a specified table based on its name.
    /// </summary>
    /// <param name="tableName">The table to search in.</param>
    /// <param name="name">The name of the record to search for.</param>
    /// <returns>The ID of the record if found; otherwise, throws an exception.</returns>
    public int GetRecordIdByName(string tableName, string name)
    {
        var commandText = $"SELECT Id FROM {tableName} WHERE Name = @name LIMIT 1";

        using var command = _connection.CreateCommand();
        command.CommandText = commandText;
        command.Parameters.AddWithValue("@name", name);

        var result = command.ExecuteScalar();

        if (result != null && result is int id)
        {
            return id;
        }

        throw new Exception($"No record with the name '{name}' found in table '{tableName}'.");
    }
    public List<string> GetRecordsByMediaType(string tableName, string mediaType)
    {
        var records = new List<string>();
        string commandText = $"SELECT Name FROM media_agencies WHERE Radio = @mediaType OR Press = @mediaType OR Tv = @mediaType";

        using var command = _connection.CreateCommand();
        command.CommandText = commandText;
        command.Parameters.AddWithValue("@mediaType", mediaType);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            records.Add(reader.GetString(0));
        }

        return records;
    }



    /// <summary>
    /// Updates a record's name in the specified table based on the provided ID.
    /// </summary>
    /// <param name="tableName">Target table name.</param>
    /// <param name="id">ID of the record to update.</param>
    /// <param name="newName">New name to set for the record.</param>
    public void UpdateRecord(string tableName, int id, string newName)
    {
        var commandText = $"UPDATE {tableName} SET Name = @name WHERE Id = @id";
        ExecuteNonQuery(commandText, ("@name", newName), ("@id", id));
    }

    /// <summary>
    /// Deletes a record from the specified table based on the provided ID.
    /// </summary>
    /// <param name="tableName">Target table name.</param>
    /// <param name="id">ID of the record to delete.</param>
    public void DeleteRecord(string tableName, int id)
    {
        var commandText = $"DELETE FROM {tableName} WHERE Id = @id";
        ExecuteNonQuery(commandText, ("@id", id));
    }

    /// <summary>
    /// Fetches all table names in the database.
    /// </summary>
    /// <returns>List of table names.</returns>
    public List<string> GetAllTableNames()
    {
        var tables = new List<string>();
        const string commandText = "SELECT name FROM sqlite_master WHERE type='table'";

        using var command = _connection.CreateCommand();
        command.CommandText = commandText;

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            tables.Add(reader.GetString(0));
        }

        return tables;
    }

    

    public List<string> GetAllRowNames(String tableName)
    {
        var rows = new List<string>();
        string commandText = $"PRAGMA table_info({tableName})";

        using var command = _connection.CreateCommand();
        command.CommandText = commandText;

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            rows.Add(reader.GetValue(1).ToString());
        }

        return rows;
    }

    /// <summary>
    /// Fetches all records, including their IDs, from the specified table.
    /// </summary>
    /// <param name="tableName">Table from which to fetch records.</param>
    /// <returns>Dictionary containing ID-name pairs of records.</returns>
    public Dictionary<int, string> GetAllRecordsWithIds(string tableName)
    {
        var records = new Dictionary<int, string>();
        var commandText = $"SELECT Id, Name FROM {tableName}";

        using var command = _connection.CreateCommand();
        command.CommandText = commandText;

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            records.Add(reader.GetInt32(0), reader.GetString(1));
        }

        return records;
    }
    

    public Dictionary<int, string> GetAllRecordsWithIdsAndFilter(string tableName, string columnName, string filterValue)
    {
        var records = new Dictionary<int, string>();
        string commandText = $"SELECT Id, Name FROM {tableName} WHERE {columnName} LIKE '%{filterValue}%'";

        using var command = _connection.CreateCommand();
        command.CommandText = commandText;

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            records.Add(reader.GetInt32(0), reader.GetString(1));
        }

        return records;
    }

    /// <summary>
    /// Executes a non-query command on the database (e.g., INSERT, UPDATE, DELETE).
    /// </summary>
    /// <param name="commandText">SQL command text.</param>
    /// <param name="parameters">Parameters to add to the command.</param>
    private void ExecuteNonQuery(string commandText, params (string name, object value)[] parameters)
    {
        using var command = _connection.CreateCommand();
        command.CommandText = commandText;

        foreach (var (name, value) in parameters)
        {
            command.Parameters.AddWithValue(name, value);
        }

        command.ExecuteNonQuery();
    }
}
