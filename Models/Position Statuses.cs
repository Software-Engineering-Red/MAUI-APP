using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    internal class position_statuses : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private string name;
        public string Name
        {
            get => name; 
            
            set => SetField(ref name,value);
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string proertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(proertyName));

        protected bool SetField<T>(ref T feild, T value, [CallerMemberName] string propetyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(feild, value)) return false;
            feild = value;
            OnPropertyChanged(propetyName);
            return true;
        }
    }
}
