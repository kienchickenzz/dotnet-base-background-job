/**
 * ProductStats represents the result of product statistics query.
 *
 * <p>This DTO holds aggregated statistics data returned from the database
 * including count, average, max and min price of products.</p>
 */

namespace BaseBackgroundJob.Persistence.BackgroundJobs.ProductStatistics;

/// <summary>
/// DTO for product statistics query result.
/// </summary>
/// <param name="TotalCount">Total number of active products.</param>
/// <param name="AveragePrice">Average price of all products.</param>
/// <param name="MaxPrice">Highest product price.</param>
/// <param name="MinPrice">Lowest product price.</param>
internal sealed record ProductStats(
    int TotalCount,
    decimal AveragePrice,
    decimal MaxPrice,
    decimal MinPrice);
