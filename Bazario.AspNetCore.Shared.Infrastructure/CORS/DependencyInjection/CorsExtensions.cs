﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Bazario.AspNetCore.Shared.Infrastructure.CORS.DependencyInjection
{
    public static class CorsExtensions
    {
        public const string AllowSpecificOrigins = "AllowSpecificOrigins";

        public static IServiceCollection ConfigureCors(
            this IServiceCollection services,
            params string[] allowedOrigins)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(AllowSpecificOrigins, policy =>
                {
                    policy.WithOrigins(allowedOrigins)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            return services;
        }

        public static IApplicationBuilder UseAppCors(this IApplicationBuilder app)
        {
            app.UseCors(AllowSpecificOrigins);

            return app;
        }
    }
}
