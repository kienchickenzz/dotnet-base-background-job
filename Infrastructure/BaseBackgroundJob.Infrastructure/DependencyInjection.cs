namespace BaseBackgroundJob.Infrastructure;

using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using BaseBackgroundJob.Application.Common.ApplicationServices.BackgroundJob;
using BaseBackgroundJob.Infrastructure.BackgroundJobs;


public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services
            ._AddBackgroundJobs(config)
            ._AddServices();

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder, IConfiguration configuration)
    {
        builder
            ._UseHangfireDashboard(configuration);

        return builder;
    }

    private static IServiceCollection _AddServices(this IServiceCollection services)
    {
        services.AddScoped<IJobService, HangfireService>();

        return services;
    }

    internal static IServiceCollection _AddBackgroundJobs(this IServiceCollection services, IConfiguration config)
    {
        services.AddHangfireServer();

        services.AddHangfire(hangfireConfig => hangfireConfig
            .UseSqlServerStorage(config.GetConnectionString("DefaultConnection")) // Lưu jobs vào SQL Server 
            .UseFilter(new LogJobFilter())); // Gắn filter để log job lifecycle  

        return services;
    }

    private static IApplicationBuilder _UseHangfireDashboard(this IApplicationBuilder app, IConfiguration config)
    {
        var dashboardOptions = config.GetSection("HangfireSettings:Dashboard").Get<DashboardOptions>();
        if (dashboardOptions is null) throw new Exception("Hangfire Dashboard is not configured.");
        dashboardOptions.Authorization = new[]
        {
           new HangfireCustomBasicAuthenticationFilter
           {
                User = config.GetSection("HangfireSettings:Credentials:User").Value!,
                Pass = config.GetSection("HangfireSettings:Credentials:Password").Value!
           }
        };

        return app.UseHangfireDashboard(config["HangfireSettings:Route"], dashboardOptions);
    }
}
