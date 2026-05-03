using System.Net.Http.Json;
using OpenApi.Application.DTO;
using OpenApi.Core.Interfaces.Clients;

namespace OpenApi.Infrastructure.Clients;

internal sealed class AuditEventsServiceClient : IAuditEventsServiceClient
{
    private readonly HttpClient _httpClient;

    public AuditEventsServiceClient(HttpClient httpClient) => _httpClient = httpClient;

    public Task<IReadOnlyList<AuditEventSourceDto>?> GetAuditEventsAsync(CancellationToken cancellationToken)
        => _httpClient.GetFromJsonAsync<IReadOnlyList<AuditEventSourceDto>>("/api/audit-events", cancellationToken);
}
