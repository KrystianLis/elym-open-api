using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using OpenApi.Application.DTO;
using Xunit;

namespace OpenApi.Tests.Integration.GetLatestUsers;

public class GetLatestUsersTests : BaseIntegrationTests
{
    private readonly HttpClient _client;

    public GetLatestUsersTests(IntegrationWebAppFactory apiFactory)
        : base(apiFactory)
    {
        _client = apiFactory.CreateClient();
    }

    [Fact]
    public async Task GivenLatestUsersEndpoint_WhenLatestLimitIsFive_ThenFiveLatestUsers()
    {
        // Arrange
        const int latestLimit = 5;

        // Act
        var response = await _client.GetAsync("/latest-users");

        // Assert
        var dbUserResponse = await DbContext.Users
            .OrderByDescending(u => u.CreatedAt)
            .Take(latestLimit)
            .ToListAsync();

        var expectedResult = dbUserResponse.Select(x => new UserDto(x.FirstName, x.LastName));

        var users = await response.Content.ReadFromJsonAsync<IReadOnlyCollection<UserDto>>();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        users.Should().BeEquivalentTo(expectedResult);
    }
}