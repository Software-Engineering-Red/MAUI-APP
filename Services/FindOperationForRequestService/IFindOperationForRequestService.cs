using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndacApp.Services
{
	public interface IFindOperationForRequestService
	{
		Task<List<int>> GetOperationIdsWithRequests();
	}
}
