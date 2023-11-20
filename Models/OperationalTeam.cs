﻿using System;
using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

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

		/// <summary>
		/// The attribute is described a bit confusing as operationId in the ERD
		/// </summary>
		private int _operationId;
		[ForeignKey(typeof(Operation))]
		public int OperationId
		{
			get => _operationId;
			set => SetField(ref _operationId, value);
		}

		private string _createdBy;
        public string CreatedBy
		{
            get => _createdBy;
            set => SetField(ref _createdBy, value);
		}

		[OneToMany]
		public List<OperationResourceRequest> OperationResourceRequests { get; set; }
	}
}
