using UndacApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndacApp.Services
{
    public interface IPrivilegeRequestService
    {
        /*! <summary>
         Method responsible for quering database, for entries in TeamMember table
        </summary> 
         <returns>Returns Task containing List of TeamMembers present in the database.</returns>*/
        Task<List<PrivilegeRequest>> GetPrivilegeRequestList();

        /*! <summary>
         Method responsible for updating an entry from TeamMember table, based on primary key of the element passed.
        </summary> 
         <param name="org">TeamMember class instance that will be attempted to be updated, based on its' primary key value</param>
         <returns>Returns Task containing number of rows updated in TeamMember table.</returns>*/
        Task<int> UpdatePrivilegeRequest(PrivilegeRequest request);

        Task<int> AddRequest(PrivilegeRequest request);

        Task<int> DeleteRequest(PrivilegeRequest request);
    }
}
