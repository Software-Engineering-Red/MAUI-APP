namespace UndacApp.Models
{
    /// <summary>
    /// Represents a Rota object.
    /// </summary>
    public class Rota : AModel
    {
        private string _location = string.Empty;
        public string Location {
            get => _location;
            set => SetField(ref _location, value);
        }

        private bool _valid = false;
        public bool Valid {
            get => _valid;
            set => SetField(ref _valid, value);
        }
    }
}
