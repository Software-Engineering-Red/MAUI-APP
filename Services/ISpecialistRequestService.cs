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
		void AddSkillRequest(string skillName, int numberRequired, 
			DateTime startDate,DateTime endDate);
		List<SkillRequest> GetAllSkillRequests();
		SkillRequest GetSkillRequestById(int id);
		void UpdateSkillRequest(int id, SkillRequest updatedRequest);
	}
}
