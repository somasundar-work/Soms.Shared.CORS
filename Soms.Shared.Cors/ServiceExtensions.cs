using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Soms.Shared.Cors;

public static class ServiceExtensions
{
    public static IServiceCollection ConfigureAppCors(this IServiceCollection services)
    {
        var config = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

        var CorsOptions = new CorsOptions();
        config.GetSection("Cors").Bind(CorsOptions);

        if (CorsOptions == null)
        {
            throw new ArgumentException("CorsOptions must be set");
        }

        if (!CorsOptions.IsEnabled)
            return services;

        CorsOptions.Name = string.IsNullOrWhiteSpace(CorsOptions.Name)
            ? Constants.DefaultPolicyName
            : CorsOptions.Name;

        if (CorsOptions.Origins == null || CorsOptions.Origins.Count == 0)
        {
            throw new ArgumentException("Origins must be set");
        }

        services.AddCors(policy =>
        {
            policy.AddPolicy(
                CorsOptions.Name,
                builder =>
                {
                    if (CorsOptions.Origins.Count > 0)
                    {
                        builder.WithOrigins(CorsOptions.Origins.ToArray());
                    }

                    if (CorsOptions.Methods.Count > 0)
                    {
                        builder.WithMethods(CorsOptions.Methods.ToArray());
                    }
                    else
                    {
                        builder.AllowAnyMethod();
                    }

                    if (CorsOptions.Headers.Count > 0)
                    {
                        builder.WithHeaders(CorsOptions.Headers.ToArray());
                    }
                    else
                    {
                        builder.AllowAnyHeader();
                    }

                    if (CorsOptions.ExposedHeaders.Count > 0)
                    {
                        builder.WithExposedHeaders(CorsOptions.ExposedHeaders.ToArray());
                    }

                    if (CorsOptions.AllowCredentials)
                    {
                        builder.AllowCredentials();
                    }
                }
            );
        });

        return services;
    }
}
