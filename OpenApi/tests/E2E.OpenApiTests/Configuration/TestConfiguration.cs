namespace E2E.OpenApiTests.Configuration;

public class TestConfiguration
{
    public Uri BaseUrl { get; set; } = default!;

    public int LastLimit { get; set; } = default;
}