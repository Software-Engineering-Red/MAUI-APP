using UndacApp.Data;
using UndacApp.Models;
using SQLite;

namespace UndacApp.Services
{
    /*! <summary>
        OrganisationService extending IOrganisationService Interface
    </summary> */
    public class OrganisationService : IOrganisationService
    {
        /*! <summary>
            Variable storing dbConnection to SQLite database.
        </summary> */
        private SQLiteAsyncConnection _dbConnection;

        /*! <summary>
            Method that initiates connection to SQLite database, and creates Organisation class table, if none is present.
        </summary> */
        private async Task SetUpDb()
        {
            if (_dbConnection != null) return;

            _dbConnection = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConnection.CreateTableAsync<Organisation>();
        }

        /*! <summary>
            Method responsible for addtion of entry into Organisation table.
        </summary> 
         <param name="org">Organisation class instance that is attempted to be inserted into Organisation table.</param>
         <returns>Returns Task containing number of rows inserted into Organisation table.</returns> */
        public async Task<int> AddOrganisation(Organisation org)
        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(org);
        }

        /*! <summary>
         Method responsible for deletion of an entry from Organisation table.
        </summary> 
         <param name="org">Organisation class instance, that is attempted to be deleted from Organisation table. (Based on its' primary key)</param>
         <returns>Returns Task containing number of rows deleted from Organisation table.</returns>*/
        public async Task<int> DeleteOrganisation(Organisation org)
        {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(org);
        }

        /*! <summary>
         Method responsible for quering database, for entries in Organisation table
        </summary> 
         <returns>Returns Task containing List of Organisations present in the database.</returns>*/
        public async Task<List<Organisation>> GetOrganisationList()
        {
            await SetUpDb();
            return await _dbConnection.Table<Organisation>().ToListAsync();
        }

        /*! <summary>
         Method responsible for updating an entry from Organisation table, based on primary key of the element passed.
        </summary> 
         <param name="org">Organisation class instance that will be attempted to be updated, based on its' primary key value</param>
         <returns>Returns Task containing number of rows updated in Organisation table.</returns>*/
        public async Task<int> UpdateOrganisation(Organisation org)
        {
            await SetUpDb();
            return await _dbConnection.UpdateAsync(org);
        }
    }
}
