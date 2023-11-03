using UndacApp.Data;
using UndacApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UndacApp.Services
{
    public class ISubTeamStatusService : ISubTeamStatus
    {
        private SQLiteAsyncConnection _dbConn;

        /*! <summary>
        Method that initiates a connection to the database. Stored in _dbConn.
        </summary> */
        private async Task SetUpDb()
        {
            if (_dbConn != null)
                return;
            _dbConn = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConn.CreateTableAsync<SubTeamStatus>();
        }

        public async Task<int> AddSubTeamStatus(SubTeamStatus subTeam)
        {
            await SetUpDb();
            return await _dbConn.InsertAsync(subTeam);
        }

        public async Task<int> DeleteSubTeamStatus(SubTeamStatus subTeam)
        {
            await SetUpDb();
            return await _dbConn.DeleteAsync(subTeam);
        }

        public async Task<int> UpdateSubTeamStatus(SubTeamStatus subTeam)
        {
            await SetUpDb();
            return await _dbConn.UpdateAsync(subTeam);
        }

        public async Task<List<SubTeamStatus>> GetSubTeamStatuses()
        {
            await SetUpDb();
            return await _dbConn.Table<SubTeamStatus>().ToListAsync();
        }
    }
}
