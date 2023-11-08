using UndacApp.Data;
using UndacApp.Models;
using SQLite;

namespace UndacApp.Services
{
	/// <summary>
	/// Service to Add Unapproved Skill Requests and Approve them.
	/// Inherits from SkillRequestService the implementation of a CRUD Interface 
	/// to interact with the table SkillRequest.
	/// </summary>
	public class SpecialistRequestService : SkillRequestService, ISpecialistRequestService
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
			await _dbConnection.CreateTableAsync<SkillRequest>();
		}

		/// <summary>
		/// Inserts a new unapporved Specialist Request in the skills_request table
		/// (confirmed_date can not be null so it is set to MinValue aswell as organisation.
		/// requested_by is a dummy Value)
		/// </summary>
		/// <param name="skillId">Required SkillId</param>
		/// <param name="numberRequired">Number of Persons Required</param>
		/// <param name="startDate">Start Date</param>
		/// <param name="endDate">End Date</param>
		async Task<int> ISpecialistRequestService.AddUnapprovedSkillRequest(int skillId, int numberRequired, 
			DateTime startDate, DateTime endDate)
		{
			SkillRequest skillRequest = new SkillRequest()
			{
				SkillId = skillId,
				OrganisationId = 0,
				RequestDate = DateTime.Now,
				RequestedBy = 0,
				NumberRequired = numberRequired,
				StartDate = startDate,
				EndDate = endDate,
				Status = "Pending",
				ConfirmedDate = DateTime.MaxValue
			};
			return await Add(skillRequest);
		}

		/// <summary>
		/// Approve Request by assingning the date of approval,
		/// changing the Status to Approved, and Assignig the Corresponding Organisation
		/// </summary>
		/// <param name="id">Id of Request</param>
		/// <param name="organisation">Approving Organisation</param>
		async Task<int> ISpecialistRequestService.approveSkillRequest(int id, Organisation organisation)
		{
			SkillRequest skillRequest = await GetOne(id);
			skillRequest.OrganisationId = organisation.id;
			skillRequest.ConfirmedDate = DateTime.Today;
			skillRequest.Status = "Approved";
			return await Update(skillRequest);
		}

	}
}
