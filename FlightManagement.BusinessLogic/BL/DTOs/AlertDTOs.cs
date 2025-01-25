using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagement.BusinessLogic.BL.DTOs
{
    public class AlertDTOs
    {
        public class BaseAlert
        {
            public string AirportDestination { get; set; } = null!;
            public string AirportOrigin { get; set; } = null!;
            public decimal Price { get; set; }
        }
        public class Create : BaseAlert
        {
            public Guid UserId { get; set; }
        }
        public class Update : Create
        {
            public Guid Id { get; set; }
        }

        public class GetAlerts : BaseAlert
        {
            public Guid Id { get; set; }
        }
    }
}
