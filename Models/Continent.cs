using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace UndacApp.Models {
    public class Continent : AModel {
    
        private string name;
        public string Name {
            get => name;
            set => SetField(ref name, value);
        }
    }
}
