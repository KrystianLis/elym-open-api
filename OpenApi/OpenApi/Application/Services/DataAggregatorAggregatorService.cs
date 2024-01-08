using Newtonsoft.Json;
using OpenApi.Application.DTO;
using OpenApi.Core.Entities;
using OpenApi.Core.Interfaces.Clients;
using OpenApi.Core.Interfaces.Repositories;

namespace OpenApi.Application.Services;

public class DataAggregatorAggregatorService : IDataAggregatorService
{
    private readonly IUsersServiceClient _usersServiceClient;
    private readonly IUserRepository _userRepository;

    public DataAggregatorAggregatorService(IUsersServiceClient usersServiceClient, IUserRepository userRepository)
    {
        _usersServiceClient = usersServiceClient;
        _userRepository = userRepository;
    }
    
    public async Task<IEnumerable<UserDto>> GetLatestUsersAsync(CancellationToken token = default)
    {
         var usersResponse = await _usersServiceClient.GetUsersAsync();
        
        if (usersResponse is not null && usersResponse.Any())
        {
            var users = usersResponse!.Select(x => new User
            {
                Id = Guid.NewGuid(),
                FirstName = x.FirstName,
                LastName = x.LastName
            });
            
            await _userRepository.AddUsersAsync(users, token);
        }

        var latestUsers = await _userRepository.GetLatestUsersAsync(token);

        return latestUsers!.Select(Map);
    }
    
    private static UserDto Map(User entity)
        => new(entity.FirstName, entity.LastName);
}