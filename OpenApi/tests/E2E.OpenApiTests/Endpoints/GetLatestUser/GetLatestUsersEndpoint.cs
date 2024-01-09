namespace E2E.OpenApiTests.Endpoints.GetLatestUser;

public class GetLatestUsersEndpoint : BaseEndpoint
{
    private const string Path = "latest-users";
    private readonly HttpClient _httpClient;

    public GetLatestUsersEndpoint(Uri? url)
    {
        _httpClient = GetHttpClient();
        _httpClient.BaseAddress = url;
    }

    public async Task<HttpResponseMessage?> GetLatestUsersAsync()
    {
        var response = await _httpClient.GetAsync(Path);
        return response;
    }
}