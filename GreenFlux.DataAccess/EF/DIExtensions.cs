using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreenFlux.DataAccess.EF
{
   public static class DIExtensions
    {

        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options => options.UseInMemoryDatabase(databaseName: "GreenFluxDb"));
           
            services.AddScoped<DatabaseContext>();


            return services;
        }
    }
}
