namespace UndacApp.Models
{
    /// <summary>
    /// A class representing a building type.
    /// </summary>
    public class BuildingType : AModel
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => SetField(ref _id, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }

        private int _number;
        public int Number
        {
            get => _number;
            set => SetField(ref _number, value);
        }

        private string _type;
        public string Type
        {
            get => _type;
            set => SetField(ref _type, value);
        }

        private string _mapArea;
        public string MapArea
        {
            get => _mapArea;
            set => SetField(ref _mapArea, value); 
        }
    }
}

