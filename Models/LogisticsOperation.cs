
using System.ComponentModel;
using SQLite;

namespace UndacApp.Models
{
    public class LogisticsOperation : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set => Utils.SetProperty(ref _name, value, this);
        }
        private string _vehicleAssigned;
        public string VehicleAssigned
        {
            get => _vehicleAssigned;
            set => Utils.SetProperty(ref _vehicleAssigned, value, this);
        }
        private string _equipmentAssigned;
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
        

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}