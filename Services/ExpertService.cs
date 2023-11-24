using UndacApp.Data;
using UndacApp.Models;
using SQLite;

namespace UndacApp.Services {
    public class ExpertService : IExpertService {
        /*! <summary>
         * Variable storing dbConnection to SQLite database.
         * </summary> 
         */
        private SQLiteAsyncConnection _dbConnection;
        /*! <summary>
         * Method initiates connection to SQLite database, and creates _dbConnection table, if none is present.
        </summary> */
        private async Task SetUpDb() {
            if (_dbConnection != null)
                return;


            _dbConnection = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConnection.CreateTableAsync<Expert>();
        }

        public async Task<int> AddExpert(Expert expert) {
            await SetUpDb();
            return await _dbConnection.InsertAsync(expert);
        }

        public async Task<int> DeleteExpert(Expert expert) {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(expert);
        }

        public async Task<List<Expert>> GetExpertsList() {
            await SetUpDb();
            return await _dbConnection.Table<Expert>().ToListAsync();
        }

        public async Task<int> UpdateExpert(Expert expert) {
            await SetUpDb();
            return await _dbConnection.UpdateAsync(expert);
        }
    }
}
