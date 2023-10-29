using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
	internal class SpecialistRequestService : ISpecialistRequestService
	{
		private readonly SqliteConnection _connection;

		/// <summary>
		/// Creates a new database operation instance, initializing the connection to the SQLite database.
		/// </summary>
		/// <param name="connectionString">The SQLite database connection string.</param>
		public SpecialistRequestService(string connectionString)
		{
			_connection = new SqliteConnection(connectionString);

			try
			{
				_connection.Open();
			}
			catch (Exception ex)
			{
				throw new Exception("Error establishing database connection.", ex);
			}
		}

		void ISpecialistRequestService.AddSkillsRequest(SkillRequest request)
		{
			throw new NotImplementedException();
		}

		void ISpecialistRequestService.DeleteSkillsRequest(int id)
		{
			throw new NotImplementedException();
		}

		List<SkillRequest> ISpecialistRequestService.GetAllSkillsRequests()
		{
			throw new NotImplementedException();
		}

		SkillRequest ISpecialistRequestService.GetSkillsRequestById(int id)
		{
			throw new NotImplementedException();
		}

		void ISpecialistRequestService.UpdateSkillsRequest(int id, SkillRequest updatedRequest)
		{
			throw new NotImplementedException();
		}
	}
}
