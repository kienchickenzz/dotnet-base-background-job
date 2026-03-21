namespace BaseBackgroundJob.Application.Features.V1.Products.Commands.DeleteProduct;

using BaseBackgroundJob.Application.Common.Messaging;


public sealed record DeleteProductCommand(int Id) : ICommand<int>;
