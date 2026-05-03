namespace OpenApi.Application.DTO;

public record DeviceDto(string MacAddress, string Firmware, string Fingerprint);

public record DeviceSourceDto(string MacAddress, string Firmware);
