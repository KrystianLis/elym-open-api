using OpenApi.Application.DTO;

namespace OpenApi.Core.Interfaces.Clients;

public interface IOrdersServiceClient
{
    Task<OrderSourceDto?> GetOrderAsync(CancellationToken cancellationToken);
}
