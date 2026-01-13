using RpgFramework.Driver;

namespace RpgFramework.Hooks;

[Binding]
public class AfterTestRunHook
{
    [AfterTestRun]
    public static void AfterTestRun()
    {
        WebDriverHost.Stop();
    }
}