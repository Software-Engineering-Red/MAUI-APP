using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndacApp.Models
{
	public class Operation : AModel
	{
		public Operation()
		{
			_createdBy = string.Empty;
			_type = string.Empty;
			_purpose = string.Empty;
			_location = string.Empty;
		}

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
	}
}
