using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UndacApp.Models;

namespace UndacApp.Services
{
	public class FindOperationForRequestService : IFindOperationForRequestService
	{
		private readonly IOperationService operationService;
		private readonly IOperationalTeamService teamService;
		private readonly IOperationResourceRequestService operationRequestService;


		public FindOperationForRequestService()
		{
			operationService = new OperationService();
			teamService = new OperationalTeamService();
			operationRequestService = new OperationResourceRequestService();
		}

		public async Task<List<int>> GetOperationIdsWithRequests()
		{
			try
			{
				var allRequests = await operationRequestService.GetAll();
				if (allRequests == null)
					return new List<int>();

				var teams = await FindTeamsWithRequestIds(allRequests.ToArray());
				if (teams == null)
					return new List<int>();

				var operations = await FindOperationsWithTeamIds(teams);
				if (operations == null)
					return new List<int>();

				var ids = operations.Where(operation => operation != null).Select(operation => operation.ID).ToList();
				if (ids != null)
					return ids;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in GetHighlightedIds: {ex.Message}");
			}
			return new List<int>();
		}

		private async Task<OperationalTeam[]> FindTeamsWithRequestIds(OperationResourceRequest[] requests)
		{
			var teamTasks = requests.Select(async request => await teamService.GetOne(request.OperationalTeamId)).ToList();
			return await Task.WhenAll(teamTasks); ;
		}


		private async Task<Operation[]> FindOperationsWithTeamIds(OperationalTeam[] teams)
		{
			var operationTasks = teams.Select(async team => await operationService.GetOne(team.OperationId)).ToList();
			return await Task.WhenAll(operationTasks);
		}

		public async Task<List<OperationResourceRequest>> GetRequestsByOperation(Operation operation)
		{
			try
			{
				var teamsWithOperationID = await GetTeamsWithOperationalId(operation.ID);
				if (teamsWithOperationID == null || teamsWithOperationID.Count == 0)
					return new List<OperationResourceRequest>();

				var operationRequests = await GetRequestsByTeams(teamsWithOperationID);
				if (operationRequests == null || operationRequests.Count == 0)
					return new List<OperationResourceRequest>();

				return operationRequests;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in GetRequestsByOperation: {ex.Message}");
				return new List<OperationResourceRequest>();
			}
		}

		private async Task<List<OperationalTeam>> GetTeamsWithOperationalId(int operationalId)
		{
			var allTeams = await teamService.GetAll();
			if (allTeams == null)
				return new List<OperationalTeam>();

			var teamsWithOperationID = allTeams.Where(team => team.OperationId == operationalId).ToList();
			if (teamsWithOperationID == null)
				return teamsWithOperationID;
			return new List<OperationalTeam>();
		}



		private async Task<List<OperationResourceRequest>> GetRequestsByTeams(List<OperationalTeam> teams)
		{
			var allRequests = await operationRequestService.GetAll();
			if (allRequests == null)
				return new List<OperationResourceRequest>();

			List<int> operationalTeamIds = teams.Select(team => team.ID).ToList();
			if (operationalTeamIds == null)
				return new List<OperationResourceRequest>();

			var teamsWithOperationID = allRequests.Where(request => operationalTeamIds.Contains(request.OperationalTeamId)).ToList();
			if (teamsWithOperationID == null)
				return teamsWithOperationID;
			return new List<OperationResourceRequest>();
		}

	}
}
