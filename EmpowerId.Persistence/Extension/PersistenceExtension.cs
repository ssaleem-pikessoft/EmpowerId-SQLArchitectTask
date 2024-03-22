using EmpowerId.Persistence.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmpowerId.Interfaces.Contracts.Persistence.Data;
using EmpowerId.Persistence.Repositories.Data;

namespace EmpowerId.Persistence.Extension
{
    public static class PersistenceExtension
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EmpowerDbContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("DefaultConnection"),
                   b => b.MigrationsAssembly(typeof(EmpowerDbContext).Assembly.FullName)));

            services.AddScoped<ISearchService, SearchService>();
            return services;
            
        }
    }
}
