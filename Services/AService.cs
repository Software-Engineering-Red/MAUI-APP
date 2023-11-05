using UndacApp.Data;
using UndacApp.Models;
using SQLite;

namespace UndacApp.Services
{
    public abstract class AService<T> : IService<T> where T : AModel,new()
    {
        private SQLiteAsyncConnection _database;

        public AService()
        {
            _database = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            _database.CreateTableAsync<T>().Wait();
        }

        public async Task<T> GetOne(int id)
        {
            return await _database.Table<T>().Where(t => t.ID == id).FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAll()
        {
            return await _database.Table<T>().ToListAsync();
        }
        public async Task<int> Add(T entity)
        {
            return await _database.InsertAsync(entity);
        }
        public async Task<int> Update(T entity)
        {
            return await _database.UpdateAsync(entity);
        }
        public async Task<int> Remove(T entity)
        {
            return await _database.DeleteAsync(entity);
        }
        public async Task<bool> Exists(int id)
        {
            return await GetOne(id) != null;
        }
    }
}
