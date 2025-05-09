using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Soms.Shared.Cors;

/// <summary>
/// Provides extension methods for configuring CORS in the application.
/// </summary>
public static class AppExtensions
{
    /// <summary>
    /// Applies the configured CORS policy to the application pipeline.
    /// </summary>
    /// <param name="app">The application builder instance.</param>
    /// <returns>The application builder with CORS middleware configured, if enabled.</returns>
    /// <exception cref="ArgumentException">Thrown if the CORS configuration section is missing or invalid.</exception>
    public static IApplicationBuilder UseAppCors(this IApplicationBuilder app)
    {
        var config = app.ApplicationServices.GetRequiredService<IConfiguration>();

        var CorsOptions = new CorsOptions();
        config.GetSection("Cors").Bind(CorsOptions);

        if (CorsOptions == null)
        {
            throw new ArgumentException("CorsOptions must be set");
        }

        if (!CorsOptions.IsEnabled)
            return app;

        CorsOptions.Name = string.IsNullOrWhiteSpace(CorsOptions.Name)
            ? Constants.DefaultPolicyName
            : CorsOptions.Name;

        app.UseCors(CorsOptions.Name);

        return app;
    }
}
