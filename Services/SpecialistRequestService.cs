using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Models;

namespace MauiApp1.Services
{
	/// <summary>
	/// Service to Apply the required SQL Statements to Add new 
	/// </summary>
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

		/// <summary>
		/// Inserts a new unapporved Specialist Request in the skills_request table
		/// (confirmed_date can not be null so it is set to MinValue aswell as organisation.
		/// requested_by is a dummy Value)
		/// </summary>
		/// <param name="skillName">Required Skill</param>
		/// <param name="numberRequired">Number of Persons Required</param>
		/// <param name="startDate">Start Date</param>
		/// <param name="endDate">End Date</param>
		void ISpecialistRequestService.AddUnapprovedSkillRequest(string skillName, int numberRequired,
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
				command.Parameters.AddWithValue("@requested_by", 0); // dummy value 
				command.Parameters.AddWithValue("@number_required", numberRequired);
				command.Parameters.AddWithValue("@start_date", startDate);
				command.Parameters.AddWithValue("@end_date", endDate);
				command.Parameters.AddWithValue("@status", "Pending");
				command.Parameters.AddWithValue("@confirmed_date", DateTime.MinValue);

				command.ExecuteNonQuery();
			}
		}

		/// <summary>
		/// Approve Request by assingning the date of approval,
		/// changing the Status to Approved, and Assignig the Corresponding Organisation
		/// </summary>
		/// <param name="id">Id of Request</param>
		/// <param name="organisationId">Id of Organisation</param>
		void ISpecialistRequestService.approveSkillRequest(int id, int organisationId)
		{
			var currentDate = DateTime.Today;
			var commandText = "UPDATE skills_request SET confirmed_date = @currentDate," +
				" organisation_id = @organisationId, status = 'Approved' WHERE Id = @id";

			using (var command = new SqliteCommand(commandText, _connection))
			{
				command.Parameters.AddWithValue("@currentDate", currentDate);
				command.Parameters.AddWithValue("@organisationId", organisationId);
				command.Parameters.AddWithValue("@id", id);

				command.ExecuteNonQuery();
			}
		}

		/// <summary>
		/// Returns a list of SkillRequest objects by Calling a 
		/// SELECT statement for skills_request
		/// </summary>
		/// <returns>A list of SkillRequest objects, created from the database</returns>
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
					SkillRequest skillRequest = CreateSkillRequestFromReader(reader);
					skillRequests.Add(skillRequest);
				}
			}
			return skillRequests;
		}

		void ISpecialistRequestService.deleteSkillRequestById(int id)
		{
			var commandText = $"DELETE FROM skills_request WHERE Id = @id";
			using (var command = new SqliteCommand(commandText, _connection))
			{
				command.Parameters.AddWithValue("@id", id);

				command.ExecuteNonQuery();
			}
		}
		/// <summary>
		/// Creates one SkillRequest object by transfering 
		/// data from the reader.
		/// </summary>
		/// <param name="reader">Reading Database respose</param>
		/// <returns></returns>
		private SkillRequest CreateSkillRequestFromReader(SqliteDataReader reader)
		{
			return new SkillRequest
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
		}
	}
}
