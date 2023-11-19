
using System.ComponentModel;
using SQLite;

namespace UndacApp.Models
{
    public class LogisticsOperation : AModel
    {
        private string _vehicleAssigned = String.Empty;
        public string VehicleAssigned
        {
            get => _vehicleAssigned;
            set => Utils.SetProperty(ref _vehicleAssigned, value, this);
        }
        private string _equipmentAssigned = String.Empty;
        public string EquipmentAssigned 
        {
            get => _equipmentAssigned;
            set => Utils.SetProperty(ref _equipmentAssigned, value, this);
        }
        private DateTime _createdAt;
        public DateTime createdAt {
            get => _createdAt;
            set => Utils.SetProperty(ref _createdAt, value, this);
        }
    }
}