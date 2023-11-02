using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Models;

namespace MauiApp1.Services
{
	internal interface ISpecialistRequestService
	{
		/// <summary>
		/// Inserts a new unapporved Specialist Request in the skills_request table
		/// (confirmed_date can not be null so it is set to MinValue aswell as organisation.
		/// requested_by is a dummy Value)
		/// </summary>
		/// <param name="skillName">Required Skill</param>
		/// <param name="numberRequired">Number of Persons Required</param>
		/// <param name="startDate">Start Date</param>
		/// <param name="endDate">End Date</param>
		void AddUnapprovedSkillRequest(string skillName, int numberRequired, 
			DateTime startDate,DateTime endDate);

		/// <summary>
		/// Approve Request by assingning the date of approval,
		/// changing the Status to Approved, and Assignig the Corresponding Organisation
		/// </summary>
		/// <param name="id">Id of Request</param>
		/// <param name="organisationId">Id of Organisation</param>
		void approveSkillRequest(int id, int organisationId);
		/// <summary>
		/// Adds a instance of OperationalTeamStatus to the Database.
		/// </summary>
		/// <param name="status">Instance of OperationalTeamStatus to be added</param>
		/// <returns>Task promise to insert instance to Database</returns>
		Task<int> AddSkillRequest(SkillRequest skillRequest);

		/// <summary>
		/// Deletes a certain instance of OperationalTeamStatus from the Database.
		/// </summary>
		/// <param name="status">Instance of OperationalTeamStatus to be deleted</param>
		/// <returns>Task promise to delete Instance from Database</returns>
		Task<int> DeleteSkillRequestAsync(SkillRequest skillRequest);

		/// <summary>
		/// Gets a List of OperationalTeamStatus from Database.
		/// </summary>
		/// <returns>Task promise to get a List of OperationalTeamStatus</returns>
		Task<List<SkillRequest>> GetSkillRequestListAsync();

		Task<SkillRequest> GetSkillRequestByIdAsync(int id);


		/// <summary>
		/// Updates an entry of OperationalTeamStatus in Database.
		/// </summary>
		/// <param name="status">entry of OperationalTeamStatus</param>
		/// <returns>Task promise to update certain Status</returns>
		Task<int> UpdateSkillRequestAsync(SkillRequest skillRequest);
	}
}
