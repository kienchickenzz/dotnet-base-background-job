namespace BaseBackgroundJob.Application.Features.V1.Products.Queries.GetProductById;

using BaseBackgroundJob.Application.Common.Messaging;
using BaseBackgroundJob.Application.Features.V1.Products.Models.Responses;


public sealed record GetProductByIdQuery(int Id) : IQuery<ProductResponse>;
