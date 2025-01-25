using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagement.DataAccessLayer.Entities
{
    public class User(string fullName) : Entity
    {
        public string FullName { get; set; } = fullName;
        public List<Alert>? Alerts { get; set; } = [];
    }
}
