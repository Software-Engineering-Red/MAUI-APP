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
    /*! <summary>
     * SystemTypeService inhereting from ISystemTypeService Interface
     * </summary> 
     */
    public class SystemTypeService : ISystemTypeService
    {
        /*! <summary>
         * Variable storing dbConnection to SQLite database.
         * </summary> 
         */
        private SQLiteAsyncConnection _dbConnection;
        /*! <summary>
         * Method initiates connection to SQLite database, and creates _dbConnection table, if none is present.
        </summary> */
        private async Task SetUpDb()
        {
            if (_dbConnection != null)
                return;


            _dbConnection = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConnection.CreateTableAsync<SystemType>();
        }
        /*! <summary>
         * Method responsible for addition of entry into _dbConnection table.
         * </summary>
         * <param name="systemType">SystemType class instance is attempted to be inserted into _dbConnection table.</param>
         */
        public async Task<int> AddSystemType(SystemType systemType)
        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(systemType);
        }
        /*! <summary>
         * Method responsible for deletion of an entry from _dbConnection table.
         * </summary>
         * <param name="systemType">SystemType class instance,  is attempted to be deleted from _dbConnection table. (Based on its' primary key)</param>
         */
        public async Task<int> DeleteSystemType(SystemType systemType)
        {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(systemType);
        }
        /*! <summary>
         * Method responsible for quering database, for entries in _dbConnection table
         * </summary> 
         */
        public async Task<List<SystemType>> GetSystemTypeList()
        {
            await SetUpDb();
            return await _dbConnection.Table<SystemType>().ToListAsync();
        }
        /*! <summary>
         * Method responsible for updating an entry from _dbConnection table, based on primary key of the element passed.
         * </summary> 
         * <param name="systemType">SystemType class instance which will attempt to be updated, based on its' primary key value</param>
         */
        public async Task<int> UpdateSystemType(SystemType systemType)
        {
            await SetUpDb();
            return await _dbConnection.UpdateAsync(systemType);
        }
    }
}
