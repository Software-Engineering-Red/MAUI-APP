using MauiApp1.Data;
using MauiApp1.Models;
using System.Text;
using SQLite;
using Microsoft.Maui.Controls;

namespace MauiApp1.Services
{
	/// <summary>
	/// Service to Apply the required SQL Statements to Add new 
	/// </summary>
	internal class SpecialistRequestService : ISpecialistRequestService
	{
		private SQLiteAsyncConnection _dbConnection;


		/// <summary>
		/// Sets up the database connection and creates the Skill table if it doesn't exist.
		/// </summary>
		private async Task SetUpDb()
		{
			if (_dbConnection != null)
				return;

			_dbConnection = new SQLiteAsyncConnection(DatabaseSettings.DBPath, DatabaseSettings.Flags);
			await _dbConnection.CreateTableAsync<Skill>();
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
		async void ISpecialistRequestService.AddUnapprovedSkillRequest(string skillName, int numberRequired,
			DateTime startDate, DateTime endDate)
		{
			SkillRequest skillRequest = new SkillRequest()
			{
				Id = 1,
				SkillName = skillName,
				OrganisationId = 0,
				RequestDate = DateTime.Now,
				RequestedBy = 0,
				NumberRequired = numberRequired,
				StartDate = startDate,
				EndDate = endDate,
				Status = "Pending",
				ConfirmedDate = DateTime.MinValue
			};
			await ((ISpecialistRequestService)this).AddSkillRequest(skillRequest);
		}

		/// <summary>
		/// Approve Request by assingning the date of approval,
		/// changing the Status to Approved, and Assignig the Corresponding Organisation
		/// </summary>
		/// <param name="id">Id of Request</param>
		/// <param name="organisationId">Id of Organisation</param>
		async void ISpecialistRequestService.approveSkillRequest(int id, int organisationId)
		{
			SkillRequest skillRequest = await ((ISpecialistRequestService)this).GetSkillRequestByIdAsync(id);
			skillRequest.OrganisationId = organisationId;
			skillRequest.ConfirmedDate = DateTime.Today;
			skillRequest.Status = "Approved";
		}



		async Task<int> ISpecialistRequestService.AddSkillRequest(SkillRequest skillRequest)
		{
			await SetUpDb();
			return await _dbConnection.InsertAsync(skillRequest);
		}

		async Task<int> ISpecialistRequestService.DeleteSkillRequestAsync(SkillRequest skillRequest)
		{
			await SetUpDb();
			return await _dbConnection.DeleteAsync(skillRequest);
		}
		async Task<SkillRequest> ISpecialistRequestService.GetSkillRequestByIdAsync(int id)
		{
			await SetUpDb();
			return await _dbConnection.Table<SkillRequest>().Where(sr => sr.Id == id).FirstOrDefaultAsync();
		}

		async Task<List<SkillRequest>> ISpecialistRequestService.GetSkillRequestListAsync()
		{
			await SetUpDb();
			return await _dbConnection.Table<SkillRequest>().ToListAsync();
		}

		async Task<int> ISpecialistRequestService.UpdateSkillRequestAsync(SkillRequest skillRequest)
		{
			await SetUpDb();
			return await _dbConnection.UpdateAsync(skillRequest);
		}
	}
}
