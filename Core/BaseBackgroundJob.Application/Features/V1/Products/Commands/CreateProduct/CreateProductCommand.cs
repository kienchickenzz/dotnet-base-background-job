namespace BaseBackgroundJob.Application.Features.V1.Products.Commands.CreateProduct;

using BaseBackgroundJob.Application.Common.Messaging;


public sealed record CreateProductCommand(
    string Name,
    string? Description,
    decimal Price)
    : ICommand<int>;
