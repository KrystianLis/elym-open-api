using OpenApi.Application.DTO;

namespace OpenApi.Application.Services;

public interface IQuotesService
{
    QuoteDto GetRandomQuote();
}
