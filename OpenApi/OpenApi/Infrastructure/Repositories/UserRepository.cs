using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OpenApi.Core.Entities;
using OpenApi.Core.Interfaces.Repositories;
using OpenApi.Infrastructure.Data;

namespace OpenApi.Infrastructure.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly ConfOptions _confOptions;
    private readonly ApplicationDbContext _context;
    private readonly DbSet<User> _users;

    public UserRepository(ApplicationDbContext context, IOptions<ConfOptions> options)
    {
        _context = context;
        _users = context.Users;
        _confOptions = options.Value;
    }

    public async Task AddUsersAsync(IEnumerable<User> users, CancellationToken cancellationToken = default)
    {
        await _users.AddRangeAsync(users, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<User>> GetLatestUsersAsync(CancellationToken cancellationToken = default)
    {
        return await _users
            .AsNoTracking()
            .OrderByDescending(u => u.CreatedAt)
            .Take(_confOptions.LastLimit)
            .ToListAsync(cancellationToken);
    }
}