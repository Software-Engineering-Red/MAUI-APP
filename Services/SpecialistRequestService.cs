using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Models;

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

		void ISpecialistRequestService.AddSkillRequest(string skillName, int numberRequired,
			DateTime startDate, DateTime endDate)
		{
			var commandText = @"INSERT INTO skills_request (skill_name, organisation_id, request_date, requested_by, number_required, start_date, end_date, status, confirmed_date) 
                               VALUES (@skill_name, @organisation_id, @request_date, @requested_by, @number_required, @start_date, @end_date, @status, @confirmed_date)";

			using (var command = new SqliteCommand(commandText, _connection))
			{
				command.Parameters.AddWithValue("@skill_name", skillName);
				command.Parameters.AddWithValue("@organisation_id", null);
				command.Parameters.AddWithValue("@request_date", DateTime.Today);
				command.Parameters.AddWithValue("@requested_by", null); // specific Persons and users not implemented yet 
				command.Parameters.AddWithValue("@number_required", numberRequired);
				command.Parameters.AddWithValue("@start_date", startDate);
				command.Parameters.AddWithValue("@end_date", endDate);
				command.Parameters.AddWithValue("@status", "Pending");
				command.Parameters.AddWithValue("@confirmed_date", null);

				command.ExecuteNonQuery();
			}
		}


		List<SkillRequest> ISpecialistRequestService.GetAllSkillRequests()
		{
			throw new NotImplementedException();
		}

		SkillRequest ISpecialistRequestService.GetSkillRequestById(int id)
		{
			throw new NotImplementedException();
		}

		void ISpecialistRequestService.UpdateSkillRequest(int id, SkillRequest updatedRequest)
		{
			throw new NotImplementedException();
		}

		void approveSkillRequest(int id, int organisationId) 
		{ 
			
		}
	}
}
