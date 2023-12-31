﻿using UndacApp.Data;
using UndacApp.Models;
using Microsoft.Maui.Animations;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndacApp.Services
{
    public class AlertTypeService : IAlertTypeService
    {

        private SQLiteAsyncConnection _dbConnection;

        private async Task SetUpDb()
        {
            if (_dbConnection != null)
                return;


            _dbConnection = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConnection.CreateTableAsync<AlertType>();
        }

        public async Task<int> AddAlertType(AlertType alert)
        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(alert);
        }

        public async Task<int> DeleteAlertType(AlertType alert)
        {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(alert);
        }
        public async Task<int> DeleteAllAlertType()
        {
            await SetUpDb(); 

            return await _dbConnection.DeleteAllAsync<AlertType>();
        }

        public async Task<List<AlertType>> GetAlertTypesByStatus(string status)
        {
            await SetUpDb();
            return await _dbConnection.Table<AlertType>().Where(a => a.Status == status).ToListAsync();
        }

        public async Task<List<AlertType>> GetAlertTypes()
        {
            await SetUpDb();
            return await _dbConnection.Table<AlertType>().ToListAsync();
        }

        public async Task<int> UpdateAlertType(AlertType alert)
        {
            await SetUpDb();
            return await _dbConnection.UpdateAsync(alert);
        }

        
    }
}
