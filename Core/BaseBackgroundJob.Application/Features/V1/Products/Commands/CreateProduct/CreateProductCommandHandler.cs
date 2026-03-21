namespace BaseBackgroundJob.Application.Features.V1.Products.Commands.CreateProduct;

using BaseBackgroundJob.Application.Common.Messaging;
using BaseBackgroundJob.Application.Common.ApplicationServices.Persistence;
using BaseBackgroundJob.Domain.AggregatesModels.Products;
using BaseBackgroundJob.Domain.Common;


public sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, int>
{
    private readonly IProductRepository _productRepository;
    public CreateProductCommandHandler(
        IProductRepository productRepository
    )
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<Result<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = Product.Create(
            request.Name,
            request.Description,
            request.Price);

        var result = await _productRepository.AddAsync(product);

        return result.Id;
    }
}
