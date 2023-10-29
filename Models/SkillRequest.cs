using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
	public class SkillRequest
	{
		public int Id { get; set; }
		public string SkillName { get; set; }
		public int OrganisationId { get; set; }
		public DateTime RequestDate { get; set; }
		public int RequestedBy { get; set; }
		public int NumberRequired { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Status { get; set; }
		public DateTime ConfirmedDate { get; set; }
	}
}
