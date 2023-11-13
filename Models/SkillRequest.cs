using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;


namespace UndacApp.Models
{
	public class SkillRequest : AModel
	{
		private int skillId;
		public int SkillId
		{
			get => skillId;
			set => SetField(ref skillId, value);
		}

		private int organisationId;
		public int OrganisationId
		{
			get => organisationId;
			set => SetField(ref organisationId, value);
		}

		private DateTime requestDate;
		public DateTime RequestDate
		{
			get => requestDate;
			set => SetField(ref requestDate, value);
		}

		private int requestedBy;
		public int RequestedBy
		{
			get => requestedBy;
			set => SetField(ref requestedBy, value);
		}

		private int numberRequired;
		public int NumberRequired
		{
			get => numberRequired;
			set => SetField(ref numberRequired, value);
		}

		private DateTime startDate;
		public DateTime StartDate
		{
			get => startDate;
			set => SetField(ref startDate, value);
		}

		private DateTime endDate;
		public DateTime EndDate
		{
			get => endDate;
			set => SetField(ref endDate, value);
		}

		private string status;
		public string Status
		{
			get => status;
			set => SetField(ref status, value);
		}

		private DateTime confirmedDate;
		public DateTime ConfirmedDate
		{
			get => confirmedDate;
			set => SetField(ref confirmedDate, value);
		}
	}
}
