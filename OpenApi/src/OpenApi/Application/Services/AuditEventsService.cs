using OpenApi.Application.DTO;
using OpenApi.Core.Interfaces;
using OpenApi.Core.Interfaces.Clients;

namespace OpenApi.Application.Services;

internal sealed class AuditEventsService : IAuditEventsService
{
    private readonly IAuditEventsServiceClient _client;
    private readonly IHashService _hashService;

    public AuditEventsService(IAuditEventsServiceClient client, IHashService hashService)
    {
        _client = client;
        _hashService = hashService;
    }

    public async Task<IEnumerable<AuditEventDto>> GetAuditEventsAsync(CancellationToken cancellationToken)
    {
        var source = await _client.GetAuditEventsAsync(cancellationToken) ?? Array.Empty<AuditEventSourceDto>();
        return source.Select(e => new AuditEventDto(
            e.Id,
            e.Actor,
            e.Action,
            e.At,
            _hashService.Compute(e.Id, e.Actor, e.Action, e.At)));
    }
}
