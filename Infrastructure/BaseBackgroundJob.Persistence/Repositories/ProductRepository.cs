/**
 * Repository implementation for Product aggregate.
 *
 * <p>Inherits all CRUD operations from base Repository.
 * Add domain-specific query methods here if needed.</p>
 */
namespace BaseBackgroundJob.Persistence.Repositories;

using BaseBackgroundJob.Application.Common.ApplicationServices.Persistence;
using BaseBackgroundJob.Domain.AggregatesModels.Products;
using BaseBackgroundJob.Persistence.Common;
using BaseBackgroundJob.Persistence.DatabaseContext;


public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
