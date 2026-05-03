using OpenApi.Application.DTO;

namespace OpenApi.Application.Services;

public interface ITransactionsService
{
    Task<IEnumerable<TransactionDto>> GetTransactionsAsync(CancellationToken cancellationToken);
}
