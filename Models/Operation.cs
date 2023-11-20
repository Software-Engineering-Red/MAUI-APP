using System;
using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace UndacApp.Models
{
	public class Operation : AModel
	{


		private string _type;
		public string Type
		{
			get => _type;
			set => SetField(ref _type, value);
		}

		private string _purpose;
		public string Purpose
		{
			get => _purpose;
			set => SetField(ref _purpose, value);
		}

		private string _location;
		public string Location
		{
			get => _location;
			set => SetField(ref _location, value);
		}


		private string _createdBy;
		public string CreatedBy
		{
			get => _createdBy;
			set => SetField(ref _createdBy, value);
		}

		private DateTime? _created_Date;
		public DateTime? CreatedDate
		{
			get => _created_Date;
			set => SetField(ref _created_Date, value);
		}

		[OneToMany]
		public List<OperationalTeam> OperationalTeams { get; set; }
	}
}
