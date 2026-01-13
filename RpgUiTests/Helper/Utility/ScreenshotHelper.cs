using OpenQA.Selenium;

public static class ScreenshotHelper
{
    public static void Save(IWebDriver driver, string testName)
    {
        if (driver is not ITakesScreenshot screenshotDriver)
            return;

        var timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        var baseDir = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,  
            "..", "..", "..",   // omhoog naar projectroot
            "TestResults",
            "Screenshots",
            timestamp
        );

        baseDir = Path.GetFullPath(baseDir);
        Directory.CreateDirectory(baseDir);

        var maxLength = 50; //Je pad mag max 250 tekens zijn
        var safeTestName = string.Concat(testName.Split(Path.GetInvalidFileNameChars()));
        if (safeTestName.Length > maxLength) safeTestName = safeTestName.Substring(0, maxLength);

        var filePath = Path.Combine(baseDir, $"{safeTestName}.png");

        try
        {
            var screenshot = screenshotDriver.GetScreenshot();
            screenshot.SaveAsFile(filePath);
            Console.WriteLine($"Screenshot saved to {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save screenshot: {ex.Message}");
            throw;
        }

    }
}