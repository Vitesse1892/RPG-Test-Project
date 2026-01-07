using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace RpgFramework.Hooks;

[Binding]
public class Hooks
{
    private readonly IWebDriver _driver;

    public Hooks(IWebDriver driver)
    {
        _driver = driver;
    }

    [AfterScenario]
    public void AfterScenario()
    {
        if (_driver != null)
        {
            _driver.Quit(); // sluit alle vensters en beëindigt de driver
            _driver.Dispose(); // optioneel, ruimt resources op
        }
    }
}



