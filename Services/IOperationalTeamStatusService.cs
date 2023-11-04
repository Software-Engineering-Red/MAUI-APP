using UndacApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndacApp.Services
{
    /// <summary>
    /// Interface exposing the methods for CRUD-Interface of OperationalTeamStatusService.
    /// </summary>
    internal interface IOperationalTeamStatusService
    {
        /// <summary>
        /// Adds a instance of OperationalTeamStatus to the Database.
        /// </summary>
        /// <param name="status">Instance of OperationalTeamStatus to be added</param>
        /// <returns>Task promise to insert instance to Database</returns>
        Task<int> AddStatus(OperationalTeamStatus status);

        /// <summary>
        /// Deletes a certain instance of OperationalTeamStatus from the Database.
        /// </summary>
        /// <param name="status">Instance of OperationalTeamStatus to be deleted</param>
        /// <returns>Task promise to delete Instance from Database</returns>
        Task<int> DeleteStatusAsync(OperationalTeamStatus status);

        /// <summary>
        /// Gets a List of OperationalTeamStatus from Database.
        /// </summary>
        /// <returns>Task promise to get a List of OperationalTeamStatus</returns>
        Task<List<OperationalTeamStatus>> GetStatesListAsync();


        /// <summary>
        /// Updates an entry of OperationalTeamStatus in Database.
        /// </summary>
        /// <param name="status">entry of OperationalTeamStatus</param>
        /// <returns>Task promise to update certain Status</returns>
        Task<int> UpdateStatusAsync(OperationalTeamStatus status);

    }
}
