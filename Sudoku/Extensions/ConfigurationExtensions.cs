using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sudoku.Mvc.Common.Configuration.Options;

namespace Sudoku.Mvc.Api.Extensions
{
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Here add configurations
        /// </summary>
        public static IServiceCollection AddOptionsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {services.Configure<EnvironmentOptions>(configuration);
            services.Configure<DadataOptions>(configuration.GetSection(DadataOptions.SectionName));
            services.Configure<HttpClientOptions>(configuration.GetSection(HttpClientOptions.SectionName));

            services.Configure<HostingOptions>(
                configuration.GetSection(HostingOptions.SectionName));

            return services;
        }
    }
}
