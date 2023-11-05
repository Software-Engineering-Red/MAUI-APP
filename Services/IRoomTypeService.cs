using UndacApp.Models;

namespace UndacApp.Services
{
    /// <summary>
    /// Interface used for managing rooms 
    /// </summary>

    public interface IRoomTypeService
    {
        /// <summary>
        /// Retrieves list of room types
        /// </summary>
        /// <returns>A list of room types</returns>
        Task<List<RoomType>> GetRoomTypeList();
        /// <summary>
        /// Adds a new room type
        /// </summary>
        /// <param name="roomType">The room type to be added</param>
        /// <returns>An integer indicating the result of the operation as a task</returns>
        Task<int> AddRoomType(RoomType roomType);
        /// <summary>
        /// Deletes a room type
        /// </summary>
        /// <param name="roomType">The room type to be deleted</param>
        /// <returns>An integer indicating the result of the operation as a task</returns>
        Task<int> DeleteRoomType(RoomType roomType);
        /// <summary>
        /// Updates an existing room type
        /// </summary>
        /// <param name="roomType">The room type to be updated</param>
        /// <returns>An integer indicating the result of the operation as a task</returns>
        Task<int> UpdateRoomType(RoomType roomType);

    }
}