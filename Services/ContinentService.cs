using MauiApp1.Data;
using MauiApp1.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public class ContinentService : IContinentService {

        private SQLiteAsyncConnection _dbConnection;

        private async Task SetUpDb() {
            if (_dbConnection != null)
                return;
            

            _dbConnection = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConnection.CreateTableAsync<Continent>();
        }

        public async Task<int> AddContinent(Continent continent) {
            await SetUpDb();
             await _dbConnection.InsertAsync(continent);
            return await _dbConnection.Table<Continent>().ToListAsync();
        }

        public async Task<int> DeleteContinent(Continent continent) {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(continent);
        }

        public async Task<List<Continent>> GetContinentList() {
            await SetUpDb();
            return await _dbConnection.Table<Continent>().ToListAsync();
        }

        public async Task<int> UpdateContinent(Continent continent) {
            await SetUpDb();
            return await _dbConnection.UpdateAsync(continent);
        }
    }
}
