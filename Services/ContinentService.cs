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
    /*! <summary>
        ContinentService extending IContinentService Interface
    </summary> */
    public class ContinentService : IContinentService
    {
        /*! <summary>
            Variable storing dbConnection to SQLite database.
        </summary> */
        private SQLiteAsyncConnection _dbConnection;

        /*! <summary>
            Method that initiates connection to SQLite database, and creates Continent class table, if none is present.
        </summary> */
        private async Task SetUpDb() {
            if (_dbConnection != null)
                return;
            

            _dbConnection = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConnection.CreateTableAsync<Continent>();
        }

        /*! <summary>
            Method responsible for addtion of entry into Continent table.
        </summary> 
         <param name="org">Continent class instance that is attempted to be inserted into Continent table.</param>
         <returns>Returns Task containing number of rows inserted into Continent table.</returns> */
        public async Task<int> AddContinent(Continent continent) {
            await SetUpDb();
            return await _dbConnection.InsertAsync(continent);
             
        }

        /*! <summary>
         Method responsible for deletion of an entry from Continent table.
        </summary> 
         <param name="org">Continent class instance, that is attempted to be deleted from Continent table. (Based on its' primary key)</param>
         <returns>Returns Task containing number of rows deleted from Continent table.</returns>*/
        public async Task<int> DeleteContinent(Continent continent) {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(continent);
        }

        /*! <summary>
         Method responsible for quering database, for entries in Continent table
        </summary> 
         <returns>Returns Task containing List of Continents present in the database.</returns>*/
        public async Task<List<Continent>> GetContinentList() {
            await SetUpDb();
            return await _dbConnection.Table<Continent>().ToListAsync();
        }

        /*! <summary>
         Method responsible for updating an entry from Continent table, based on primary key of the element passed.
        </summary> 
         <param name="org">Continent class instance that will be attempted to be updated, based on its' primary key value</param>
         <returns>Returns Task containing number of rows updated in Continent table.</returns>*/
        public async Task<int> UpdateContinent(Continent continent) {
            await SetUpDb();
            return await _dbConnection.UpdateAsync(continent);
        }
    }
}
