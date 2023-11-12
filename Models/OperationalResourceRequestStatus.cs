using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndacApp.Models
{
	public class OperationalResourceRequestStatus
	{

		private string _name;

		[PrimaryKey]
		public string Name
		{
			get => _name;
			set => Utils.SetProperty(ref _name, value, this);
		}

		public event PropertyChangedEventHandler? PropertyChanged;
	}
}
