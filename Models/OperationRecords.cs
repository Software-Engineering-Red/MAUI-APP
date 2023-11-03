using System.Runtime.CompilerServices;
using System.ComponentModel;
using SQLite;

namespace MauiApp1.Models
{
    [Table("Operation_Authorisation")]
    class OperationRecords : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement] 
        public int Id { get; set; }

        private int operationalTeamID;
        public int OperationalTeamID
        {  
            get => operationalTeamID; 
            
            set => SetField(ref operationalTeamID, value); 
        }

        private string requested_By;
        public string Requested_By
        { 
            get => Requested_By; 

            set => SetField(ref requested_By, value); 
        }

        /*private*/ 

        

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = " ")
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}

