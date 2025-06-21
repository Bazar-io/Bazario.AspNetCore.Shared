using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Bazario.AspNetCore.Shared.Application.Mappers.DependencyInjection
{
    public static class MapperExtensions
    {
        public static IServiceCollection AddMappers(
            this IServiceCollection services,
            Assembly assembly)
        {
            services.Scan(scan =>
                scan.FromAssemblies(assembly)
                    .AddClasses(classes => classes.AssignableTo(typeof(Mapper<,>)), publicOnly: false)
                    .As(type =>
                    {
                        var baseMapperType = type.BaseType;

                        return baseMapperType != null && baseMapperType.IsGenericType &&
                               baseMapperType.GetGenericTypeDefinition() == typeof(Mapper<,>)
                            ? [baseMapperType]
                            : [];
                    })
                    .WithScopedLifetime());

            return services;
        }
    }
}
