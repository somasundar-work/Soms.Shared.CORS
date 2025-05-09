using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Soms.Shared.Cors;

/// <summary>
/// Provides extension methods for configuring CORS settings in an ASP.NET Core application.
/// </summary>
public static class ServiceExtensions
{
    /// <summary>
    /// Configures Cross-Origin Resource Sharing (CORS) for the application based on the "Cors" section
    /// in the <see cref="IConfiguration"/>. Adds a named CORS policy to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to add CORS services to.</param>
    /// <returns>The original <see cref="IServiceCollection"/> with CORS configured if enabled.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown if CORS is enabled but required options such as "Origins" are not specified.
    /// </exception>
    /// <remarks>
    /// The configuration section "Cors" must be present in the appsettings.json or equivalent configuration source.
    /// Example configuration:
    /// <code>
    /// "Cors": {
    ///   "IsEnabled": true,
    ///   "Name": "MyPolicy",
    ///   "Origins": [ "https://example.com" ],
    ///   "Methods": [ "GET", "POST" ],
    ///   "Headers": [ "Content-Type" ],
    ///   "ExposedHeaders": [ "X-Custom-Header" ],
    ///   "AllowCredentials": true
    /// }
    /// </code>
    /// </remarks>
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
