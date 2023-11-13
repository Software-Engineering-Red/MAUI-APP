using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndacApp.Models
{
    public class LocalMedia : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        /*! <summary>
         * Determine a private field 'name' to store the value of the 'Name' property.
         * </summary> 
         */
        private string name;
        public string Name
        {
            get => name;
            set => Utils.SetProperty(ref name, value, this);
        }
        public string type;
        public string Type
        {
            get => type;
            set => Utils.SetProperty(ref type, value, this);
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}

