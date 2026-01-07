using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RpgFramework.Driver;
using RpgFramework.Extensions;
using RpgUiTests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgUiTests.Pages
{
    public class CommonPage : ICommonPage
    {
        private readonly IDriverWait _driver;
        private readonly IDriverFixture _driverFixture;

        public CommonPage(IDriverWait driver, IDriverFixture driverFixture)
        {
            _driver = driver;
            _driverFixture = driverFixture;
        }

        private IWebElement GetPageHeader(string headerText) => _driver.FindElement(By.XPath($"//h3[normalize-space(text())='{headerText}']"));


        public bool IsOnPage(string cardName)
        {
            try { return GetPageHeader(cardName).Displayed; }
            catch { return false; }
        }


    }
}
