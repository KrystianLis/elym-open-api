using OpenApi.Application.DTO;

namespace OpenApi.Application.Services;

public interface IProductsService
{
    Task<IEnumerable<ProductDto>> GetProductsAsync(CancellationToken cancellationToken);
}
