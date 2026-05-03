using System.Net.Http.Json;
using OpenApi.Application.DTO;
using OpenApi.Core.Interfaces.Clients;

namespace OpenApi.Infrastructure.Clients;

internal sealed class OrdersServiceClient : IOrdersServiceClient
{
    private readonly HttpClient _httpClient;

    public OrdersServiceClient(HttpClient httpClient) => _httpClient = httpClient;

    public Task<OrderSourceDto?> GetOrderAsync(CancellationToken cancellationToken)
        => _httpClient.GetFromJsonAsync<OrderSourceDto>("/api/order", cancellationToken);
}
