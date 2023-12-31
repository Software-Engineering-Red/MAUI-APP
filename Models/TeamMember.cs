﻿using SQLite;
using System.ComponentModel;

namespace UndacApp.Models
{
    public class TeamMember : AModel {

        public TeamMember()
        {
            Available = true;
        }

        private string _accessPrivilegeLevel;
        public string AccessPrivilegeLevel
        {
            get => _accessPrivilegeLevel;
            set => SetField(ref _accessPrivilegeLevel, value);
        }

        private bool _available;
        public bool Available
        {
            get => _available;
            set => SetField(ref _available, value);
        }
        private string systemType;
        public string SystemType
        {
            get => systemType;
            set => SetField(ref systemType, value);
        }
        public bool isSystemTypeUpdatePending;
        public bool IsSystemTypeUpdatePending
        {
            get => isSystemTypeUpdatePending;
            set => SetField(ref isSystemTypeUpdatePending, value);

        }
    }
}
