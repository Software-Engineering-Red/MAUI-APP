using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public interface IEquipmentService
    {
        Task<List<Equipment>> GetEquipmentList();
        Task<int> AddEquipment(Equipment equipment);
        Task<int> DeleteEquipment(Equipment equipment);
        Task<int> UpdateEquipment(Equipment equipment);
    }
}