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
    /// <summary>
    /// Represents the RotaService class, which provides CRUD operations for Rota objects.
    /// </summary>
    public class RotaService : IRotaService
    {
        private SQLiteAsyncConnection _dbConnection;

        /// <summary>
        /// Sets up the SQLite database connection and creates the Rota table if it doesn't exist.
        /// </summary>
        private async Task SetUpDb()
        {
            if (_dbConnection != null)
                return;

            _dbConnection = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConnection.CreateTableAsync<Rota>();
        }

        /// <summary>
        /// Adds a new Rota to the database.
        /// </summary>
        /// <param name="rota">The Rota object to be added.</param>
        /// <returns>The number of rows affected (should be 1 if successful).</returns>
        public async Task<int> AddRota(Rota rota)
        {
            try
            {
                await SetUpDb();
                return await _dbConnection.InsertAsync(rota);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database or insert failure: " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Deletes a Rota from the database.
        /// </summary>
        /// <param name="rota">The Rota object to be deleted.</param>
        /// <returns>The number of rows affected (should be 1 if successful).</returns>
        public async Task<int> DeleteRota(Rota rota)
        {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(rota);
        }

        /// <summary>
        /// Retrieves a list of all Rotas from the database.
        /// </summary>
        /// <returns>A list of Rota objects.</returns>
        public async Task<List<Rota>> GetRotaList()
        {
            await SetUpDb();
            return await _dbConnection.Table<Rota>().ToListAsync();
        }

        /// <summary>
        /// Updates an existing Rota in the database.
        /// </summary>
        /// <param name="rota">The Rota object to be updated.</param>
        /// <returns>The number of rows affected (should be 1 if successful).</returns>
        public async Task<int> UpdateRota(Rota rota)
        {
            await SetUpDb();
            return await _dbConnection.UpdateAsync(rota);
        }
    }
}
