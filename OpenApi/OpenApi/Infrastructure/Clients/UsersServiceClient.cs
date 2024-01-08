using OpenApi.Application.DTO;
using OpenApi.Core.Interfaces.Clients;

namespace OpenApi.Infrastructure.Clients;

public class UsersServiceClient : IUsersServiceClient
{
    private readonly HttpClient _httpClient;

    public UsersServiceClient(HttpClient httpClient)
        => _httpClient = httpClient;
    
    public async Task<IReadOnlyList<UserDto>?> GetUsersAsync()
        => await _httpClient.GetFromJsonAsync<IReadOnlyList<UserDto>>($"/api/users");
}