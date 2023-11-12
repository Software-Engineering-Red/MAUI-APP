using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndacApp.Models
{
	public class OperationResourceRequest : AModel
	{
		private int operational_team_Id;
		public int OperationalTeamId
		{
			get => operational_team_Id;
			set => SetField(ref operational_team_Id, value);
		}

		private int requestedBy;
		public int RequestedBy
		{
			get => requestedBy;
			set => SetField(ref requestedBy, value);
		}

		private string request_detail;
		public string RequestedDetail
		{
			get => request_detail;
			set => SetField(ref request_detail, value);
		}

		private DateTime requestDate;
		public DateTime RequestDate
		{
			get => requestDate;
			set => SetField(ref requestDate, value);
		}

		private int resource_Id;
		public int ResourceId
		{
			get => resource_Id;
			set => SetField(ref resource_Id, value);
		}

		private string status;
		public string Status
		{
			get => status;
			set => SetField(ref status, value);
		}

		private int confirmed_by;
		public int confirmedBy
		{
			get => confirmed_by;
			set => SetField(ref confirmed_by, value);
		}

		private DateTime confirmedDate;
		public DateTime ConfirmedDate
		{
			get => confirmedDate;
			set => SetField(ref confirmedDate, value);
		}
	}
}
