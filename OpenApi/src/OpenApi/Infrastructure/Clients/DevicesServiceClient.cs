using System.Net.Http.Json;
using OpenApi.Application.DTO;
using OpenApi.Core.Interfaces.Clients;

namespace OpenApi.Infrastructure.Clients;

internal sealed class DevicesServiceClient : IDevicesServiceClient
{
    private readonly HttpClient _httpClient;

    public DevicesServiceClient(HttpClient httpClient) => _httpClient = httpClient;

    public Task<IReadOnlyList<DeviceSourceDto>?> GetDevicesAsync(CancellationToken cancellationToken)
        => _httpClient.GetFromJsonAsync<IReadOnlyList<DeviceSourceDto>>("/api/devices", cancellationToken);
}
