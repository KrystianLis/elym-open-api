using OpenApi.Application.DTO;

namespace OpenApi.Core.Interfaces.Clients;

public interface IDevicesServiceClient
{
    Task<IReadOnlyList<DeviceSourceDto>?> GetDevicesAsync(CancellationToken cancellationToken);
}
