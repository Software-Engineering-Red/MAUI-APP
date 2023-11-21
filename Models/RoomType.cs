namespace UndacApp.Models
{
    /// <summary>
    /// Represents a type of room in the application
    /// </summary>
    public class RoomType : AModel
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => SetField(ref _id, value);
        }

        private int _buildingId;
        public int BuildingId
        {
            get => _buildingId;
            set => SetField(ref _buildingId, value);
        }

        private string _number;
        public string Number
        {
            get => _number;
            set => SetField(ref _number, value);
        }

        private int _typeId; 
        public int TypeId
        {
            get => _typeId;
            set => SetField(ref _typeId, value);
        }

        private int _floor;
        public int Floor
        {
            get => _floor;
            set => SetField(ref _floor, value);
        }

        private int _capacity;
        public int Capacity
        {
            get => _capacity;
            set => SetField(ref _capacity, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }
    }
}
