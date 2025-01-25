using FlightManagement.BusinessLogic.BL;
using FlightManagement.DataAccessLayer.Db;
using FlightManagement.DataAccessLayer.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagement.BusinessLogic
{
    public static class DIRegister
    {
        public static IServiceCollection AddBusinessLogicDependencies(this IServiceCollection serviceCollection)
        {


            serviceCollection.AddScoped<AlertBL>();
            serviceCollection.AddScoped<UserBL>();

            return serviceCollection;
        }
    }
}
