/**
 * ProductStatisticsJob collects and logs product statistics periodically.
 *
 * <p>This background job runs every 2 minutes to query product data
 * from the database and output basic statistics to the console/log.</p>
 */

namespace BaseBackgroundJob.Persistence.BackgroundJobs.ProductStatistics;

using Dapper;
using Microsoft.Extensions.Logging;

using BaseBackgroundJob.Application.Common.ApplicationServices.Persistence;

/// <summary>
/// Background job that queries product statistics from database and logs to console.
/// </summary>
internal sealed class ProductStatisticsJob
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly ILogger<ProductStatisticsJob> _logger;

    public ProductStatisticsJob(
        ISqlConnectionFactory sqlConnectionFactory,
        ILogger<ProductStatisticsJob> logger)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _logger = logger;
    }

    /// <summary>
    /// Executes the job: queries product stats and logs to console.
    /// </summary>
    public async Task Execute()
    {
        _logger.LogInformation("[ProductStatisticsJob] Starting...");

        using var connection = _sqlConnectionFactory.CreateConnection();

        var stats = await connection.QuerySingleOrDefaultAsync<ProductStats>(
                @"SELECT
                COUNT(*) AS TotalCount,
                ISNULL(AVG(Price), 0) AS AveragePrice,
                ISNULL(MAX(Price), 0) AS MaxPrice,
                ISNULL(MIN(Price), 0) AS MinPrice
            FROM Products
            WHERE DeletedOn IS NULL");

        if (stats is null)
        {
            _logger.LogWarning("[ProductStatisticsJob] No data returned from query");
            return;
        }

        _logger.LogInformation(
            "[ProductStatisticsJob] Stats: Total={TotalCount}, AvgPrice={AveragePrice:F2}, MaxPrice={MaxPrice:F2}, MinPrice={MinPrice:F2}",
            stats.TotalCount,
            stats.AveragePrice,
            stats.MaxPrice,
            stats.MinPrice);

        _logger.LogInformation("[ProductStatisticsJob] Completed");
    }
}
