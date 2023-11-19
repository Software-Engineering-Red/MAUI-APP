using System.Runtime.CompilerServices;
using System.ComponentModel;
using SQLite;

namespace UndacApp.Models.Temp_Models
{
    [Table("Temp-Operation_Team")]
    class Temp_Operation_Team : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int OTId { get; set; }


        private string operationTeamName;
        public string OperationTeamName 
        {  
            get => operationTeamName; 

            set => SetField(ref operationTeamName, value); 
        }
        

        private string created_By;
        public string Created_By 
        { 
            get => created_By; 
            set => SetField(ref created_By, value);
                
        }

        private int team_Id;
        public int Team_Id
        {
            get => team_Id;

            set => SetField(ref  team_Id, value);
        }

        private string status;
        public string Status
        {
            get => status;

            set => SetField(ref status, value);
        }




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
