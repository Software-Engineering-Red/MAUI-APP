using MauiApp1.Data;
using MauiApp1.Models;
using SQLite;

namespace MauiApp1.Services
{
    public class OrganisationService : IOrganisationService
    {

        private SQLiteAsyncConnection _dbConnection;

        private async Task SetUpDb()
        {
            if (_dbConnection != null) return;

            _dbConnection = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConnection.CreateTableAsync<Organisation>();
        }

        public async Task<int> AddOrganisation(Organisation org)
        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(org);
        }

        public async Task<int> DeleteOrganisation(Organisation org)
        {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(org);
        }

        public async Task<List<Organisation>> GetOrganisationList()
        {
            await SetUpDb();
            return await _dbConnection.Table<Organisation>().ToListAsync();
        }

        public async Task<int> UpdateOrganisation(Organisation org)
        {
            await SetUpDb();
            return await _dbConnection.UpdateAsync(org);
        }
    }
}
