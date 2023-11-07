using System.ComponentModel;


namespace UndacApp.Models {
    public class Continent : AModel, INotifyPropertyChanged
    {
    
        private string name;
        public string Name {
            get => name;
            set => Utils.SetProperty(ref name, value, this);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
