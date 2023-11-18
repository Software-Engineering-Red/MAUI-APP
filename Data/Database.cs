using SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndacApp.Data
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

                // Create your database tables here
                await _database.CreateTableAsync<Models.AlertType>();
                await _database.CreateTableAsync<Models.OperationalTeamStatus>();
                await _database.CreateTableAsync<Models.OperationalTeam>();
                await _database.CreateTableAsync<Models.BuildingType>();
                await _database.CreateTableAsync<Models.Continent>();
                await _database.CreateTableAsync<Models.Equipment>();
                await _database.CreateTableAsync<Models.OrderStatus>();
                await _database.CreateTableAsync<Models.Organisation>();
                await _database.CreateTableAsync<Models.position_statuses>();
                await _database.CreateTableAsync<Models.Role>();
                await _database.CreateTableAsync<Models.RoomType>();
                await _database.CreateTableAsync<Models.Rota>();
                await _database.CreateTableAsync<Models.Skill>();
                await _database.CreateTableAsync<Models.SystemType>();
                await _database.CreateTableAsync<Models.TeamMember>();
                await _database.CreateTableAsync<Models.PrivilegeRequest>();
                await _database.CreateTableAsync<Models.ResourceType>();
                await _database.CreateTableAsync<Models.Resource>();
                await _database.CreateTableAsync<Models.LocalMedia>();
            // Try to create the tables based on the specified model types.
            try
            {
                // Create your database tables here
                await _database.CreateTableAsync<Models.AlertType>();
                await _database.CreateTableAsync<Models.OperationalTeamStatus>();
                await _database.CreateTableAsync<Models.BuildingType>();
                await _database.CreateTableAsync<Models.Continent>();
                await _database.CreateTableAsync<Models.Equipment>();
                await _database.CreateTableAsync<Models.OrderStatus>();
                await _database.CreateTableAsync<Models.Organisation>();
                await _database.CreateTableAsync<Models.position_statuses>();
                await _database.CreateTableAsync<Models.Role>();
                await _database.CreateTableAsync<Models.RoomType>();
                await _database.CreateTableAsync<Models.Rota>();
                await _database.CreateTableAsync<Models.Skill>();
                await _database.CreateTableAsync<Models.SystemType>();
                
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
