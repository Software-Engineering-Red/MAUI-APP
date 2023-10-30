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
    public class ITSystemStatusService : IITSystemStatus
    {
        private SQLiteAsyncConnection _dbConn;
        
        /*! <summary>
        Method that initiates connection to database. Stored in _dbConn.
        </summary> */
        private async Task SetUpDb()
        {
            if (_dbConn != null)
                return;
            _dbConn = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConn.CreateTableAsync<OrderStatus>();
        }

        public async Task<int> AddITSystemStatus(ITSystemStatus item)
        {
            await SetUpDb();
            return await _dbConn.InsertAsync(item);
        }

        public async Task<int> DeleteITSystemStatus(ITSystemStatus item)
        {
            await SetUpDb();
            return await _dbConn.DeleteAsync(item);
        }

        public async Task<int> UpdateITSystemStatus(ITSystemStatus item)
        {
            await SetUpDb();
            return await _dbConn.UpdateAsync(item);
        }

        public async Task<List<ITSystemStatus>> GetITSystemStatuses()
        {
            await SetUpDb();
            return await _dbConn.Table<ITSystemStatus>().ToListAsync();
        }
    }
}