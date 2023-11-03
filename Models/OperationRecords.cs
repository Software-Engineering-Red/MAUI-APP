using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using SQLite;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace MauiApp1.Models
{
    [Table("Operation_Authorisation")]
    class OperationRecords : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement] 
        public int Id { get; set; }

        /*[ForeignKey(nameof(Operational_Team_ID))]
        public int Operational_Team_ID { get; set; }*/

        private string requested_By;
        public string Requested_By
        { 
            get => Requested_By; 

            set => SetField(ref requested_By, value); 
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

