using FlightManagement.DataAccessLayer.Db;
using FlightManagement.DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagement.DataAccessLayer
{
    public static class DIRegister
    {
        public static IServiceCollection AddDataAccessLayerDependencies(this IServiceCollection serviceCollection)
        {

            serviceCollection.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("FlightManagment");
            });

            serviceCollection.AddTransient<IUnitOfWork, UnitOfWork>();

            return serviceCollection;
        }
    }
}
