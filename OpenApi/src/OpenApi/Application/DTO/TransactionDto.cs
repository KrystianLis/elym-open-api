namespace OpenApi.Application.DTO;

public record TransactionDto(
    string Id,
    string From,
    string To,
    decimal Amount,
    string At,
    string PrevHash,
    string Hash);

public record TransactionSourceDto(string Id, string From, string To, decimal Amount, string At);
