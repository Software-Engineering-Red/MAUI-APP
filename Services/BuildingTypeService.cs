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
    public class BuildingTypeService : IBuildingTypeService {

        private SQLiteAsyncConnection _dbConnection;

        private async Task SetUpDb() {
            if (_dbConnection != null)
                return;
            

            _dbConnection = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConnection.CreateTableAsync<BuildingType>();
        }

        public async Task<int> AddBuildingType(BuildingType buildingType) {
            await SetUpDb();
            return await _dbConnection.InsertAsync(buildingType);
        }

        public async Task<int> DeleteBuildingType(BuildingType buildingType) {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(buildingType);
        }

        public async Task<List<BuildingType>> GetBuildingTypeList() {
            await SetUpDb();
            return await _dbConnection.Table<BuildingType>().ToListAsync();
        }

        public async Task<int> UpdateBuildingType(BuildingType buildingType) {
            await SetUpDb();
            return await _dbConnection.UpdateAsync(buildingType);
        }
    }
}
