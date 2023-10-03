using MauiApp1.Models;

namespace MauiApp1.Services
{
    /*! <summary>
        Interface that exposes methods of OrganisationService
    </summary> */
    public interface IOrganisationService
    {
        /*! <summary>
         Method responsible for quering database, for entries in Organisation table
        </summary> 
         <returns>Returns Task containing List of Organisations present in the database.</returns>*/
        Task<List<Organisation>> GetOrganisationList();

        /*! <summary>
            Method responsible for addtion of entry into Organisation table.
        </summary> 
         <param name="org">Organisation class instance that is attempted to be inserted into Organisation table.</param>
         <returns>Returns Task containing number of rows inserted into Organisation table.</returns> */
        Task<int> AddOrganisation(Organisation org);

        /*! <summary>
         Method responsible for deletion of an entry from Organisation table.
        </summary> 
         <param name="org">Organisation class instance, that is attempted to be deleted from Organisation table. (Based on its' primary key)</param>
         <returns>Returns Task containing number of rows deleted from Organisation table.</returns>*/
        Task<int> DeleteOrganisation(Organisation org);

        /*! <summary>
         Method responsible for updating an entry from Organisation table, based on primary key of the element passed.
        </summary> 
         <param name="org">Organisation class instance that will be attempted to be updated, based on its' primary key value</param>
         <returns>Returns Task containing number of rows updated in Organisation table.</returns>*/
        Task<int> UpdateOrganisation(Organisation org);
    }
}
