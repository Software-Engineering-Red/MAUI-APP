using MauiApp1.Data;
using MauiApp1.Models;
using System;
using System.Collections.Generic;
using SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    /// <summary>
    /// This class extending IOperationalTeamStatusService 
    /// </summary>
    public class OperationalTeamStatusService : IOperationalTeamStatusService
    {
        /// <summary>
        /// Stores SQLite Database Connection
        /// </summary>
        SQLiteAsyncConnection _dbConnection;

        /// <summary>
        /// Constructor without implementation
        /// </summary>
        public OperationalTeamStatusService(){}

        /// <summary>
        /// Initiates Database Connection and creates Table.
        /// </summary>
        /// <returns></returns>
        async Task Init()
        {
            if (_dbConnection is not null)
                return;

            _dbConnection = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConnection.CreateTableAsync<OperationalTeamStatus>();
        }

        /// <summary>
        /// Adds a instance of OperationalTeamStatus to the Database.
        /// </summary>
        /// <param name="status">Instance of OperationalTeamStatus to be added</param>
        /// <returns>Task promise to insert instance to Database</returns>
        public async Task<int> AddStatus(OperationalTeamStatus status)
        {
            await Init();
            return await _dbConnection.InsertAsync(status);
        }

        /// <summary>
        /// Deletes a certain instance of OperationalTeamStatus from the Database.
        /// </summary>
        /// <param name="status">Instance of OperationalTeamStatus to be deleted</param>
        /// <returns>Task promise to delete Instance from Database</returns>
        public async Task<int> DeleteStatusAsync(OperationalTeamStatus status)
        {
            await Init();
            return await _dbConnection.DeleteAsync(status);
        }

        /// <summary>
        /// Gets a List of OperationalTeamStatus from Database.
        /// </summary>
        /// <returns>Task promise to get a List of OperationalTeamStatus</returns>
        public async Task<List<OperationalTeamStatus>> GetStatesListAsync()
        {
            await Init();
            return await _dbConnection.Table<OperationalTeamStatus>().ToListAsync();
        }

        /// <summary>
        /// Updates an entry of OperationalTeamStatus in Database.
        /// </summary>
        /// <param name="status">entry of OperationalTeamStatus</param>
        /// <returns>Task promise to update certain Status</returns>
        public async Task<int> UpdateStatusAsync(OperationalTeamStatus status)
        {
            await Init();
            return await _dbConnection.UpdateAsync(status);
        }
    }
}
