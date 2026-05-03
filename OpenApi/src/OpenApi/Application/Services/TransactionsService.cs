using System.Globalization;
using OpenApi.Application.DTO;
using OpenApi.Core.Interfaces;
using OpenApi.Core.Interfaces.Clients;

namespace OpenApi.Application.Services;

internal sealed class TransactionsService : ITransactionsService
{
    private const string GenesisHash = "0";

    private readonly ITransactionsServiceClient _client;
    private readonly IHashService _hashService;

    public TransactionsService(ITransactionsServiceClient client, IHashService hashService)
    {
        _client = client;
        _hashService = hashService;
    }

    public async Task<IEnumerable<TransactionDto>> GetTransactionsAsync(CancellationToken cancellationToken)
    {
        var source = await _client.GetTransactionsAsync(cancellationToken) ?? Array.Empty<TransactionSourceDto>();
        var result = new List<TransactionDto>(source.Count);
        var prevHash = GenesisHash;

        foreach (var t in source)
        {
            var hash = _hashService.Compute(
                prevHash,
                t.Id,
                t.From,
                t.To,
                t.Amount.ToString(CultureInfo.InvariantCulture),
                t.At);

            result.Add(new TransactionDto(t.Id, t.From, t.To, t.Amount, t.At, prevHash, hash));
            prevHash = hash;
        }

        return result;
    }
}
