using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagement.Framework.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string key, params string[] args) : base(key, "Entity was not found exception", args)
        {
        }
    }
}
