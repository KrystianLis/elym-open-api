using OpenApi.Application.DTO;

namespace OpenApi.Application.Services;

public interface IDevicesService
{
    Task<IEnumerable<DeviceDto>> GetDevicesAsync(CancellationToken cancellationToken);
}
