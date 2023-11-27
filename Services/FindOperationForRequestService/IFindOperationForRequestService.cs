using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UndacApp.Models;

namespace UndacApp.Services
{
	public interface IFindOperationForRequestService
	{
		Task<List<int>> GetOperationIdsWithPendingRequests();
		Task<List<OperationResourceRequest>> GetRequestsByOperation(Operation operation);
		Task<int> ApproveRequest(OperationResourceRequest request);
	}
}
