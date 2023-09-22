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
    public class RoleService : IRoleService
    {

        private SQLiteAsyncConnection _dbConnection;

        private async Task SetUpDb()
        {
            if (_dbConnection != null)
                return;


            _dbConnection = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConnection.CreateTableAsync<Role>();
        }

        public async Task<int> AddRole(Role role)
        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(role);
        }

        public async Task<int> DeleteRole(Role role)
        {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(role);
        }

        public async Task<List<Role>> GetRoleList()
        {
            await SetUpDb();
            return await _dbConnection.Table<Role>().ToListAsync();
        }

        public async Task<int> UpdateRole(Role role)
        {
            await SetUpDb();
            return await _dbConnection.UpdateAsync(role);
        }
    }
}