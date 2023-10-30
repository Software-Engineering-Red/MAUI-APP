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
    public class TeamService : ITeamService
    {
        private SQLiteAsyncConnection _dbConn;
        /*! <summary>
        Method that initiates connection to database. Will create the OrderStatus table if connected.
        </summary> */
        private async Task SetUpDb()
        {
            if (_dbConn != null)
                return;


            _dbConn = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConn.CreateTableAsync<OrderStatus>();
        }

        public async Task<int> AddTeam(Team team)
        {
            await SetUpDb();
            return await _dbConn.InsertAsync(team);
        }

        public async Task<int> DeleteTeam(Team team)
        {
            await SetUpDb();
            return await _dbConn.DeleteAsync(team);
        }

        public async Task<List<Team>> GetTeamList()
        {
            await SetUpDb();
            return await _dbConn.Table<Team>().ToListAsync();
        }

        public async Task<int> UpdateTeam(Team team)
        {
            await SetUpDb();
            return await _dbConn.UpdateAsync(team);
        }
    }
}
