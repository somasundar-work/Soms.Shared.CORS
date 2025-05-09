using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Soms.Shared.Cors;

public static class AppExtensions
{
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
