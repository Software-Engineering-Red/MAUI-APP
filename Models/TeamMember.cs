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
    public class TeamMember : AModel
    {
        public TeamMember()
        {
            Available = true;
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }

        private bool _available;
        public bool Available
        {
            get => _available;
            set => SetField(ref _available, value);
        }
    }
}
