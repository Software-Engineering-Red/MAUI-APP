using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UndacApp.Data;
using UndacApp.Models;

namespace UndacApp.Services
{
    public class SecurityAlertService : ISecurityAlertService
    {
        private SQLiteAsyncConnection _dbConnection;

        private async Task SetUpDb()
        {
            if (_dbConnection != null)
                return;

            _dbConnection = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConnection.CreateTableAsync<SecurityAlert>();
        }

        public async Task<int> CreateSecurityAlert(SecurityAlert alert)
        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(alert);
        }

        public async Task<int> DeleteSecurityAlert(SecurityAlert alert)
        {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(alert);
        }

        public async Task<List<SecurityAlert>> GetSecurityAlerts()
        {
            await SetUpDb();
            return await _dbConnection.Table<SecurityAlert>().ToListAsync();
        }

        public async Task<int> UpdateSecurityAlert(SecurityAlert alert)
        {
            await SetUpDb();
            return await _dbConnection.UpdateAsync(alert);
        }
    }
}
