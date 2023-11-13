using UndacApp.Data;
using UndacApp.Models;
using SQLite;

namespace UndacApp.Services.NameDiscriminator
{
	public abstract class NameDiscriminatorService<T> : INameDiscriminatorService<T> where T : ANameModel, new()
	{
		private SQLiteAsyncConnection _database;

		public NameDiscriminatorService()
		{
			_database = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
			_database.CreateTableAsync<T>().Wait();
		}

		public async Task<int> Add(T entity)
		{
			if (!await Exists(entity.Name))
			{
				return await _database.InsertAsync(entity);
			}
			return 1; 
		}

		public async Task<bool> Exists(string name)
		{
			return await GetOne(name) != null;
		}

		public async Task<List<T>> GetAll()
		{
			return await _database.Table<T>().ToListAsync();
		}

		public async Task<T> GetOne(string name)
		{
			return await _database.Table<T>().Where(t => t.Name == name).FirstOrDefaultAsync();
		}

		public async Task<int> Remove(T entity)
		{
			return await _database.DeleteAsync(entity);
		}

		public async Task<int>  Update(T entity)
		{
			return await _database.UpdateAsync(entity);
		}
	}
}
