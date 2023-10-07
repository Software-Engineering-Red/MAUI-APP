using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    /// <summary>
    /// A service for handling building types.
    /// </summary>
    public interface IBuildingTypeService
    {
        /// <summary>
        /// Gets a list of available building types.
        /// </summary>
        /// <returns>The list of available building types.</returns>
        Task<List<BuildingType>> GetBuildingTypeList();

        /// <summary>
        /// Add a building type.
        /// </summary>
        /// <param name="buildingType">The building type to be added.</param>
        Task<int> AddBuildingType(BuildingType buildingType);

        /// <summary>
        /// Delete a building type,
        /// </summary>
        /// <param name="buildingType">The building type to be deleted.</param>
        Task<int> DeleteBuildingType(BuildingType buildingType);

        /// <summary>
        /// Update a building type.
        /// </summary>
        /// <param name="buildingType">The building type to be updated.</param>
        Task<int> UpdateBuildingType(BuildingType buildingType);
    }
}
