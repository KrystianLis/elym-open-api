namespace OpenApi.Application.DTO;

public record ProductDto(string Sku, string Name, decimal Price, string IntegrityHash);

public record ProductSourceDto(string Sku, string Name, decimal Price);
