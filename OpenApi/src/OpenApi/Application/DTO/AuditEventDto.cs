namespace OpenApi.Application.DTO;

public record AuditEventDto(string Id, string Actor, string Action, string At, string Checksum);

public record AuditEventSourceDto(string Id, string Actor, string Action, string At);
