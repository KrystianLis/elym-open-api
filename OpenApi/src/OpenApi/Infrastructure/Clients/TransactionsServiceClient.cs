using System.Net.Http.Json;
using OpenApi.Application.DTO;
using OpenApi.Core.Interfaces.Clients;

namespace OpenApi.Infrastructure.Clients;

internal sealed class TransactionsServiceClient : ITransactionsServiceClient
{
    private readonly HttpClient _httpClient;

    public TransactionsServiceClient(HttpClient httpClient) => _httpClient = httpClient;

    public Task<IReadOnlyList<TransactionSourceDto>?> GetTransactionsAsync(CancellationToken cancellationToken)
        => _httpClient.GetFromJsonAsync<IReadOnlyList<TransactionSourceDto>>("/api/transactions", cancellationToken);
}
