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
    public class RotaService : IRotaService
    {

        private SQLiteAsyncConnection _dbConnection;

        private async Task SetUpDb()
        {
            if (_dbConnection != null)
                return;


            _dbConnection = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConnection.CreateTableAsync<Rota>();
        }

        public async Task<int> AddRota(Rota rota)
        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(rota);
        }

        public async Task<int> DeleteRota(Rota rota)
        {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(rota);
        }

        public async Task<List<Rota>> GetRotaList()
        {
            await SetUpDb();
            return await _dbConnection.Table<Rota>().ToListAsync();
        }

        public async Task<int> UpdateRota(Rota rota)
        {
            await SetUpDb();
            return await _dbConnection.UpdateAsync(rota);
        }
    }
}