using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
	internal interface ISpecialistRequestService
	{
		void AddSkillsRequest(SkillRequest request);
		List<SkillRequest> GetAllSkillsRequests();
		SkillRequest GetSkillsRequestById(int id);
		void UpdateSkillsRequest(int id, SkillRequest updatedRequest);
		void DeleteSkillsRequest(int id);
	}
}
