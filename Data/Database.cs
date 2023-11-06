using SQLite;
using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Data
{
    /*this code has come from the master version of the branch that i have added via copied and paste instead of merging master into this branch*/
    public class Database
    {
        public SQLiteAsyncConnection _database;

        public Database()
        {
            InitializeDatabaseAsync().Wait();
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
                await _database.CreateTableAsync<Models.AlertType>();
                await _database.CreateTableAsync<Models.BuildingType>();
                await _database.CreateTableAsync < Models.Continent> ();
                await _database.CreateTableAsync < Models.Equipment> ();
                await _database.CreateTableAsync < Models.OperationalTeamStatus> ();
                await _database.CreateTableAsync < Models.OperationRecords> ();
                await _database.CreateTableAsync < Models.OrderStatus> ();
                await _database.CreateTableAsync < Models.Organisation> ();
                await _database.CreateTableAsync < Models.position_statuses> ();
                await _database.CreateTableAsync < Models.Role> ();
                await _database.CreateTableAsync < Models.RoomType> ();
                await _database.CreateTableAsync < Models.Rota> ();
                await _database.CreateTableAsync < Models.Skill> ();
                await _database.CreateTableAsync < Models.SystemType> ();
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Table creation error: {ex.Message}");
            }
        }
    }
}
