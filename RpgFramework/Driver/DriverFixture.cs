using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using RpgFramework.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgFramework.Driver
{
    public class DriverFixture : IDriverFixture, IDisposable
    {
        private readonly TestSettings _testSettings;
        public IWebDriver Driver { get; }

        public DriverFixture(TestSettings testSettings)
        {
            _testSettings = testSettings;
            Driver = GetDriverType(_testSettings.BrowserType);
            Driver.Navigate().GoToUrl(_testSettings.ApplicationUrl);
        }

        private IWebDriver GetDriverType(BrowserType browserType)
        {
            return browserType switch
            {
                BrowserType.Chrome => new ChromeDriver(),
                BrowserType.Firefox => new FirefoxDriver(),
                BrowserType.Edge => new EdgeDriver(),
                BrowserType.Safari => new SafariDriver(),
                _ => new ChromeDriver(),
            };
        }

        public void Dispose()
        {
            Driver.Quit();
        }



        public enum BrowserType
        {
            Chrome,
            Firefox,
            Edge,
            Safari
        }
    }
}
