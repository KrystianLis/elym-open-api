using OpenApi.Application.DTO;

namespace OpenApi.Application.Services;

public interface IAuditEventsService
{
    Task<IEnumerable<AuditEventDto>> GetAuditEventsAsync(CancellationToken cancellationToken);
}
