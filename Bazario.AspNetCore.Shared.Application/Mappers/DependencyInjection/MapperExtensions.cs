using Microsoft.Extensions.DependencyInjection;

namespace Bazario.AspNetCore.Shared.Application.Mappers.DependencyInjection
{
    public static class MapperExtensions
    {
        public static IServiceCollection AddMapper<TSource, TDestination, TMapper>(
            this IServiceCollection services)
            where TSource : class
            where TDestination : class
            where TMapper : Mapper<TSource, TDestination>
        {
            services.AddScoped<Mapper<TSource, TDestination>, TMapper>();

            return services;
        }
    }
}
