// NeedService.cs
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using UndacApp.Data;
using UndacApp.Models;

namespace UndacApp.Services
{
    internal class NeedService : INeedService
    {
        private SQLiteAsyncConnection _dbConnection;

        // Wait for db con
        private async Task SetUpDb()
        {
            if (_dbConnection != null)
            {
                return;
            }

            _dbConnection = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConnection.CreateTableAsync<Need>();
        }

        // Add a new need
        public async Task<int> AddNeed(Need need)
        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(need);
        }

        // Delete a need
        public async Task<int> DeleteNeed(Need need)
        {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(need);
        }

        // Get the list of needs
        public async Task<List<Need>> GetAllNeeds()
        {
            await SetUpDb();
            return await _dbConnection.Table<Need>().ToListAsync();
        }

        // Update a need
        public async Task<int> UpdateNeed(Need need)
        {
            await SetUpDb();
            return await _dbConnection.UpdateAsync(need);
        }
    }
}
