using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagement.DataAccessLayer.Repositories.Interfaces
{
    public interface IFilterBuilder<T>
    {
        IFilterBuilder<T> Add(Expression<Func<T, bool>> filter);
        IEnumerable<Expression<Func<T, bool>>> Build();
    }
}
