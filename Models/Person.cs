using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndacApp.Models
{
	internal class Person : AModel
	{
		private string name;
		public string Name
		{
			get => name;
			set => SetField(ref name, value);
		}
		private string fieldname;
		public string Fieldname
		{
			get => fieldname;
			set => SetField(ref fieldname, value);
		}
	}
}
