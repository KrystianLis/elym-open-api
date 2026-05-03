namespace OpenApi.Core.Interfaces;

public interface IHashService
{
    string Compute(params string?[] parts);
}
