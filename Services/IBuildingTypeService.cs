using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public interface IBuildingTypeService
    {
        Task<List<BuildingType>> GetBuildingTypeList();
        Task<int> AddBuildingType(BuildingType buildingType);
        Task<int> DeleteBuildingType(BuildingType buildingType);
        Task<int> UpdateBuildingType(BuildingType buildingType);
    }
}
