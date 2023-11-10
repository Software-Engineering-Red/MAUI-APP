using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;


namespace UndacApp.Models
{
    public enum Status
    {
        Pending,
        Approved,
        Denied
    }
    public class ARequest : AModel
    {
        private string? requestType = null;
        public string? RequestType
        {
            get => requestType;
            set => SetField(ref requestType, value);
        }

        private string? status = String.Empty;
        public string? Status
        {
            get => status;
            set => SetField(ref status, value);
        }
    }
}
