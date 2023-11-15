using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndacApp.Models
{
	public class OperationalTeam : AModel
	{
		private string name;
		public string Name
		{
			get => name;
			set => SetField(ref name, value);
		}

		private int created_by;
		public int CreatedBy
		{
			get => created_by;
			set => SetField(ref created_by, value);
		}


		private int team_id;
		public int TeamId
		{
			get => team_id;
			set => SetField(ref team_id, value);
		}

		private int status;
		public int Status
		{
			get => status;
			set => SetField(ref status, value);
		}
	}
}
