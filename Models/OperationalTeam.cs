using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndacApp.Models
{
    public class OperationalTeam : AModel
    {
        public OperationalTeam()
        {
            _createdBy = string.Empty;
        }

        [ForeignKey(typeof(OperationalTeamStatus))]     // Specify the foreign key
        public int TeamStatusID { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public OperationalTeamStatus TeamStatus { get; set; }

        private string _createdBy;
        public string CreatedBy
        {
            get => _createdBy;
            set => SetField(ref _createdBy, value);
        }
    }
}
