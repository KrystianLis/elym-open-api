using Microsoft.Extensions.Configuration;

namespace E2E.OpenApiTests.Configuration;

public static class TestConfigurationProvider
{
    private static readonly Lazy<IConfiguration> ConfigurationSection = new(BuildConfiguration());
    private static TestConfiguration? _configuration;

    private static IConfiguration BuildConfiguration()
    {
        var configurationBuilder = new ConfigurationBuilder();
        return configurationBuilder
            .AddJsonFile("testConfiguration.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
    }

    private static TestConfiguration Configuration
    {
        get
        {
            if (_configuration == null)
            {
                _configuration = new TestConfiguration();
                ConfigurationSection.Value.GetSection(nameof(TestConfiguration))
                    .Bind(_configuration);
            }
            
            return _configuration;
        }
    }

    public static Uri BaseUrl => Configuration.BaseUrl;
    
    public static int LastLimit => Configuration.LastLimit;
}