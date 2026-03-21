namespace BaseBackgroundJob.Application.Features.V1.Products.Commands.DeleteProduct;

using BaseBackgroundJob.Application.Common.Messaging;
using BaseBackgroundJob.Application.Common.ApplicationServices.Persistence;
using BaseBackgroundJob.Domain.AggregatesModels.Products;
using BaseBackgroundJob.Domain.Common;


public sealed class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, int>
{
    private readonly IProductRepository _productRepository;
    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository= productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<Result<int>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product == null)
            return Result.Failure<int>(ProductErrors.NotFound);

        await _productRepository.DeleteAsync(product);

        return product.Id;
    }
}
