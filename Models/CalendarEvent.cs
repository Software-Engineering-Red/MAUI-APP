using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UndacApp.Models
{
    [Table("calendar_events")]
    internal class CalendarEvent : AModel
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        private string eventName;
        public string EventName
        {
            get => eventName;
            set => SetField(ref eventName, value);
        }

        private DateTime eventStartDate;
        public DateTime EventStartDate
        {
            get => eventStartDate;
            set => SetField(ref eventStartDate, value);
        }

        private DateTime eventEndDate;
        public DateTime EventEndDate
        {
            get => eventEndDate;
            set => SetField(ref eventEndDate, value);
        }
    }
}
