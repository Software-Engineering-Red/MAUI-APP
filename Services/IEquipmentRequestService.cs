using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
        public interface IEquipmentRequestService
        {
            Task<List<EquipmentRequest>> GetAllRequestsAsync();
            Task<EquipmentRequest> GetRequestByIdAsync(int id);
            Task CreateRequestAsync(EquipmentRequest request);
            Task UpdateRequestAsync(EquipmentRequest request);
        }

        public class EquipmentRequestService : IEquipmentRequestService
        {
            // Example implementation. Replace with actual database/API calls
            private List<EquipmentRequest> _requests = new List<EquipmentRequest>();

            public async Task<List<EquipmentRequest>> GetAllRequestsAsync()
            {
                return await Task.FromResult(_requests);
            }

            public async Task<EquipmentRequest> GetRequestByIdAsync(int id)
            {
                return await Task.FromResult(_requests.FirstOrDefault(r => r.Id == id));
            }

            public async Task CreateRequestAsync(EquipmentRequest request)
            {
                _requests.Add(request);
                await Task.CompletedTask;
            }

            public async Task UpdateRequestAsync(EquipmentRequest request)
            {
                var existingRequest = _requests.FirstOrDefault(r => r.Id == request.Id);
                if (existingRequest != null)
                {
                    _requests.Remove(existingRequest);
                    _requests.Add(request);
                }
                await Task.CompletedTask;
            }
        }
}
