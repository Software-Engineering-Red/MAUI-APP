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
    public class EquipmentService : IEquipmentService
    {

        private SQLiteAsyncConnection _dbConnection;

        private async Task SetUpDb()
        {
            if (_dbConnection != null)
                return;


            _dbConnection = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConnection.CreateTableAsync<Equipment>();
        }

        public async Task<int> AddEquipment(Equipment equipment)
        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(equipment);
        }

        public async Task<int> DeleteEquipment(Equipment equipment)
        {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(equipment);
        }

        public async Task<List<Equipment>> GetEquipmentList()
        {
            await SetUpDb();
            return await _dbConnection.Table<Equipment>().ToListAsync();
        }

        public async Task<int> UpdateEquipment(Equipment equipment)
        {
            await SetUpDb();
            return await _dbConnection.UpdateAsync(equipment);
        }
    }
}