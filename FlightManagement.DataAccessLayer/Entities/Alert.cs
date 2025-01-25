using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagement.DataAccessLayer.Entities
{
    public class Alert(Guid userId, string airportOrigin, string airportDestination, decimal price) : Entity
    {
        [ForeignKey("User")]
        public Guid UserId { get; set; } = userId;

        public string AirportDestination { get; set; } = airportDestination;
        public string AirportOrigin { get; set; } = airportOrigin;
        public decimal Price { get; set; } = price;

        public User? User { get; set; }
    }
}
