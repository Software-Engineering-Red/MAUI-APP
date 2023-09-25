using MauiApp1.Data;
using MauiApp1.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public class SystemTypeService : ISystemTypeService
    {

        private SQLiteAsyncConnection _dbConnection;

        private async Task SetUpDb()
        {
            if (_dbConnection != null)
                return;


            _dbConnection = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConnection.CreateTableAsync<SystemType>();
        }

        public async Task<int> AddSystemType(SystemType systemType)
        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(systemType);
        }

        public async Task<int> DeleteSystemType(SystemType systemType)
        {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(systemType);
        }

        public async Task<List<SystemType>> GetSystemTypeList()
        {
            await SetUpDb();
            return await _dbConnection.Table<SystemType>().ToListAsync();
        }

        public async Task<int> UpdateSystemType(SystemType systemType)
        {
            await SetUpDb();
            return await _dbConnection.UpdateAsync(systemType);
        }
    }
}
