using Microsoft.Extensions.DependencyInjection;
using Reqnroll.Microsoft.Extensions.DependencyInjection;
using RpgFramework.Config;
using RpgFramework.Driver;
using RpgUiTests.Pages.AdventurePlay;
using RpgUiTests.Pages.Base;
using RpgUiTests.Pages.PreparePage;

namespace RpgUiTests;

public class Startup
{
    [ScenarioDependencies]
    public static IServiceCollection CreateServices()
    {
        var services = new ServiceCollection();

        // Config toevoegen
        var config = ConfigReader.ReadConfig();
        services.AddSingleton(config); // TestSettings

        // DriverFixture met TestSettings
        services.AddScoped<IDriverFixture>(sp =>
        {
            var testSettings = sp.GetRequiredService<TestSettings>();
            return new DriverFixture(testSettings);
        });

        services
                .AddScoped<IDriverWait, DriverWait>()
                .AddScoped<IPreparePlayPage, PreparePlayPage>()
                .AddScoped<IAdventurePlayPage, AdventurePlayPage>()
                .AddScoped<IBasePage, BasePage>()
                .AddScoped<ScenarioContext>();

        return services;
    }
}