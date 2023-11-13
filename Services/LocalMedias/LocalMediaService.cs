using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UndacApp.Data;
using UndacApp.Models;
using SQLite;

namespace UndacApp.Services.LocalMedias
{
    public class LocalMediaService : ILocalMediaService
    {  /*! <summary>
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
            await _dbConnection.CreateTableAsync<LocalMedia>();
        }
        /*! <summary>
         * Method responsible for addition of entry into _dbConnection table.
         * </summary>
         * <param name="localMedia">LocalMedia class instance is attempted to be inserted into _dbConnection table.</param>
         */
        public async Task<int> AddLocalMedia(LocalMedia localMedia)
        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(localMedia);
        }
        /*! <summary>
         * Method responsible for deletion of an entry from _dbConnection table.
         * </summary>
         * <param name="localMedia">LocalMedia class instance,  is attempted to be deleted from _dbConnection table. (Based on its' primary key)</param>
         */
        public async Task<int> DeleteLocalMedia(LocalMedia localMedia)
        {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(localMedia);
        }
        /*! <summary>
         * Method responsible for quering database, for entries in _dbConnection table
         * </summary> 
         */
        public async Task<List<LocalMedia>> GetLocalMediaList()
        {
            await SetUpDb();
            return await _dbConnection.Table<LocalMedia>().ToListAsync();
        }
        /*! <summary>
         * Method responsible for updating an entry from _dbConnection table, based on primary key of the element passed.
         * </summary> 
         * <param name="localMedia">LocalMedia class instance which will attempt to be updated, based on its' primary key value</param>
         */
        public async Task<int> UpdateLocalMedia(LocalMedia systemType)
        {
            await SetUpDb();
            return await _dbConnection.UpdateAsync(systemType);
        }
    }
}
