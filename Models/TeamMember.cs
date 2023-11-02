﻿using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UndacApp.Models
{
    public class TeamMember : INotifyPropertyChanged {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        private string name;
        private string accessPrivledgeLevel;

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string AccessPrivledgeLevel
        {
            get => accessPrivledgeLevel;
            set => SetProperty(ref accessPrivledgeLevel, value);
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