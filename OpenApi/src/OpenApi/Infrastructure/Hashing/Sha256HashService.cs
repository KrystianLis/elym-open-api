using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using OpenApi.Core.Interfaces;

namespace OpenApi.Infrastructure.Hashing;

internal sealed class Sha256HashService : IHashService
{
    private readonly byte[] _key;

    public Sha256HashService(IOptions<HashOptions> options)
    {
        _key = Encoding.UTF8.GetBytes(options.Value.SecretKey);
    }

    public string Compute(params string?[] parts)
    {
        var canonical = string.Join("|", parts.Select(p => p ?? string.Empty));
        var bytes = Encoding.UTF8.GetBytes(canonical);
        var hash = HMACSHA256.HashData(_key, bytes);
        return Convert.ToHexString(hash).ToLowerInvariant();
    }
}
