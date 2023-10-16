using MauiApp1.Data;
using MauiApp1.Models;
using SQLite;


namespace MauiApp1.Services
{
    /// <summary>
    /// service class for managing room types in a SQLite database
    /// </summary>
    public class RoomTypeService : IRoomTypeService
    {

        private SQLiteAsyncConnection _dbConnection;

        /// <summary>
        /// sets up the SQLite database connection if it has not been initialized
        /// </summary>

        private async Task SetUpDb()
        {
            if (_dbConnection != null)
                return;


            _dbConnection = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
            await _dbConnection.CreateTableAsync<RoomType>();
        }

        /// <summary>
        /// adds a new room type to the database
        /// </summary>
        /// <param name="roomType">the room type to be added</param>
        /// <returns>an integer indicating the result of the operation as a task</returns>
        public async Task<int> AddRoomType(RoomType roomType)

        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(roomType);
        }

        /// <summary>
        /// Deletes a room type from the database
        /// </summary>
        /// <param name="roomType">The room type to be deleted.</param>
        /// <returns>An integer indicating the result of the operation as a task.</returns>
        public async Task<int> DeleteRoomType(RoomType roomType)
        {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(roomType);
        }
        /// <summary>
        /// retrieves a list of room types from the database
        /// </summary>
        /// <returns>a list of room types as a task</returns>
        public async Task<List<RoomType>> GetRoomTypeList()
        {
            await SetUpDb();
            return await _dbConnection.Table<RoomType>().ToListAsync();
        }
        /// <summary>
        /// Updates an existing room type in the database.
        /// </summary>
        /// <param name="roomType">The room type to be updated</param>
        /// <returns>An integer indicating the result of the operation as a task</returns>
        public async Task<int> UpdateRoomType(RoomType roomType)
        {
            await SetUpDb();
            return await _dbConnection.UpdateAsync(roomType);
        }

        
    }
}
