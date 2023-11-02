using SQLite;
using MauiApp1.Models;

namespace MauiApp1.Data
{
    public class Database
    {
        public SQLiteAsyncConnection _database;

		public Database()
		{
			Init().Wait(); 
		}

		async Task Init()
        {
            if (_database is not null)
                return;

            _database = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            intitialiseTables();

		}


        private void intitialiseTables()
        {
            _database.CreateTableAsync<Skill>();
			_database.CreateTableAsync<AlertType>();
			_database.CreateTableAsync<BuildingType>();
			_database.CreateTableAsync<Continent>();
			_database.CreateTableAsync<Equipment>();
			_database.CreateTableAsync<OperationalTeamStatus>();
			_database.CreateTableAsync<OrderStatus>();
			_database.CreateTableAsync<Organisation>();
			_database.CreateTableAsync<position_statuses>();
			_database.CreateTableAsync<Role>();
			_database.CreateTableAsync<RoomType>();
			_database.CreateTableAsync<Rota>();
			_database.CreateTableAsync<SystemType>();
			_database.CreateTableAsync<SkillRequest>();
		}
    }
}
