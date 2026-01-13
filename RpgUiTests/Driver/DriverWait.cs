using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RpgFramework.Config;

namespace RpgFramework.Driver;

public class DriverWait : IDriverWait
{
    private readonly IDriverFixture _driverFixture;
    private readonly TestSettings _testSettings;
    private readonly Lazy<WebDriverWait> _webDriverWait;

    public DriverWait(IDriverFixture driverFixture, TestSettings testSettings)
    {
        _driverFixture = driverFixture;
        _testSettings = testSettings;
        _webDriverWait = new Lazy<WebDriverWait>(GetWaitDriver);
    }

    public IWebElement FindElement(By elementLocator)
    {
        return _webDriverWait.Value.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(elementLocator));
    }


    public IEnumerable<IWebElement> FindElements(By elementLocator)
    {
        return _webDriverWait.Value.Until(driver =>
        {
            var elements = driver.FindElements(elementLocator);
            return elements.Any() ? elements : null;
        });
    }

    private WebDriverWait GetWaitDriver()
    {
        return new WebDriverWait(_driverFixture.Driver, TimeSpan.FromSeconds(_testSettings.TimeoutInterval ?? 30))
        {
            PollingInterval = TimeSpan.FromSeconds(_testSettings.TimeoutInterval ?? 1)
        };
    }
}

