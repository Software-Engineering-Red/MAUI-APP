using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UndacApp.Models;

namespace UndacApp.Services
{
	public class PendingResourceRequestService : IPendingResourcerRequestService
	{
		private readonly IOperationService operationService;
		private readonly IOperationalTeamService teamService;
		private readonly IOperationResourceRequestService operationRequestService;


		public PendingResourceRequestService()
		{
			operationService = new OperationService();
			teamService = new OperationalTeamService();
			operationRequestService = new OperationResourceRequestService();
		}

		public async Task<List<int>> GetOperationIdsWithPendingRequests()
		{
			try
			{
				var teams = await FindTeamsWithRequestIds();
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


		private async Task<OperationalTeam[]> FindTeamsWithRequestIds()
		{
			var allRequests = await operationRequestService.GetAll();
			if (allRequests == null)
				return new OperationalTeam[] {};

			var teamTasks = allRequests.Where(request => request.Status == OperationResourceRequestStatus.Pending).
				Select(async request => await teamService.GetOne(request.OperationalTeamId)).ToList();
			return await Task.WhenAll(teamTasks); ;
		}


		private async Task<Operation[]> FindOperationsWithTeamIds(OperationalTeam[] teams)
		{
			var operationTasks = teams.Select(async team => await operationService.GetOne(team.OperationId)).ToList();
			return await Task.WhenAll(operationTasks);
		}

		public async Task<List<OperationResourceRequest>> GetPendingRequestsByOperation(Operation operation)
		{
			try
			{
				var teamsWithOperationID = await GetTeamsWithOperationalId(operation.ID);
				if (teamsWithOperationID == null || teamsWithOperationID.Count == 0)
					return new List<OperationResourceRequest>();

				var operationRequests = await GetPendingRequestsByTeams(teamsWithOperationID);
				if (operationRequests == null || operationRequests.Count == 0)
					return new List<OperationResourceRequest>();

				return operationRequests;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in GetPendingRequestsByOperation: {ex.Message}");
				return new List<OperationResourceRequest>();
			}
		}

		private async Task<List<OperationalTeam>> GetTeamsWithOperationalId(int operationalId)
		{
			var allTeams = await teamService.GetAll();
			if (allTeams == null)
				return new List<OperationalTeam>();

			var teamsWithOperationID = allTeams.Where(team => team.OperationId == operationalId).ToList();
			if (teamsWithOperationID != null)
				return teamsWithOperationID;
			return new List<OperationalTeam>();
		}



		private async Task<List<OperationResourceRequest>> GetPendingRequestsByTeams(List<OperationalTeam> teams)
		{
			var allRequests = await operationRequestService.GetAll();
			if (allRequests == null)
				return new List<OperationResourceRequest>();

			List<int> operationalTeamIds = teams.Select(team => team.ID).ToList();
			if (operationalTeamIds == null)
				return new List<OperationResourceRequest>();

			var teamsWithOperationID = allRequests.Where(request => operationalTeamIds.Contains(request.OperationalTeamId) && 
			request.Status == OperationResourceRequestStatus.Pending).ToList();
			if (teamsWithOperationID != null)
				return teamsWithOperationID;
			return new List<OperationResourceRequest>();
		}

		public async Task<int> ApproveRequest(OperationResourceRequest request)
		{
			return await operationRequestService.ApproveRequest(request);
		}
	}
}
