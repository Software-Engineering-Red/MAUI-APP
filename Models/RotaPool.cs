using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MauiApp1.Models
{
    /// <summary>
    /// Represents a RotaPool object.
    /// </summary>
    public class RotaPool : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        private string _rota;
        public string Rota {
            get => _rota;
            set => SetProperty(ref _rota, value);
        }

        private string _member;
        public string Member {
            get => _member;
            set => SetProperty(ref _member, value);
        }

        private bool _assigned;
        public bool Assigned {
            get => _assigned;
            set => SetProperty(ref _assigned, value);
        }

        private string _start_date;
        public string StartDate {
            get => _start_date;
            set => SetProperty(ref _start_date, value);
        }

        private string _end_date;
        public string EndDate {
            get => _end_date;
            set => SetProperty(ref _end_date, value);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
