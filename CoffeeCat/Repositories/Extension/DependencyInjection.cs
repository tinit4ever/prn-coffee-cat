
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;
using Entities;
namespace Repositories.Extension
{
    public static class DependencyInjection
    {
   

        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddDbContext<CoffeeCatContext>(options => options.UseSqlServer(GetConnectionString()));
            return services;
        }

        private static string GetConnectionString()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", true, true)
                        .Build();
            var strConn = config["ConnectionStrings:CoffeeCatDb"];

            return strConn;
        }

    }
}
