using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UndacApp.Models;

namespace UndacApp.Services
{
	public interface IPendingResourcerRequestService
	{
		Task<List<int>> GetOperationIdsWithPendingRequests();
		Task<List<OperationResourceRequest>> GetPendingRequestsByOperation(Operation operation);
		Task<int> ApproveRequest(OperationResourceRequest request);
	}
}
