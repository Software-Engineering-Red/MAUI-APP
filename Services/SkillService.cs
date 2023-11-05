using UndacApp.Data;
using UndacApp.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UndacApp.Services
{
    /// <summary>
    /// Service for managing skills using SQLite database.
    /// </summary>
    public class SkillService : ISkillService
    {
        private SQLiteAsyncConnection _dbConnection;

        /// <summary>
        /// Sets up the database connection and creates the Skill table if it doesn't exist.
        /// </summary>
        private async Task SetUpDb()
        {
            if (_dbConnection != null)
                return;

            _dbConnection = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConnection.CreateTableAsync<Skill>();
        }

        /// <summary>
        /// Adds a new skill to the database.
        /// </summary>
        /// <param name="skill">The skill to add.</param>
        /// <returns>The number of rows affected.</returns>
        public async Task<int> AddSkill(Skill skill)
        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(skill);
        }

        /// <summary>
        /// Deletes a skill from the database.
        /// </summary>
        /// <param name="skill">The skill to delete.</param>
        /// <returns>The number of rows affected.</returns>
        public async Task<int> DeleteSkill(Skill skill)
        {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(skill);
        }

        /// <summary>
        /// Retrieves a list of all skills from the database.
        /// </summary>
        /// <returns>A list of skills.</returns>
        public async Task<List<Skill>> GetSkillList()
        {
            await SetUpDb();
            return await _dbConnection.Table<Skill>().ToListAsync();
        }

        /// <summary>
        /// Updates an existing skill in the database.
        /// </summary>
        /// <param name="skill">The skill to update.</param>
        /// <returns>The number of rows affected.</returns>
        public async Task<int> UpdateSkill(Skill skill)
        {
            await SetUpDb();
            return await _dbConnection.UpdateAsync(skill);
        }
    }
}
