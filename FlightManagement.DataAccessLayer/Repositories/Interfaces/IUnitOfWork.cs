using FlightManagement.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagement.DataAccessLayer.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        void Clear();
        Task<int> Commit(CancellationToken? cancellationToken = null);
        IRepository<T> Repository<T>() where T : Entity;
    }
}
