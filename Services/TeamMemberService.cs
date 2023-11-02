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
    public class TeamMemberService : ITeamMemberService
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
            await _dbConnection.CreateTableAsync<TeamMember>();
        }

        /*! <summary>
            Method responsible for addtion of entry into TeamMember table.
        </summary> 
         <param name="org">TeamMember class instance that is attempted to be inserted into TeamMember table.</param>
         <returns>Returns Task containing number of rows inserted into TeamMember table.</returns> */
        public async Task<int> AddTeamMember(TeamMember team_member)
        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(team_member);

        }

        /*! <summary>
         Method responsible for deletion of an entry from TeamMember table.
        </summary> 
         <param name="org">TeamMember class instance, that is attempted to be deleted from TeamMember table. (Based on its' primary key)</param>
         <returns>Returns Task containing number of rows deleted from TeamMember table.</returns>*/
        public async Task<int> DeleteTeamMember(TeamMember team_member)
        {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(team_member);
        }

        /*! <summary>
         Method responsible for quering database, for entries in TeamMember table
        </summary> 
         <returns>Returns Task containing List of TeamMembers present in the database.</returns>*/
        public async Task<List<TeamMember>> GetTeamMemberList()
        {
            await SetUpDb();
            return await _dbConnection.Table<TeamMember>().ToListAsync();
        }

        public async Task<TeamMember> GetTeamMemberById(int memberId)
        {
            await SetUpDb();
            return await _dbConnection.FindAsync<TeamMember>(memberId);
        }


        /*! <summary>
         Method responsible for updating an entry from TeamMember table, based on primary key of the element passed.
        </summary> 
         <param name="org">TeamMember class instance that will be attempted to be updated, based on its' primary key value</param>
         <returns>Returns Task containing number of rows updated in TeamMember table.</returns>*/
        public async Task<int> UpdateTeamMember(TeamMember team_member)
        {
            await SetUpDb();
            return await _dbConnection.UpdateAsync(team_member);
        }
    }
}
