using OpenApi.Application.DTO;

namespace OpenApi.Core.Interfaces.Clients;

public interface IProductsServiceClient
{
    Task<IReadOnlyList<ProductSourceDto>?> GetProductsAsync(CancellationToken cancellationToken);
}
