using OpenApi.Application.DTO;

namespace OpenApi.Core.Interfaces.Clients;

public interface IUsersServiceClient
{
    public Task<IReadOnlyList<UserDto>?> GetUsersAsync();
}