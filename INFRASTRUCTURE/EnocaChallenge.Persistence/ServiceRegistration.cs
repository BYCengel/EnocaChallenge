using EnocaChallenge.Application.Repositories.CompanyRep;
using EnocaChallenge.Application.Repositories.OrderRep;
using EnocaChallenge.Application.Repositories.ProductRep;
using EnocaChallenge.Persistence.Contexts;
using EnocaChallenge.Persistence.Repositories.CompanyConcrete;
using EnocaChallenge.Persistence.Repositories.OrderConcrete;
using EnocaChallenge.Persistence.Repositories.ProductConcrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnocaChallenge.Persistence
{
    /// <summary>
    /// Service registration for Persistence Layer.
    /// </summary>
    public static class ServiceRegistration
    { 
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            //Getting DB connection string from appsettings.json
            ConfigurationManager configurationManager = new();
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/EnocaChallenge.API"));
            configurationManager.AddJsonFile("appsettings.json");

            services.AddDbContext<ShopDbContext>(options => options.UseSqlServer(configurationManager.GetConnectionString("MSSQL")));

            //Dependency Injection for Repositories
            services.AddScoped<ICompanyReadRepository, CompanyReadRepository>();
            services.AddScoped<ICompanyWriteRepository, CompanyWriteRepository>();

            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
        }
    }
}
