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
    public class TeamMemberService : ITeamMemberService
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

        public async Task<int> AddTeamMember(TeamMember teamMember)
        {
            await SetUpDb();
            return await _dbConn.InsertAsync(teamMember);
        }

        public async Task<int> DeleteTeamMember(TeamMember teamMember)
        {
            await SetUpDb();
            return await _dbConn.DeleteAsync(teamMember);
        }

        public async Task<List<TeamMember>> GetTeamMemberList()
        {
            await SetUpDb();
            return await _dbConn.Table<TeamMember>().ToListAsync();
        }

        public async Task<int> UpdateTeamMember(TeamMember teamMember)
        {
            await SetUpDb();
            return await _dbConn.UpdateAsync(teamMember);
        }
    }
}
