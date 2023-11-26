namespace UndacApp.Models
{
    /*! <summary>
        A model structure for Equipment data
    </summary> 
    <details>Data is stored in SQLite database.</details> */
    public class AnEquipment : AModel
    {
        private string? _type = String.Empty;
        public string? Type
        {
            get => _type;
            set => SetField(ref _type, value);
        }

        private string? _location = String.Empty;
        public string? Location
        {
            get => _location;
            set => SetField(ref _location, value);
        }

        private string? _currentOperation = String.Empty;
        public string? CurrentOperation
        {
            get => _currentOperation;
            set => SetField(ref _currentOperation, value);
        }

        private int _quantity = 0;
        public int Quantity
        {
            get => _quantity;
            set => SetField(ref _quantity, value);
        }

    }
}