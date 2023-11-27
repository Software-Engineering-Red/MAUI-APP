using UndacApp.Models;

namespace UndacApp.Services
{
	public interface IOperationResourceRequestService : IService<OperationResourceRequest>
	{
		Task<int> ApproveRequest(OperationResourceRequest request);
	}
}
