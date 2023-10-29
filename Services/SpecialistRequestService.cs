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
			var commandText = "INSERT INTO skills_request " +
									  "(skill_name, organisation_id, request_date, requested_by, number_required, " +
									  "start_date, end_date, status, confirmed_date) " +
									  "VALUES " +
									  "(@skill_name, @organisation_id, @request_date, @requested_by, @number_required, " +
									  "@start_date, @end_date, @status, @confirmed_date)";

			using (var command = new SqliteCommand(commandText, _connection))
			{
				command.Parameters.AddWithValue("@skill_name", skillName);
				command.Parameters.AddWithValue("@organisation_id", 0); 
				command.Parameters.AddWithValue("@request_date", DateTime.Today);
				command.Parameters.AddWithValue("@requested_by", 0); // specific Persons and users not implemented yet 
				command.Parameters.AddWithValue("@number_required", numberRequired);
				command.Parameters.AddWithValue("@start_date", startDate);
				command.Parameters.AddWithValue("@end_date", endDate);
				command.Parameters.AddWithValue("@status", "Pending");
				command.Parameters.AddWithValue("@confirmed_date", DateTime.MinValue);

				command.ExecuteNonQuery();
			}
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
			var commandText = $"UPDATE skills_request SET confirmedDate = {DateTime.Today}," +
				$" organisation_id = {organisationId}, status = 'Approved' WHERE Id = {id};";
			using (var command = new SqliteCommand(commandText, _connection))
			{
				command.ExecuteNonQuery();
			}

		}

		List<SkillRequest> ISpecialistRequestService.GetAllSkillRequests()
		{
			var skillRequests = new List<SkillRequest>();
			var commandText = "SELECT * FROM skills_request";

			using var command = _connection.CreateCommand();
			command.CommandText = commandText;

			using (var reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					SkillRequest skillRequest = new SkillRequest
					{
						Id = reader.GetInt32(0),
						SkillName = reader.GetString(1),
						OrganisationId = reader.GetInt32(2),
						RequestDate = reader.GetDateTime(3),
						RequestedBy = reader.GetInt32(4),
						NumberRequired = reader.GetInt32(5),
						StartDate = reader.GetDateTime(6),
						EndDate = reader.GetDateTime(7),
						Status = reader.GetString(8),
						ConfirmedDate = reader.GetDateTime(9)
					};

					skillRequests.Add(skillRequest);
				}
			}
			return skillRequests;
		}
	}
}
