using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using UndacApp.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UndacApp.Services
{
        public class ResourceRequestService
        {
            readonly SQLiteAsyncConnection _database;

            public ResourceRequestService(string dbPath)
            {
                _database = new SQLiteAsyncConnection(dbPath);
                _database.CreateTableAsync<ErrorResourcePair>().Wait();
            }

            public Task<List<ErrorResourcePair>> GetErrorsAsync()
            {
                return _database.Table<ErrorResourcePair>().ToListAsync();
            }

            public Task<int> SaveErrorAsync(ErrorResourcePair errorItem)
            {
                if (errorItem.Id != 0)
                {
                    return _database.UpdateAsync(errorItem);
                }
                else
                {
                    return _database.InsertAsync(errorItem);
                }
            }
        }
    }

