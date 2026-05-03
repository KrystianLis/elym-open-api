using OpenApi.Application.DTO;

namespace OpenApi.Application.Services;

public interface IOrdersService
{
    Task<OrderDto?> GetOrderAsync(CancellationToken cancellationToken);
}
