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
    public class PreparePlayPage : IPreparePlayPage
    {
        private readonly IDriverWait _driver;
        private readonly IDriverFixture _driverFixture;

        public PreparePlayPage(IDriverWait driver, IDriverFixture driverFixture)
        {
            _driver = driver;
            _driverFixture = driverFixture;
        }

        private IWebElement btnClickHereToPlay => _driver.FindElement(By.XPath("//a[@href='/play' and contains(text(), 'Click here to play')]"));
        private IWebElement btnStart => _driver.FindElement(By.XPath("//button[contains(text(), 'Start!')]"));
        private IWebElement txtInpFieldCharacterName => _driver.FindElement(By.XPath("//input[@placeholder='Galactic space lord']"));
        private IWebElement txtHeaderCharacterName => _driver.FindElement(By.XPath("//h3[@data-testid='character-name']"));
        private IWebElement ddlBuildType => _driver.FindElement(By.XPath("//select[@aria-hidden='true']"));
        private IWebElement txtSelFieldBuildType => _driver.FindElement(By.XPath("//button[@role='combobox']//span"));
        private IWebElement txtHeaderBuildType => _driver.FindElement(By.XPath("//p[@data-testid='character-stats']"));
        private IWebElement errMsgCharacterName => _driver.FindElement(By.XPath("//p[contains(@class,'text-destructive')]"));
        private IEnumerable<IWebElement> errMsgElementsCharacterName => _driverFixture.Driver.FindElements(By.XPath("//p[contains(@class,'text-destructive')]"));
        private IWebElement valueStrength => _driver.FindElement(By.XPath("//div[@data-character-stats='Strength']//span"));
        private IWebElement valueAgility => _driver.FindElement(By.XPath("//div[@data-character-stats='Agility']//span"));
        private IWebElement valueWisdom => _driver.FindElement(By.XPath("//div[@data-character-stats='Wisdom']//span"));
        private IWebElement valueMagic => _driver.FindElement(By.XPath("//div[@data-character-stats='Magic']//span"));
        private IWebElement valueLevel => _driver.FindElement(By.XPath("//div[@data-character-stats='Level']//span"));


        public void ClickClickHereToPlaybtn() => btnClickHereToPlay.Click();
        public void ClickStartBtn() => btnStart.Click();

        public void ChooseName(CharacterOverviewDto character)
        {
            txtInpFieldCharacterName.ClearAndEnterText(character.CharacterName);
        }

        public void SelectBuild(CharacterOverviewDto character)
        {
            ddlBuildType.SelectDropdownByText(character.BuildType.ToString());
        }

        public void ChooseNameAndSelectBuild(CharacterOverviewDto character)
        {
            txtInpFieldCharacterName.ClearAndEnterText(character.CharacterName);
            ddlBuildType.SelectDropdownByText(character.BuildType.ToString()); 
        }


        public void AssertErrMsgCharacterName(string expErrMsg)
        {
            var ActErrMsg = errMsgCharacterName.Text.Trim();
            errMsgCharacterName.Text.Trim().Should().Be(expErrMsg, $"Expected error message to be \"{expErrMsg}\", but found \"{ActErrMsg}\"");
        }


        public void AssertNameErrorMessageIsNotVisible()
        {
            int errorElementsCount = errMsgElementsCharacterName.Count();
            errorElementsCount.Should().Be(0, $"Expected no validation error to be shown, but found \"{errorElementsCount}\" error element(s).");
        }


        public void AssertDataBindingCharacterName(CharacterOverviewDto expData)
        {
            var charNameInpField = txtInpFieldCharacterName.GetAttribute("value")?.Trim();
            var charNameHeader = txtHeaderCharacterName.Text.Trim();

            charNameInpField.Should().Be(expData.CharacterName, $"Input field issue: Expected character name to be \"{expData.CharacterName}\", but found \"{charNameInpField}\"");
            charNameHeader.Should().Be(expData.CharacterName, $"Header issue: Expected character name to be \"{expData.CharacterName}\", but found \"{charNameHeader}\"");
            //Data binding check
            charNameInpField.Should().Be(charNameHeader, $"Expected character name input field \"{charNameInpField}\", to be equal to character name in header \"{charNameHeader}\"");
        }

        public void AssertDataBindingBuildType(CharacterOverviewDto expData)
        {
            var buildTypeSelected = txtSelFieldBuildType.Text.Trim().ToLowerInvariant();
            
            var buildTypeHeaderFullText = txtHeaderBuildType.Text.Trim();
            var buildTypeHeaderSplit = buildTypeHeaderFullText.Split(' ').Last();

            buildTypeSelected.Should().Be(expData.BuildType.ToString().ToLowerInvariant(), $"Selection field issue: Expected Build type to be \"{expData.BuildType.ToString().ToLowerInvariant()}\", but found \"{buildTypeSelected}\"");
            buildTypeHeaderSplit.Should().Be(expData.BuildType.ToString().ToLowerInvariant(), $"Header issue: Expected build type to be \"{expData.BuildType.ToString().ToLowerInvariant()}\", but found \"{buildTypeHeaderSplit}\"");
            //Data binding check
            buildTypeSelected.Should().Be(buildTypeHeaderSplit, $"Expected build type selection field \"{buildTypeSelected}\", to be equal to build type in header \"{buildTypeHeaderSplit}\"");
        }


        public void AssertStats(CharacterOverviewDto expData)
        {
            AssertStat("Strength", valueStrength.Text.Trim(), expData.Strength);
            AssertStat("Agility", valueAgility.Text.Trim(), expData.Agility);
            AssertStat("Wisdom", valueWisdom.Text.Trim(), expData.Wisdom);
            AssertStat("Magic", valueMagic.Text.Trim(), expData.Magic);
            AssertStat("Level", valueLevel.Text.Trim(), expData.Level);
        }

        public void AssertStat(string statName, string actualValue, int expectedValue)
        {
            actualValue.Should().Be(
                expectedValue.ToString(),
                $"Expected {statName} value to be \"{expectedValue}\", but found \"{actualValue}\""
            );
        }


    }
}
