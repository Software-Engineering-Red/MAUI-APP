using SQLite;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UndacApp.Models
{
    public class WeatherAnomalyNotification : AModel
    {

        private string _message;
        public string Message
        {
            get => _message;
            set => SetField(ref _message, value);
        }

        private DateTime _createdAt;
        public DateTime CreatedAt
        {
            get => _createdAt;
            set => SetField(ref _createdAt, value);
        }

        private bool _cleared;
        public bool Cleared
        {
            get => _cleared;
            set => SetField(ref _cleared, value);
        }
    }
}
