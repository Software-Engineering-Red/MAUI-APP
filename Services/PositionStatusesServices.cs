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
    /// <summary>
    /// this class deals with setting up the database ready to obtain and delete data from the application and impliments an interface.
    /// </summary>
    internal class PositionStatusesServices : IPositionStatusesServices
    {
        private SQLiteAsyncConnection _dbConnection;
        
        /*this task function sets up the database and check whether the database is already set up or not*/

        /*if there is a connection already then the function will stop*/

        /*otherwise the the fucntion will set up a new connection by getting the DatabaseSettings.cs
         * to get the local database path and retreiveing the flags that define what each of the task function in this class does.*/
        /*then awaits the database connection to be set up before the adding the position status as a table*/
        private async Task SetUpDb()
        {
            if (_dbConnection != null) 
            {
                return;
            }

            _dbConnection = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
                await _dbConnection.CreateTableAsync<position_statuses>();
        }
        /*this task function is to await for the databse to be set up after waiting to add the object to the database*/
        /*as well as increase the incremental intiger for primary key if it is auto incremental(which it is) then returns the result */
        public async Task<int> AddStatus(position_statuses status)
        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(status);
        }

        /*this task function is to await the for the databas to be set up after wating delete the object that is in the database */
        /*while using the index of the object then returns the result*/
        public async Task<int> DeleteStatus(position_statuses status)
        {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(status);
        }

        /*this task function is to await the database to be set up after wating creates a list from the database table*/
        /*then returns the result*/
        public async Task<List<position_statuses>> GetPosition_StatusesList()
        {
            await SetUpDb();
            return await _dbConnection.Table<position_statuses>().ToListAsync();
        }

        /*this task function is to await the database to be set up after update the position status to the database*/
        /*while useing the primary key then return the results*/
        public async Task<int> UpdateStatus(position_statuses status)
        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(status);
        }

    }
}
