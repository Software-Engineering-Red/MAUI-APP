﻿using MauiApp1.Data;
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
            await _dbConnection.CreateTableAsync<BuildingType>();
        }

        public async Task<int> AddContinent(BuildingType continent) {
            await SetUpDb();
            return await _dbConnection.InsertAsync(continent);
        }

        public async Task<int> DeleteContinent(BuildingType continent) {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(continent);
        }

        public async Task<List<BuildingType>> GetContinentList() {
            await SetUpDb();
            return await _dbConnection.Table<BuildingType>().ToListAsync();
        }

        public async Task<int> UpdateContinent(BuildingType continent) {
            await SetUpDb();
            return await _dbConnection.UpdateAsync(continent);
        }
    }
}
