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
    public class PrivledgeRequestService : IPrivledgeRequestService
    {
        private SQLiteAsyncConnection _dbConnection;

        /*! <summary>
            Method that initiates connection to SQLite database, and creates TeamMember class table, if none is present.
        </summary> */
        private async Task SetUpDb()
        {
            if (_dbConnection != null)
                return;


            _dbConnection = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConnection.CreateTableAsync<PrivledgeRequest>();
        }

        public async Task<List<PrivledgeRequest>> GetPrivledgeRequestList()
        {
            await SetUpDb();
            return await _dbConnection.Table<PrivledgeRequest>().ToListAsync();
        }

        public async Task<int> UpdatePrivledgeRequest(PrivledgeRequest request)
        {
            await SetUpDb();
            return await _dbConnection.UpdateAsync(request);
        }

        public async Task<int> AddRequest(PrivledgeRequest request)
        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(request);

        }
    }
}
