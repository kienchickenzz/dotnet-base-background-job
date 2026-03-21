namespace BaseBackgroundJob.Domain.AggregatesModels.Products;

using BaseBackgroundJob.Domain.Common;


public static class ProductErrors
{
    public static Error NotFound = new(
        "Product.NotFound",
        "Product not found!");
}
