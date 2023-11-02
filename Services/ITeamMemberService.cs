using UndacApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndacApp.Services
{
    public interface ITeamMemberService
    {
        /*! <summary>
         Method responsible for quering database, for entries in TeamMember table
        </summary> 
         <returns>Returns Task containing List of TeamMembers present in the database.</returns>*/
        Task<List<TeamMember>> GetTeamMemberList();

        Task<TeamMember> GetTeamMemberById(int memberId);

        /*! <summary>
            Method responsible for addtion of entry into TeamMember table.
        </summary> 
         <param name="org">TeamMember class instance that is attempted to be inserted into TeamMember table.</param>
         <returns>Returns Task containing number of rows inserted into TeamMember table.</returns> */
        Task<int> AddTeamMember(TeamMember team_member);

        /*! <summary>
         Method responsible for deletion of an entry from TeamMember table.
        </summary> 
         <param name="org">TeamMember class instance, that is attempted to be deleted from TeamMember table. (Based on its' primary key)</param>
         <returns>Returns Task containing number of rows deleted from TeamMember table.</returns>*/
        Task<int> DeleteTeamMember(TeamMember team_member);

        /*! <summary>
         Method responsible for updating an entry from TeamMember table, based on primary key of the element passed.
        </summary> 
         <param name="org">TeamMember class instance that will be attempted to be updated, based on its' primary key value</param>
         <returns>Returns Task containing number of rows updated in TeamMember table.</returns>*/
        Task<int> UpdateTeamMember(TeamMember team_member);
    }
}
