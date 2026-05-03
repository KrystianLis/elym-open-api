using System.Globalization;
using OpenApi.Application.DTO;
using OpenApi.Core.Interfaces;
using OpenApi.Core.Interfaces.Clients;

namespace OpenApi.Application.Services;

internal sealed class ProductsService : IProductsService
{
    private readonly IProductsServiceClient _client;
    private readonly IHashService _hashService;

    public ProductsService(IProductsServiceClient client, IHashService hashService)
    {
        _client = client;
        _hashService = hashService;
    }

    public async Task<IEnumerable<ProductDto>> GetProductsAsync(CancellationToken cancellationToken)
    {
        var source = await _client.GetProductsAsync(cancellationToken) ?? Array.Empty<ProductSourceDto>();
        return source.Select(p => new ProductDto(
            p.Sku,
            p.Name,
            p.Price,
            _hashService.Compute(p.Sku, p.Name, p.Price.ToString(CultureInfo.InvariantCulture))));
    }
}
