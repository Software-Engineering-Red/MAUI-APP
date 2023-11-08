using System.ComponentModel;


namespace UndacApp.Models {
    public class Continent : AModel
    {
    
        private string name;
        public string Name {
            get => name;
            set => SetField(ref name, value);
        }
    }
}
