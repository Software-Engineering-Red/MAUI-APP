using System;
using System.Threading.Tasks;
using SQLite;

namespace MauiApp1.Data
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _database;
        private bool _isInitialised;

        public Database(string dbPath)
        {
            // Confirm that the database path is neither null nor empty as this is a critical requirement.
            if (string.IsNullOrEmpty(dbPath))
            {
                throw new ArgumentException("The database path must not be null or empty.", nameof(dbPath));
            }

            // Set up the SQLite connection using the given path.
            _database = new SQLiteAsyncConnection(dbPath);
        }

        public async Task InitialiseAsync()
        {
            // If the database has already been set up, there's no need to repeat the process.
            if (_isInitialised)
            {
                return;
            }

            // Proceed to create the database tables asynchronously, avoiding the blocking of the calling thread.
            await CreateTablesAsync();
            _isInitialised = true; // Indicate that initialisation has completed to prevent it from running again.
        }

        private async Task CreateTablesAsync()
        {
            // Try to create the tables based on the specified model types.
            try
            {
                await _database.CreateTablesAsync(CreateFlags.None, new[] {
                    typeof(Models.AlertType),
                    typeof(Models.OperationalTeamStatus),
                    typeof(Models.BuildingType),
                    typeof(Models.Continent),
                    typeof(Models.Equipment),
                    typeof(Models.OrderStatus),
                    typeof(Models.Organisation),
                    typeof(Models.position_statuses),
                    typeof(Models.Role),
                    typeof(Models.RoomType),
                    typeof(Models.Rota),
                    typeof(Models.Skill),
                    typeof(Models.SystemType),
                    typeof(Models.Expert)
                }).ConfigureAwait(false); // Using ConfigureAwait(false) to avoid capturing the synchronisation context and thus prevent deadlocks.
            }
            catch (Exception ex)
            {
                // Log the exception in a manner consistent with the application's logging strategy.
                Console.WriteLine($"An error occurred while creating the database tables: {ex.Message}");
                throw; // Re-throwing the exception to ensure the calling code can manage it effectively.
            }
        }
    }
}
