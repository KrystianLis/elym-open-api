using System.Net.Http.Json;
using OpenApi.Application.DTO;
using OpenApi.Core.Interfaces.Clients;

namespace OpenApi.Infrastructure.Clients;

internal sealed class ProductsServiceClient : IProductsServiceClient
{
    private readonly HttpClient _httpClient;

    public ProductsServiceClient(HttpClient httpClient) => _httpClient = httpClient;

    public Task<IReadOnlyList<ProductSourceDto>?> GetProductsAsync(CancellationToken cancellationToken)
        => _httpClient.GetFromJsonAsync<IReadOnlyList<ProductSourceDto>>("/api/products", cancellationToken);
}
