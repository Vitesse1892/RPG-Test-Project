using Microsoft.Extensions.DependencyInjection;
using Reqnroll.Microsoft.Extensions.DependencyInjection;
using RpgFramework.Config;
using RpgFramework.Driver;
using RpgUiTests.Pages.AdventurePlay;
using RpgUiTests.Pages.Base;
using RpgUiTests.Pages.PreparePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgUiTests;

public class Startup
{
    [ScenarioDependencies]
    public static IServiceCollection CreateServices()
    {
        var services = new ServiceCollection();

        services
                .AddSingleton(ConfigReader.ReadConfig())
                .AddScoped<IDriverFixture, DriverFixture>()
                .AddScoped<IDriverWait, DriverWait>()
                .AddScoped<IPreparePlayPage, PreparePlayPage>()
                .AddScoped<IAdventurePlayPage, AdventurePlayPage>()
                .AddScoped<IBasePage, BasePage>()
                .AddScoped<ScenarioContext>();

        return services;
    }
}