using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndacApp.Models
{
	public class OperationalTeam : AModel
	{
        public OperationalTeam()
		{
            _createdBy = string.Empty;
            _teamStatus = string.Empty;
		}

        private string _teamStatus;
        public string TeamStatus
		{
            get => _teamStatus;
            set => SetField(ref _teamStatus, value);
		}

        private string _createdBy;
        public string CreatedBy
		{
            get => _createdBy;
            set => SetField(ref _createdBy, value);
		}
	}
}
