using OpenApi.Application.DTO;
using OpenApi.Core.Interfaces;

namespace OpenApi.Application.Services;

internal sealed class QuotesService : IQuotesService
{
    private static readonly (string Author, string Text)[] Quotes =
    {
        ("Alan Kay", "The best way to predict the future is to invent it."),
        ("Edsger W. Dijkstra", "Simplicity is prerequisite for reliability."),
        ("Donald Knuth", "Premature optimization is the root of all evil."),
        ("Linus Torvalds", "Talk is cheap. Show me the code."),
        ("Grace Hopper", "The most dangerous phrase in the language is 'we've always done it this way'."),
        ("Brian Kernighan", "Debugging is twice as hard as writing the code in the first place."),
        ("Phil Karlton", "There are only two hard things in Computer Science: cache invalidation and naming things."),
        ("Martin Fowler", "Any fool can write code that a computer can understand. Good programmers write code that humans can understand."),
    };

    private readonly IHashService _hashService;

    public QuotesService(IHashService hashService)
    {
        _hashService = hashService;
    }

    public QuoteDto GetRandomQuote()
    {
        var (author, text) = Quotes[Random.Shared.Next(Quotes.Length)];
        return new QuoteDto(author, text, _hashService.Compute(author, text));
    }
}
