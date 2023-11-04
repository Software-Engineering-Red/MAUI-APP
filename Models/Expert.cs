using Microsoft.Extensions.Primitives;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiApp1.Models {
    /*! <summary>
     * The Expert data model class.
     * </summary>
     * <details>Data is stored in SQLite database.</details> 
     */
    public class Expert : INotifyPropertyChanged {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        private string _skill;
        public string Skill {
            get => _skill;
            set => SetProperty(ref _skill, value);
        }

        private string _location;
        public string Location {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        private string _status;
        public string Status {
            get => _status;
            set => SetStatus(value); 
        }

        private void SetStatus(string value) {
            SetProperty(ref _status, value);
            StatusChange = DateTime.Now.ToString();
        }

        public string StatusChange;


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null) {
            if (EqualityComparer<T>.Default.Equals(storage, value)) {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
