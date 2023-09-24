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
        Task<List<Continent>> GetContinentList();
        Task<int> AddContinent(Continent continent);
        Task<int> DeleteContinent(Continent continent);
        Task<int> UpdateContinent(Continent continent);
    }
}
