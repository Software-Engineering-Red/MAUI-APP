using System.Transactions;

namespace UndacApp.Models
{
    public class LogisticsOperation : AModel
    {
        private string _vehicleAssigned = String.Empty;
        public string VehicleAssigned
        {
            get => _vehicleAssigned;
            set => SetField(ref _vehicleAssigned, value);
        }
        private string _equipmentAssigned = String.Empty;
        public string EquipmentAssigned 
        {
            get => _equipmentAssigned;
            set => SetField(ref _equipmentAssigned, value);
        }
        private DateTime _createdAt;
        public DateTime createdAt {
            get => _createdAt;
            set => SetField(ref _createdAt, value);
        }
    }
}