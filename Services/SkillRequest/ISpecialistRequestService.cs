using UndacApp.Models;

namespace UndacApp.Services
{
	/// <summary>
	/// Interface to Add Unapproved Skill Requests and Approve them.
	/// </summary>
	public interface ISpecialistRequestService : ISkillRequestService
	{
		/// <summary>
		/// Inserts a new unapporved Specialist Request in the skills_request table
		/// (confirmed_date can not be null so it is set to MinValue aswell as organisation.
		/// requested_by is a dummy Value)
		/// </summary>
		/// <param name="skillId">Required SkillId</param>
		/// <param name="numberRequired">Number of Persons Required</param>
		/// <param name="startDate">Start Date</param>
		/// <param name="endDate">End Date</param>
		public Task<int> AddUnapprovedSkillRequest(int skillId, int numberRequired,
			DateTime startDate, DateTime endDate);

		/// <summary>
		/// Approve Request by assingning the date of approval,
		/// changing the Status to Approved, and Assignig the Corresponding Organisation
		/// </summary>
		/// <param name="id">Id of Request</param>
		/// <param name="organisation">Approving Organisation</param>
		public Task<int> approveSkillRequest(int id, Organisation organisation);
	}
}
