using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public interface IContinentService
    {
        Task<List<BuildingType>> GetContinentList();
        Task<int> AddContinent(BuildingType continent);
        Task<int> DeleteContinent(BuildingType continent);
        Task<int> UpdateContinent(BuildingType continent);
    }
}
