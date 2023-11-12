using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UndacApp.Models;

namespace UndacApp.Services
{
	public interface INameDiscriminatorService<T> where T : ANameModel, new()
	{
		Task<T> GetOne(string name);
		Task<List<T>> GetAll();
		Task<int> Add(T entity);
		Task<int> Update(T entity);
		Task<int> Remove(T entity);
		Task<bool> Exists(string name);
	}
}
