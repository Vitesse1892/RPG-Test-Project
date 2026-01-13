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


        public TestSettings(BrowserType browserType, Uri applicationUrl) //In order to keep ApplicationUrl non-nullable
        {
            BrowserType = browserType;
            ApplicationUrl = applicationUrl ?? throw new ArgumentNullException(nameof(applicationUrl));
        }
    }
}