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
    public class AdventurePlayPage : IAdventurePlayPage
    {
        private readonly IDriverWait _driver;
        private readonly IDriverFixture _driverFixture;

        public AdventurePlayPage(IDriverWait driver, IDriverFixture driverFixture)
        {
            _driver = driver;
            _driverFixture = driverFixture;
        }

        


    }
}
