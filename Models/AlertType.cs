using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiApp1.Models
{
    public class AlertType : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
