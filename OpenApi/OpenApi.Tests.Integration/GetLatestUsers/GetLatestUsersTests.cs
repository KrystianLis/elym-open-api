using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace OpenApi.Tests.Integration.GetLatestUsers;

public class GetLatestUsersTests : IClassFixture<IntegrationWebAppFactory>
{
    private readonly IntegrationWebAppFactory _apiFactory;

    public GetLatestUsersTests(IntegrationWebAppFactory apiFactory)
    {
        _apiFactory = apiFactory;
    }

    [Fact]
    public async Task Test()
    {
        await Task.Delay(5000);
    }
}