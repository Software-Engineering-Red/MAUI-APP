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
		/// Returns a list of SkillRequest objects by Calling a 
		/// SELECT statement for skills_request
		/// </summary>
		/// <returns>A list of SkillRequest objects, created from the database</returns>
		List<SkillRequest> GetAllSkillRequests();

		/// <summary>
		/// Approve Request by assingning the date of approval,
		/// changing the Status to Approved, and Assignig the Corresponding Organisation
		/// </summary>
		/// <param name="id">Id of Request</param>
		/// <param name="organisationId">Id of Organisation</param>
		void approveSkillRequest(int id, int organisationId);

		void deleteSkillRequestById(int id);
	}
}
