using OpenApi.Application.DTO;

namespace OpenApi.Core.Interfaces.Clients;

public interface IAuditEventsServiceClient
{
    Task<IReadOnlyList<AuditEventSourceDto>?> GetAuditEventsAsync(CancellationToken cancellationToken);
}
