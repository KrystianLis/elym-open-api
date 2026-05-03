using OpenApi.Application.DTO;
using OpenApi.Core.Interfaces;
using OpenApi.Core.Interfaces.Clients;

namespace OpenApi.Application.Services;

internal sealed class DevicesService : IDevicesService
{
    private readonly IDevicesServiceClient _client;
    private readonly IHashService _hashService;

    public DevicesService(IDevicesServiceClient client, IHashService hashService)
    {
        _client = client;
        _hashService = hashService;
    }

    public async Task<IEnumerable<DeviceDto>> GetDevicesAsync(CancellationToken cancellationToken)
    {
        var source = await _client.GetDevicesAsync(cancellationToken) ?? Array.Empty<DeviceSourceDto>();
        return source.Select(d => new DeviceDto(
            d.MacAddress,
            d.Firmware,
            _hashService.Compute(d.MacAddress, d.Firmware)));
    }
}
