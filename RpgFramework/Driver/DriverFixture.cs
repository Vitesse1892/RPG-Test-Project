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
            IWebDriver driver = browserType switch
            {
                BrowserType.Chrome => CreateChromeDriver(),
                BrowserType.Firefox => new FirefoxDriver(),
                BrowserType.Edge => CreateEdgeDriver(),
                BrowserType.Safari => new SafariDriver(),
                _ => CreateChromeDriver(),
            };

            driver.Manage().Window.Maximize(); // groot venster
            return driver;
        }

        private IWebDriver CreateChromeDriver()
        {
            var options = new ChromeOptions();
            float scale = _testSettings.ScaleFactor ?? 1.0f;
            options.AddArgument($"--force-device-scale-factor={scale}"); //Zoom in or out

            return new ChromeDriver(options);
        }

        private IWebDriver CreateEdgeDriver()
        {
            var options = new EdgeOptions();
            float scale = _testSettings.ScaleFactor ?? 1.0f;
            options.AddArgument($"--force-device-scale-factor={scale}"); //Zoom in or out

            return new EdgeDriver(options);
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
