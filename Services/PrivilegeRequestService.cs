using UndacApp.Data;
using UndacApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndacApp.Services
{
    public class PrivilegeRequestService : IPrivilegeRequestService
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
            await _dbConnection.CreateTableAsync<PrivilegeRequest>();
        }

        public async Task<List<PrivilegeRequest>> GetPrivilegeRequestList()
        {
            await SetUpDb();
            return await _dbConnection.Table<PrivilegeRequest>().ToListAsync();
        }

        public async Task<int> UpdatePrivilegeRequest(PrivilegeRequest request)
        {
            await SetUpDb();
            return await _dbConnection.UpdateAsync(request);
        }

        public async Task<int> AddRequest(PrivilegeRequest request)
        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(request);

        }

        public async Task<int> DeleteRequest(PrivilegeRequest request)
        {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(request);
        }
    }
}
