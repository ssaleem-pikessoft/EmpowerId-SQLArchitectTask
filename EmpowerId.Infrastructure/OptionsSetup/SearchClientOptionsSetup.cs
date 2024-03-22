using EmpowerId.Entities.OptionsSetup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace EmpowerId.Infrastructure.OptionsSetup
{
    internal class SearchClientOptionsSetup : IConfigureOptions<SearchClientOption>
    {
        private const string SectionName = "SearchClient";
        private readonly IConfiguration _configuration;

        public SearchClientOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(SearchClientOption options)
        {
            _configuration.GetSection(SectionName).Bind(options);
        }
    }
}
