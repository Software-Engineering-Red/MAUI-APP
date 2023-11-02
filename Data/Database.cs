using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Data
{
    public class Database
    {
        public SQLiteAsyncConnection _database;
        public Database()
        {
            InitializeDatabaseAsync().Wait(); // Ensure initialization is complete before proceeding
        }

        public async Task InitializeDatabaseAsync()
        {
            if (_database is not null)
                return;

            try
            {
                _database = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
                await CreateTablesAsync();
            }
            catch (Exception ex)
            {
                // Handle any initialization errors here, e.g., log or display an error message.
                Console.WriteLine($"Database initialization error: {ex.Message}");
            }
        }
        public async Task CreateTablesAsync()
        {
            try
            {
                // Create your database tables here
                await _database.CreateTableAsync<Models.AlertType>();
                await _database.CreateTableAsync<Models.OperationalTeamStatus>();

                // Add more CreateTableAsync calls for additional tables as needed.
            }
            catch (Exception ex)
            {
                // Handle any table creation errors here, e.g., log or display an error message.
                Console.WriteLine($"Table creation error: {ex.Message}");
            }
        }
    }
}
