using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public class EquipmentRequest
    {
        public int Id { get; set; }
        public string EquipmentType { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; } // e.g., Requested, Approved, Denied
    }
}