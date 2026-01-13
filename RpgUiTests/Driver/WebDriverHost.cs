using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using RpgFramework.Config;

namespace RpgFramework.Driver;

public static class WebDriverHost
{
    private static IWebDriver? _driver;
    private static readonly object _lock = new();

    public static IWebDriver Driver
    {
        get
        {
            if (_driver == null)
                throw new InvalidOperationException("WebDriver not started.");
            return _driver;
        }
    }

    public static void EnsureStarted(TestSettings settings)
    {
        if (_driver != null) return;

        lock (_lock)
        {
            if (_driver != null) return;

            _driver = CreateDriver(settings);
            if (!settings.Headless) _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(settings.ApplicationUrl);
        }
    }

    private static IWebDriver CreateDriver(TestSettings settings)
    {
        return settings.BrowserType switch
        {
            BrowserType.Chrome => CreateChrome(settings),
            BrowserType.Firefox => new FirefoxDriver(),
            BrowserType.Edge => CreateEdge(settings),
            BrowserType.Safari => new SafariDriver(),
            _ => CreateChrome(settings)
        };
    }

    private static IWebDriver CreateChrome(TestSettings settings)
    {
        var options = new ChromeOptions();
        var scale = settings.ScaleFactor ?? 1.0f;
        options.AddArgument($"--force-device-scale-factor={scale}");
        
        if (settings.Headless)
        {
            options.AddArgument("--headless=new"); 
            options.AddArgument("--window-size=1920,1080");
            options.AddArgument("--disable-gpu");
        }

        return new ChromeDriver(options);
    }

    private static IWebDriver CreateEdge(TestSettings settings)
    {
        var options = new EdgeOptions();
        var scale = settings.ScaleFactor ?? 1.0f;
        options.AddArgument($"--force-device-scale-factor={scale}");

        if (settings.Headless)
        {
            options.AddArgument("--headless=new");
            options.AddArgument("--window-size=1920,1080");
        }

        return new EdgeDriver(options);
    }

    public static void Stop()
    {
        _driver?.Quit();
        _driver?.Dispose();
        _driver = null;
    }

    public enum BrowserType
    {
        Chrome,
        Firefox,
        Edge,
        Safari
    }
}