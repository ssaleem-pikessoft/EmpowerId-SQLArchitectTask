using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EmpowerId.MediatR.Extension
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(option => option.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAzureClients(builder =>
            {
                builder.AddSearchClient(configuration.GetSection("SearchClient"));
            });
            return services;    
        }
    }
}
