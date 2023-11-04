using MauiApp1.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public class DatabaseOperations
    {
        private readonly string _dbPath;

        public DatabaseOperations(string dbPath)
        {
            _dbPath = dbPath;
            InitializeDatabase().Wait();
        }

        private async Task InitializeDatabase()
        {
            using (var connection = new SqliteConnection($"Data Source={_dbPath}"))
            {
                await connection.OpenAsync();
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Pins (
                                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                        Name TEXT,
                                        Latitude REAL,
                                        Longitude REAL,
                                        PinType TEXT,
                                        StatusOrPurpose TEXT
                                    )";
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public List<CustomPin> GetAllPins()
        {
            var pins = new List<CustomPin>();

            using (var connection = new SqliteConnection($"Data Source={_dbPath}"))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT Id, Name, Latitude, Longitude, PinType, StatusOrPurpose FROM Pins";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pins.Add(new CustomPin
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Latitude = reader.GetDouble(2),
                            Longitude = reader.GetDouble(3),
                            PinType = reader.GetString(4),
                            StatusOrPurpose = reader.GetString(5)
                        });
                    }
                }
            }

            return pins;
        }

        public async Task InsertPinAsync(CustomPin pin)
        {
            using (var connection = new SqliteConnection($"Data Source={_dbPath}"))
            {
                await connection.OpenAsync();
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO Pins (Name, Latitude, Longitude, PinType, StatusOrPurpose) 
                                    VALUES (@Name, @Latitude, @Longitude, @PinType, @StatusOrPurpose)";
                cmd.Parameters.AddWithValue("@Name", pin.Name);
                cmd.Parameters.AddWithValue("@Latitude", pin.Latitude);
                cmd.Parameters.AddWithValue("@Longitude", pin.Longitude);
                cmd.Parameters.AddWithValue("@PinType", pin.PinType);
                cmd.Parameters.AddWithValue("@StatusOrPurpose", pin.StatusOrPurpose);

                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdatePinAsync(CustomPin pin)
        {
            using (var connection = new SqliteConnection($"Data Source={_dbPath}"))
            {
                await connection.OpenAsync();
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"UPDATE Pins SET Name = @Name, Latitude = @Latitude, Longitude = @Longitude, 
                                    PinType = @PinType, StatusOrPurpose = @StatusOrPurpose WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", pin.Id);
                cmd.Parameters.AddWithValue("@Name", pin.Name);
                cmd.Parameters.AddWithValue("@Latitude", pin.Latitude);
                cmd.Parameters.AddWithValue("@Longitude", pin.Longitude);
                cmd.Parameters.AddWithValue("@PinType", pin.PinType);
                cmd.Parameters.AddWithValue("@StatusOrPurpose", pin.StatusOrPurpose);

                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task DeletePinAsync(CustomPin pin)
        {
            using (var connection = new SqliteConnection($"Data Source={_dbPath}"))
            {
                await connection.OpenAsync();
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"DELETE FROM Pins WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", pin.Id);

                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
