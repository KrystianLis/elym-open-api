namespace OpenApi.Application.DTO;

public record OrderLineDto(string Sku, int Quantity, decimal Price, string LineHash);

public record OrderDto(string Id, string Customer, IReadOnlyList<OrderLineDto> Lines, decimal Total, string Signature);

public record OrderSourceLineDto(string Sku, int Quantity, decimal Price);

public record OrderSourceDto(string Id, string Customer, IReadOnlyList<OrderSourceLineDto> Lines);
