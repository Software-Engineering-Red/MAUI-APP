using UndacApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndacApp.Services
{
    public interface IService<T> where T : AModel,new()
    {
        Task<T> GetOne(int id);
        Task<List<T>> GetAll();
        Task<int> Add(T entity);
        Task<int> Update(T entity);
        Task<int> Remove(T entity);
        Task<bool> Exists(int id);
    }
}
