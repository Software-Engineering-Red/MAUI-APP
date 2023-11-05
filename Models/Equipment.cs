using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace UndacApp.Models
{
    /*! <summary>
        A model structure for Equipment data
    </summary> 
    <details>Data is stored in SQLite database.</details> */
    public class Equipment : INotifyPropertyChanged
    {
        /*! <summary>
          An unique primary key used to manage database entries
       </summary> */
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        /*! <summary>
       A private variable, storing Equipment name
          </summary> */
        private string _name;
        public string Name
        {
            get => _name;
            set => Utils.SetProperty(ref _name, value, this);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}