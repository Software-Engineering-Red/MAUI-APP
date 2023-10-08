using MauiApp1.Models;

namespace MauiApp1.Services
{
    public interface IRoomTypeService
    {
        Task<List<RoomType>> GetRoomTypeList();
        Task<int> AddRoomType(RoomType roomType);
        Task<int> DeleteRoomType(RoomType roomType);
        Task<int> UpdateRoomType(RoomType roomType);
       
    }
}