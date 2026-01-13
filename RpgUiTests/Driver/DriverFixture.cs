using OpenQA.Selenium;
using RpgFramework.Config;

namespace RpgFramework.Driver
{
    public class DriverFixture : IDriverFixture
    {
        private readonly TestSettings _testSettings;

        public IWebDriver Driver => WebDriverHost.Driver;
        public Uri ApplicationUrl => _testSettings.ApplicationUrl;

        public DriverFixture(TestSettings testSettings)
        {
            _testSettings = testSettings;
            WebDriverHost.EnsureStarted(testSettings);
        }

        public void StopDriver()
        {
            WebDriverHost.Stop();
        }
    }
}
