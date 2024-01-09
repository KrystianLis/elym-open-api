using Org.BouncyCastle.Asn1.Ocsp;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Request = WireMock.RequestBuilders.Request;

namespace OpenApi.Tests.Integration;

internal sealed class UsersServiceApiServer : IDisposable
{
    private WireMockServer _server = null!;
    public string Url => _server.Url!;

    public void Start()
    {
        _server = WireMockServer.Start();
    }

    public void SetupServer()
    {
        _server.Given(Request.Create()
                .WithPath("/api/users")
                .UsingGet())
            .RespondWith(Response.Create()
                .WithBody(GenerateResponseBody())
                .WithHeader("content-type",
                    "application/json; charset=utf-8")
                .WithStatusCode(200));
    }

    public void Dispose()
    {
        _server.Stop();
        _server.Dispose();
    }

    private static string GenerateResponseBody()
    {
        return @"
        [
            {
                ""firstName"": ""Arno"",
                ""lastName"": ""Cartwright""
            },
            {
                ""firstName"": ""Ike"",
                ""lastName"": ""Schaefer""
            },
            {
                ""firstName"": ""Hilda"",
                ""lastName"": ""Bogan""
            },
            {
                ""firstName"": ""Mallory"",
                ""lastName"": ""Willms""
            },
            {
                ""firstName"": ""Ada"",
                ""lastName"": ""Koss""
            },
            {
                ""FirstName"": ""Ashlynn"",
                ""LastName"": ""Barrows""
            },
            {
                ""FirstName"": ""Boyd"",
                ""LastName"": ""Wolf""
            },
            {
                ""FirstName"": ""Alize"",
                ""LastName"": ""Kulas""
            },
            {
                ""FirstName"": ""Delilah"",
                ""LastName"": ""Hilll""
            },
            {
                ""FirstName"": ""Kory"",
                ""LastName"": ""Kihn""
            },
            {
                ""FirstName"": ""Amanda"",
                ""LastName"": ""Windler""
            },
            {
                ""FirstName"": ""Mya"",
                ""LastName"": ""Brown""
            },
            {
                ""FirstName"": ""Patricia"",
                ""LastName"": ""Considine""
            },
            {
                ""FirstName"": ""Constantin"",
                ""LastName"": ""Bogan""
            },
            {
                ""FirstName"": ""Trey"",
                ""LastName"": ""Morar""
            }
        ]";
    }
}