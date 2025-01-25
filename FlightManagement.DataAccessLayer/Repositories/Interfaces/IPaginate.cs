using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagement.DataAccessLayer.Repositories.Interfaces
{
    public interface IPaginate<T>
    {
        public int skip { get; set; }
        public int take { get; set; }
        public Func<IQueryable<T>, IOrderedQueryable<T>> GetOrdering(string? langKey = null);
    }
}
