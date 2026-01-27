using Microsoft.VisualStudio.TestPlatform.CoreUtilities.Helpers;
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
using static RpgFramework.Driver.WebDriverHost;

namespace RpgUiTests.Driver
{
    public static class WebDriverFactory
    {
        public static IWebDriver CreateDriver(TestSettings settings)
        {
            var pipeline = settings.BrowserType == BrowserType.ChromePipeline || IsCi();

            return settings.BrowserType switch
            {
                BrowserType.Chrome => CreateChrome(settings, pipeline),
                BrowserType.ChromePipeline => CreateChrome(settings, pipeline),
                BrowserType.Firefox => new FirefoxDriver(),
                BrowserType.Edge => CreateEdge(settings, pipeline),
                BrowserType.Safari => new SafariDriver(),
                _ => CreateChrome(settings, pipeline)
            };
        }

        private static bool IsCi()
        {
            return
                string.Equals(Environment.GetEnvironmentVariable("TF_BUILD"), "True", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(Environment.GetEnvironmentVariable("CI"), "true", StringComparison.OrdinalIgnoreCase);
        }

        private static IWebDriver CreateChrome(TestSettings settings, bool pipeline)
        {
            var options = new ChromeOptions();
            var scale = settings.ScaleFactor ?? 1.0f;
            options.AddArgument($"--force-device-scale-factor={scale}");

            var headless = settings.Headless || pipeline; //headless wordt true als ten minste één van deze twee true is

            if (headless)
            {
                options.AddArgument("--headless=new");
                options.AddArgument("--window-size=1920,1080");
                options.AddArgument("--disable-gpu");
            }

            if (pipeline)
            {
                // Deze maken Chrome veel stabieler op CI agents/containers
                options.AddArgument("--no-sandbox");
                options.AddArgument("--disable-dev-shm-usage");
                options.AddArgument("--disable-extensions");
                options.AddArgument("--disable-infobars");
            }

            return new ChromeDriver(options);
        }

        private static IWebDriver CreateEdge(TestSettings settings, bool pipeline)
        {
            var options = new EdgeOptions();

            var scale = settings.ScaleFactor ?? 1.0f;
            options.AddArgument($"--force-device-scale-factor={scale}");

            var headless = settings.Headless || pipeline;

            if (headless)
            {
                options.AddArgument("--headless=new");
                options.AddArgument("--window-size=1920,1080");
                options.AddArgument("--disable-gpu");
            }

            // Edge (Chromium) kan dezelfde CI flags gebruiken
            if (pipeline)
            {
                options.AddArgument("--no-sandbox");
                options.AddArgument("--disable-dev-shm-usage");
                options.AddArgument("--disable-extensions");
                options.AddArgument("--disable-infobars");
            }

            return new EdgeDriver(options);
        }




    }
}
