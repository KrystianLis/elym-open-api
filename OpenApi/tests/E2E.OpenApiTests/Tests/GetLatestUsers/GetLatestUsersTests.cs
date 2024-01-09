using System.Net;
using System.Net.Http.Json;
using E2E.OpenApiTests.Configuration;
using E2E.OpenApiTests.Endpoints.GetLatestUser;
using FluentAssertions;
using OpenApi.Application.DTO;
using Xunit;

namespace E2E.OpenApiTests.Tests.GetLatestUsers;

public class GetLatestUsersTests
{
    private readonly GetLatestUsersEndpoint _getLatestUsersEndpoint;

    public GetLatestUsersTests()
    {
        _getLatestUsersEndpoint = new GetLatestUsersEndpoint(TestConfigurationProvider.BaseUrl);
    }
    
    [Fact]
    public async Task GivenGetLatestUsersEndpoint_WhenSendRequest_ThenReturnsTheNewestUsers()
    {
        // Act
        var response = await _getLatestUsersEndpoint.GetLatestUsersAsync();
        
        // Assert
        var users = await response!.Content.ReadFromJsonAsync<IReadOnlyCollection<UserDto>>();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        users!.Count.Should().Be(TestConfigurationProvider.LastLimit);
    }
}