using MauiApp1.Data;
using MauiApp1.Models;
using SQLite;

namespace MauiApp1.Services
{
    public class RoomTypeService : IRoomTypeService
    {

        private SQLiteAsyncConnection _dbConnection;

        private async Task SetUpDb()
        {
            if (_dbConnection != null)
                return;


            _dbConnection = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConnection.CreateTableAsync<RoomType>();
        }

        public async Task<int> AddRoomType(RoomType roomType)
        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(roomType);
        }

        public async Task<int> DeleteRoomType(RoomType roomType)
        {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(roomType);
        }

        public async Task<List<RoomType>> GetRoomTypeList()
        {
            await SetUpDb();
            return await _dbConnection.Table<RoomType>().ToListAsync();
        }

        public async Task<int> UpdateRoomType(RoomType roomType)
        {
            await SetUpDb();
            return await _dbConnection.UpdateAsync(roomType);
        }

        
    }
}
