using System.Runtime.CompilerServices;
using System.ComponentModel;
using SQLite;

namespace MauiApp1.Models
{
    [Table("OperationRecords")]
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

        private string request_Detail;
        public string Requested_Detail
        { 
            get => request_Detail; 
            
            set => SetField(ref request_Detail, value); 
        }

        private DateTime request_Date;
        public DateTime Requested_Date
        {
            get => request_Date;

            set => SetField(ref request_Date, value);
        }

        private string status;
        public string Status
        {
            get => status;

            set => SetField(ref status, value);
        }

        private string confirmed_by;
        public string Confirmed_By
        {
            get => confirmed_by;

            set => SetField(ref confirmed_by, value);
        }

        private DateTime confirmed_Date;
        public DateTime Confirmed_Date
        {
            get => confirmed_Date;

            set => SetField(ref confirmed_Date, value);
        }

        private string fk_OpperationRecordsID_OperationalTeamID;
        public string FK_OpperationRecordsID_OperationalTeamID
        {
            get => fk_OpperationRecordsID_OperationalTeamID;
            set => SetField(ref fk_OpperationRecordsID_OperationalTeamID, value);
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

