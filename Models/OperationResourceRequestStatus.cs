using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System;
using System.ComponentModel;

namespace UndacApp.Models
{
	public enum OperationResourceRequestStatus
	{
		[Description("Approved")]
		Approved,

		[Description("Pending")]
		Pending
	}

	public static class OperationResourceRequestStatusExtensions
	{
		public static string GetDescription(this OperationResourceRequestStatus status)
		{
			var field = status.GetType().GetField(status.ToString());
			var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
			return attribute == null ? status.ToString() : attribute.Description;
		}
	}
}
