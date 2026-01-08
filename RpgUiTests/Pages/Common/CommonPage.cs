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

        private IWebElement GetPageHeaderElm(string headerText) => _driver.FindElement(By.XPath($"//h3[normalize-space(text())='{headerText}']"));
        private IWebElement txtHeaderCharacterName => _driver.FindElement(By.XPath("//h3[@data-testid='character-name']"));
        private IWebElement txtHeaderBuildType => _driver.FindElement(By.XPath("//p[@data-testid='character-stats']"));
        private IWebElement valueStrength => _driver.FindElement(By.XPath("//div[@data-character-stats='Strength']//span"));
        private IWebElement valueAgility => _driver.FindElement(By.XPath("//div[@data-character-stats='Agility']//span"));
        private IWebElement valueWisdom => _driver.FindElement(By.XPath("//div[@data-character-stats='Wisdom']//span"));
        private IWebElement valueMagic => _driver.FindElement(By.XPath("//div[@data-character-stats='Magic']//span"));
        private IWebElement valueLevel => _driver.FindElement(By.XPath("//div[@data-character-stats='Level']//span"));


        public bool IsOnPage(string cardName)
        {
            try { return GetPageHeaderElm(cardName).Displayed; }
            catch { return false; }
        }

        public CharacterOverviewDto GetCharacterStats()
        {
            var buildTypeText = txtHeaderBuildType.Text.Trim().Split(' ').Last();
            var buildType = Enum.TryParse<BuildType>(buildTypeText, true, out var bt) ? bt : throw new ArgumentException($"Build type '{buildTypeText}' is ongeldig.", nameof(buildTypeText));

            return new CharacterOverviewDto
            {
                CharacterName = txtHeaderCharacterName.Text.Trim(),
                BuildType = buildType,
                Strength = int.Parse(valueStrength.Text.Trim()),
                Agility = int.Parse(valueAgility.Text.Trim()),
                Wisdom = int.Parse(valueWisdom.Text.Trim()),
                Magic = int.Parse(valueMagic.Text.Trim()),
                Level = int.Parse(valueLevel.Text.Trim()),
            };
        }

    }
}
