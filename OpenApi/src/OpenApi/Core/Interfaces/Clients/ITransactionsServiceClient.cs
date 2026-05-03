using OpenApi.Application.DTO;

namespace OpenApi.Core.Interfaces.Clients;

public interface ITransactionsServiceClient
{
    Task<IReadOnlyList<TransactionSourceDto>?> GetTransactionsAsync(CancellationToken cancellationToken);
}
