using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Soms.Shared.CORS;

public static class AppExtensions
{
    public static IApplicationBuilder UseCors(this IApplicationBuilder app)
    {
        var config = app.ApplicationServices.GetRequiredService<IConfiguration>();
        var corsOptions = new CorsOptions();
        config.GetSection("cors").Bind(corsOptions);
        
        if (corsOptions == null)
        {
            throw new ArgumentException("corsOptions must be set");
        }
        
        if (!corsOptions.IsEnabled) return app;
        
        corsOptions.Name = string.IsNullOrWhiteSpace(corsOptions.Name) ? Constants.DefaultPolicyName : corsOptions.Name;
        app.UseCors(corsOptions.Name);
        return app;
        
    }
}