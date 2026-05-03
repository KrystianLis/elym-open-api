using System.Globalization;
using OpenApi.Application.DTO;
using OpenApi.Core.Interfaces;
using OpenApi.Core.Interfaces.Clients;

namespace OpenApi.Application.Services;

internal sealed class OrdersService : IOrdersService
{
    private readonly IOrdersServiceClient _client;
    private readonly IHashService _hashService;

    public OrdersService(IOrdersServiceClient client, IHashService hashService)
    {
        _client = client;
        _hashService = hashService;
    }

    public async Task<OrderDto?> GetOrderAsync(CancellationToken cancellationToken)
    {
        var source = await _client.GetOrderAsync(cancellationToken);
        if (source is null) return null;

        var lines = source.Lines.Select(l => new OrderLineDto(
            l.Sku,
            l.Quantity,
            l.Price,
            _hashService.Compute(
                l.Sku,
                l.Quantity.ToString(CultureInfo.InvariantCulture),
                l.Price.ToString(CultureInfo.InvariantCulture)))).ToList();

        var total = lines.Sum(l => l.Price * l.Quantity);
        var linesHash = _hashService.Compute(lines.Select(l => l.LineHash).ToArray());
        var signature = _hashService.Compute(
            source.Id,
            source.Customer,
            total.ToString(CultureInfo.InvariantCulture),
            linesHash);

        return new OrderDto(source.Id, source.Customer, lines, total, signature);
    }
}
