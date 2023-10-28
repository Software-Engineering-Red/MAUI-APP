using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public class OperationPin
    {
        public int ID { get; set; }
        public string Label { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string PinType { get; set; } // "SecurityAlert", "OperationalTeam", "Zone", "OSOCCArea"
        public string StatusOrPurpose { get; set; } // Stores either zone status or OSOCC area purpose
    }
}
