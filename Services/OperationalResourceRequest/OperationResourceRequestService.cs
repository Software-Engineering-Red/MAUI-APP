using UndacApp.Models;

namespace UndacApp.Services
{
	public class OperationResourceRequestService : AService<OperationResourceRequest>, IOperationResourceRequestService
	{
		public async Task<int> ApproveRequest(OperationResourceRequest request)
		{
			request.Status = OperationResourceRequestStatus.Approved;
			return await Update(request);
		}
	}
}
