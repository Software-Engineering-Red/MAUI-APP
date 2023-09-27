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
    internal class PositionStatusesServices : IPositionStatusesServices
    {
        private SQLiteAsyncConnection _dbConnection;

        private async Task SetUpDb()
        {
            if (_dbConnection != null) 
            {
                return;
            }

            _dbConnection = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
                await _dbConnection.CreateTableAsync<position_statuses>();
        }

        public async Task<int> AddStatus(position_statuses status)
        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(status);
        }

        public async Task<int> DeleteStatus(position_statuses status)
        {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(status);
        }

        public async Task<List<position_statuses>> GetPosition_StatusesList()
        {
            await SetUpDb();
            return await _dbConnection.Table<position_statuses>().ToListAsync();
        }

        public async Task<int> UpdateStatus(position_statuses status)
        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(UpdateStatus);
        }

    }
}
