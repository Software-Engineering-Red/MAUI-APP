namespace UndacApp.Models
{
    public class Resource : AModel
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

        private int _quantity = 0;
        public int Quantity
        {
            get => _quantity;
            set => SetField(ref _quantity, value);
        }
    }
}
