using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using UndacApp.Models;
using UndacApp.Data;

namespace UndacApp.Services
{
    internal class LogisticsService : ILogisticsService
    {
        private SQLiteAsyncConnection _dbConn;

        private async Task ConnectToDB()
        {
            if (_dbConn != null)
                return;

            _dbConn = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConn.CreateTableAsync<LogisticsOperation>();
        }
        public LogisticsService()
        {
            _dbConn = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
        }

        public async Task<List<LogisticsOperation>> GetAllOperations()
        {
            return await _dbConn.Table<LogisticsOperation>().ToListAsync();
        }

        public async Task<List<LogisticsOperation>> GetAllOperationsByVehicle(string value)
        {
            return await _dbConn.Table<LogisticsOperation>().Where(op => op.VehicleAssigned == value).ToListAsync();
        }

        public async Task<List<LogisticsOperation>> GetAllOperationsByEquip(string value)
        {
            return await _dbConn.Table<LogisticsOperation>().Where(op => op.EquipmentAssigned == value).ToListAsync();
        }

        public async Task<List<LogisticsOperation>> GetDateOperations(DateTime start, DateTime end)
        {
            return await _dbConn.Table<LogisticsOperation>()
                .Where(op => op.createdAt >= start && op.createdAt <= end)
                .ToListAsync();
        }
        public async Task<int> AddLogisticsOperation(LogisticsOperation operation)
        {
            await ConnectToDB();
            return await _dbConn.InsertAsync(operation);
        }
    }
}
