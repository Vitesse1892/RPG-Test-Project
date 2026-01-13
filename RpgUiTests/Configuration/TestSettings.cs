using static RpgFramework.Driver.WebDriverHost;


namespace RpgFramework.Config
{
    public class TestSettings
    {
        public BrowserType BrowserType { get; set; }
        public Uri ApplicationUrl { get; set; }
        public float? TimeoutInterval { get; set; }
        public float? ScaleFactor { get; set; }
        public bool Headless { get; set; }
    }
}
