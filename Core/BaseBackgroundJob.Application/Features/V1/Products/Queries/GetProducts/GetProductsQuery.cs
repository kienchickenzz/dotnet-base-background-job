namespace BaseBackgroundJob.Application.Features.V1.Products.Queries.GetProducts;

using BaseBackgroundJob.Application.Common.Messaging;
using BaseBackgroundJob.Application.Common.Models;
using BaseBackgroundJob.Application.Features.V1.Products.Models.Responses;


public sealed class GetProductsQuery : PaginationFilter, IQuery<PaginationResponse<ProductResponse>>
{
}
