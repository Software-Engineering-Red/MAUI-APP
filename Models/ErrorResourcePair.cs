using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndacApp.Models
{
    public class ErrorResourcePair
    {
        public string Error { get; set; }
        public string Resource { get; set; }

        // Default constructor
        public ErrorResourcePair()
        {
        }

        // Overloaded constructor for initializing properties
        public ErrorResourcePair(string error, string resource)
        {
            Error = error;
            Resource = resource;
        }

        // Override ToString for easy debugging and display
        public override string ToString()
        {
            return $"Error: {Error}, Resource: {Resource}";
        }

        // Other methods as needed...
    }
}
