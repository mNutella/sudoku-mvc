using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Sudoku.Mvc.Api.Automapper;

namespace Sudoku.Mvc.Api.Extensions
{
    public static class AutoMapperExtensions
    {
        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutomapperProfiles).GetTypeInfo().Assembly);
            return services;
        }
    }
}
