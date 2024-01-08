using OpenApi.Application.DTO;

namespace OpenApi.Application.Services;

public interface IDataAggregatorService
{
    Task<IEnumerable<UserDto>> GetLatestUsersAsync(CancellationToken token);
}