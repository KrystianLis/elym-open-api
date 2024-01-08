using OpenApi.Core.Entities;

namespace OpenApi.Core.Interfaces.Repositories;

public interface IUserRepository
{
    public Task AddUsersAsync(IEnumerable<User> users, CancellationToken cancellationToken);

    Task<IEnumerable<User>> GetLatestUsersAsync(CancellationToken cancellationToken);
}