using OpenQA.Selenium;

namespace RpgFramework.Driver
{
    public interface IDriverFixture
    {
        IWebDriver Driver { get; }
        Uri ApplicationUrl { get; }
        void StopDriver();
    }
}
