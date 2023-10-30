using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiApp1.Models {
    [Table("it_system_status")]
    public class ITSystemStatus : INotifyPropertyChanged
    {
        /// <summary>
        /// Unique primary key ID.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        /// <summary>
        /// Name Stored private for ITStatus. 
        /// </summary>
        private string name;

        /// <summary>
        /// The Public getter and setter. 
        /// </summary>
        public string Name
        {
            get => name;
            set => SetField(ref name, value);
        }

        /// <summary>
        /// status stored for ITStatus
        /// </summary>
        public string status;

        /// <summary>
        /// The Public getter and setter for status. 
        /// </summary>
        public string Status
        {
            get => status;
            set => SetField(ref status, value);
        }

        /// <summary>
        /// avaliable stored for ITStatus
        /// </summary>
        public bool avaliable;

        /// <summary>
        /// The Public getter and setter for avaliable. 
        /// </summary>
        public bool Avaliable
        {
            get => avaliable;
            set => SetField(ref avaliable, value);
        }

        /// <summary>
        /// Event handling PropertyChange.
        /// </summary>
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