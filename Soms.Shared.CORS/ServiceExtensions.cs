using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Soms.Shared.CORS;

public static class ServiceExtensions
{
    public static IServiceCollection ConfigureCors(this IServiceCollection services)
    {
        var corsOptions = new CorsOptions();
        var config = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        config.GetSection("cors").Bind(corsOptions);

        if (corsOptions == null)
        {
            throw new ArgumentException("corsOptions must be set");
        }
        
        if (!corsOptions.IsEnabled) return services;
        
        corsOptions.Name = string.IsNullOrWhiteSpace(corsOptions.Name) ? Constants.DefaultPolicyName : corsOptions.Name;
        
        if (corsOptions.Origins == null || corsOptions.Origins.Count == 0)
        {
            throw new ArgumentException("Origins must be set");
        }
        services.AddCors(policy =>
        {
            policy.AddPolicy(
                corsOptions.Name,
                builder =>
                {
                    if (corsOptions.Origins.Count > 0)
                    {
                        builder.WithOrigins(corsOptions.Origins.ToArray());
                    }

                    if (corsOptions.Methods.Count > 0)
                    {
                        builder.WithMethods(corsOptions.Methods.ToArray());
                    }
                    else
                    {
                        builder.AllowAnyMethod();
                    }
                        
                    if (corsOptions.Headers.Count > 0)
                    {
                        builder.WithHeaders(corsOptions.Headers.ToArray());
                    }
                    else
                    {
                        builder.AllowAnyHeader();
                    }

                    if (corsOptions.ExposedHeaders.Count > 0)
                    {
                        builder.WithExposedHeaders(corsOptions.ExposedHeaders.ToArray());
                    }

                    if (corsOptions.AllowCredentials)
                    {
                        builder.AllowCredentials();
                    }
                });
        });
        return services;
    }
}