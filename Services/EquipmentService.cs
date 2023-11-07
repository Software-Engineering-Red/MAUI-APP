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
       EquipmentService extending IEQuipmentService Interface
   </summary> */
    public class EquipmentService : IEquipmentService
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
            if (_dbConnection != null)
                return;


            _dbConnection = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConnection.CreateTableAsync<Equipment>();
        }

        /*! <summary>
            Method responsible for addtion of entry into Equipment table.
        </summary> 
         <param name="org">Equipment class instance that is attempted to be inserted into Equipment table.</param>
         <returns>Returns Task containing number of rows inserted into Equipment table.</returns> */
        public async Task<int> AddEquipment(Equipment equipment)
        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(equipment);
        }
        /*! <summary>
 Method responsible for deletion of an entry from Equipment table.
</summary> 
 <param name="org">Equipment class instance, that is attempted to be deleted from Equipment table. (Based on its' primary key)</param>
 <returns>Returns Task containing number of rows deleted from Equipment table.</returns>*/
        public async Task<int> DeleteEquipment(Equipment equipment)
        {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(equipment);
        }
        /*! <summary>
  Method responsible for quering database, for entries in Equipment table
 </summary> 
  <returns>Returns Task containing List of Equipment present in the database.</returns>*/
        public async Task<List<Equipment>> GetEquipmentList()
        {
            await SetUpDb();
            return await _dbConnection.Table<Equipment>().ToListAsync();
        }
        /*! <summary>
 Method responsible for updating an entry from Equipment table, based on primary key of the element passed.
</summary> 
 <param name="org">Equipment class instance that will be attempted to be updated, based on its' primary key value</param>
 <returns>Returns Task containing number of rows updated in Equipment table.</returns>*/
        public async Task<int> UpdateEquipment(Equipment equipment)
        {
            await SetUpDb();
            return await _dbConnection.UpdateAsync(equipment);
        }
    }
}