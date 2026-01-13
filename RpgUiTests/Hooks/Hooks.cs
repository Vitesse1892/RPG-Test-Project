using OpenQA.Selenium;
using RpgFramework.Driver;
using System.Threading.Tasks;

namespace RpgFramework.Hooks;

[Binding]
public class Hooks
{
    private readonly IDriverFixture _driverFixture;
    private readonly ScenarioContext _scenarioContext;

    public Hooks(IDriverFixture driverFixture, ScenarioContext scenarioContext)
    {
        _driverFixture = driverFixture;
        _scenarioContext = scenarioContext;
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
        //Altijd reset cookies en ga naar startpagina vóór elk scenario
        var driver = _driverFixture.Driver;

        driver.Manage().Cookies.DeleteAllCookies();
        ((IJavaScriptExecutor)driver).ExecuteScript("window.localStorage.clear(); window.sessionStorage.clear();");

        driver.Navigate().GoToUrl(_driverFixture.ApplicationUrl.AbsoluteUri);
    }

    [AfterScenario]
    public void AfterScenario()
    {
        var driver = _driverFixture.Driver;

        var testError = _scenarioContext.TestError;

        // Alleen screenshot bij failure
        if (testError != null)
        {
            ScreenshotHelper.Save(driver, _scenarioContext.ScenarioInfo.Title);
        }
    }
}



