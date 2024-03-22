using EmpowerId.Infrastructure.OptionsSetup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpowerId.Infrastructure.Extension
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureOptions<SearchClientOptionsSetup>();
        

            return services;
        }
    }
}
