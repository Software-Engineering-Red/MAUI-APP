using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public interface IRotaService
    {
        Task<List<Rota>> GetRotaList();
        Task<int> AddRota(Rota rota);
        Task<int> DeleteRota(Rota rota);
        Task<int> UpdateRota(Rota rota);
    }
}