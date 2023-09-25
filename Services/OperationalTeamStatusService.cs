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
    public class OperationalTeamStatusService : IOperationalTeamStatusService
    {
        SQLiteAsyncConnection _dbConnection;

        public OperationalTeamStatusService()
        {
        }

        async Task Init()
        {
            if (_dbConnection is not null)
                return;

            _dbConnection = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConnection.CreateTableAsync<OperationalTeamStatus>();
        }

        public async Task<int> AddStatus(OperationalTeamStatus status)
        {
            await Init();
            return await _dbConnection.InsertAsync(status);
        }

        public async Task<int> DeleteStatusAsync(OperationalTeamStatus status)
        {
            await Init();
            return _dbConnection.DeleteAsync(status);
        }
        public async Task<List<OperationalTeamStatus>> GetStatusesListAsync()
        {
            await Init();
            return await _dbConnection.Table<OperationalTeamStatus>().ToListAsync();
        }

        public async Task<OperationalTeamStatus> GetStatusAsync(string name)
        {
            await Init();
            return await _dbConnection.Table<OperationalTeamStatus>().Where(i => i.Name == name).FirstOrDefaultAsync();
        }

        public async Task<int> SaveStatusAsync(OperationalTeamStatus status)
        {
            await Init();
            if (status.Name != null)
                return await _dbConnection.UpdateAsync(status);
            else
                return await _dbConnection.InsertAsync(status);
        }

        public async Task<int> UpdateStatusAsync(OperationalTeamStatus status)
        {
            await Init();
            return await _dbConnection.UpdateAsync(status);
        }

  
    }
}
