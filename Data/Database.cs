using SQLite;

namespace UndacApp.Data
{
    public class Database
    {
        public SQLiteAsyncConnection _database;
        public Database()
        {
            InitializeDatabaseAsync().Wait(); // Ensure initialization is complete before proceeding
        }

        public async Task InitializeDatabaseAsync()
        {
            if (_database is not null)
                return;

            try
            {
                _database = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
                await CreateTablesAsync();
            }
            catch (Exception ex)
            {
                // Handle any initialization errors here, e.g., log or display an error message.
                Console.WriteLine($"Database initialization error: {ex.Message}");
            }
        }
        public async Task CreateTablesAsync()
        {
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
                await _database.CreateTableAsync<Models.TeamMember>();
                await _database.CreateTableAsync<Models.PrivilegeRequest>();

            }
            catch (Exception ex)
            {
                // Handle any table creation errors here, e.g., log or display an error message.
                Console.WriteLine($"Table creation error: {ex.Message}");
            }
        }
    }
}
