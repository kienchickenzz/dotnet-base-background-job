namespace BaseBackgroundJob.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using BaseBackgroundJob.Application.Common.ApplicationServices.Persistence;
using BaseBackgroundJob.Application.Common.ApplicationServices.BackgroundJob;
using BaseBackgroundJob.Persistence.Common;
using BaseBackgroundJob.Persistence.Repositories;
using BaseBackgroundJob.Persistence.DatabaseContext;
using BaseBackgroundJob.Persistence.BackgroundJobs.ProductStatistics;


public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructurePersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection") ??
                                  throw new ArgumentNullException(nameof(configuration));


        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<ISqlConnectionFactory>(_ =>
            new SqlConnectionFactory(connectionString));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddRepositories();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }

    /// <summary>
    /// Registers the ProductStatisticsJob as a recurring Hangfire job.
    /// Runs every 2 minutes to collect and log product statistics.
    /// </summary>
    public static void AddProductStatisticsJob(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var jobService = scope.ServiceProvider.GetRequiredService<IJobService>();

        // Cron expression: */1 * * * * = every 1 minutes
        jobService.Recurring<ProductStatisticsJob>(
            jobName: "ProductStatistics",
            methodCall: job => job.Execute(),
            cronExpression: "*/1 * * * *");
    }
}
