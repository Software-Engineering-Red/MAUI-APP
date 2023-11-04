using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;



namespace UndacApp.Models {
    public class Continent : AModel {
    
        private string name;
        public string Name {
            get => name;
            set => SetField(ref name, value);
        }
    }
}
