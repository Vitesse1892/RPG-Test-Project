using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using RpgFramework.Driver;
using RpgFramework.Extensions;
using RpgUiTests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static TechTalk.SpecFlow.Configuration.AppConfig.GeneratorConfigElement;

namespace RpgUiTests.Pages
{
    public class AdventurePlayPage : IAdventurePlayPage
    {
        private readonly IDriverWait _driver;
        private readonly IDriverFixture _driverFixture;
        private readonly ScenarioContext _scenarioContext;
        private readonly ICommonPage _commonPage;

        public AdventurePlayPage(IDriverWait driver, IDriverFixture driverFixture, ScenarioContext scenarioContext, ICommonPage commonPage)
        {
            _driver = driver;
            _driverFixture = driverFixture;
            _scenarioContext = scenarioContext;
            _commonPage = commonPage;
        }

        private IWebElement btnClickIt => _driver.FindElement(By.XPath("//button[contains(normalize-space(.), 'Click me') and contains(normalize-space(.), 'times')]"));
        private IWebElement GetConfirmMsgElm(string task) => _driver.FindElement(By.XPath($"//span[@data-task='{task}']"));
        private IEnumerable<IWebElement> GetConfirmMsgElms(string task) => _driverFixture.Driver.FindElements(By.XPath($"//span[@data-task='{task}']"));
        private IWebElement fileInput => _driver.FindElement(By.XPath("//input[@type='file' and contains(@class, 'border-input')]"));
        private IWebElement txtInpFieldToType => _driver.FindElement(By.XPath("//input[contains(@class,'rounded-md') and not(@type='file')]"));
        private IWebElement sliderThumb => _driver.FindElement(By.XPath("//span[@role='slider']"));
        private IWebElement sliderTrack => _driver.FindElement(By.XPath("//span[@data-orientation='horizontal' and contains(@class,'w-full')]"));
        private IWebElement sliderContainer => _driver.FindElement(By.XPath("//span[@data-orientation='horizontal' and contains(@class,'touch-none')]"));


        public void ClickTheClickItBtn(int amountOfClicks)
        {
            for (int i = 0; i < amountOfClicks; i++)
            {
                btnClickIt.Click();

                //Set de current stats na iedere klik, zodat te verifiëren is of de actie gevolgen heeft gehad op de hoogte van de stats
                var currentStats = _commonPage.GetCharacterStats();
                _scenarioContext.Set(currentStats, "AdventurePageStatsCurrent");
            }
        }

        public void AssertConfirmationMessageForFinishedTask(string task, string expectedConfirmationMessage)
        {
            string ActualCorfirmationMessage = GetConfirmMsgElm(task).Text.Trim();
            ActualCorfirmationMessage.Should().Be(expectedConfirmationMessage, $"Confirmation message issue: Expected message to be \"{expectedConfirmationMessage}\", but found \"{ActualCorfirmationMessage}\"");
        }

        public void AssertTaskConfirmationMessageForSpecificTaskIsNotVisible(string task)
        {
            int taskMsgElementsCount = GetConfirmMsgElms(task).Count();
            taskMsgElementsCount.Should().Be(0, $"Expected no confirmation messages to be shown for the specified task, but found \"{taskMsgElementsCount}\" error element(s).");
        }

        public void UploadFile()
        {
            fileInput.SendKeys(@"C:\Users\leroy\source\repos\RPG Test Project\RpgUiTests\Files\cotton candy.jpg");
            //Set de current stats, zodat te verifiëren is of de actie gevolgen heeft gehad op de hoogte van de stats
            var currentStats = _commonPage.GetCharacterStats();
            _scenarioContext.Set(currentStats, "AdventurePageStatsCurrent");
        }

        public void TypeMessage(string message)
        {
            txtInpFieldToType.ClearAndEnterText(message);

            //Set de current stats, zodat te verifiëren is of de actie gevolgen heeft gehad op de hoogte van de stats
            var currentStats = _commonPage.GetCharacterStats();
            _scenarioContext.Set(currentStats, "AdventurePageStatsCurrent");
        }

        public void SlideToRight(int percentage)
        {
            Actions actions = new Actions(_driverFixture.Driver);
            
            int trackWidth = sliderTrack.Size.Width;                                                           // dit geeft breedte in pixels 
            int offsetX = (int)Math.Round(trackWidth * (percentage / 100.0), MidpointRounding.AwayFromZero);   //Procentueel naar rechts bewegen 

            actions.ClickAndHold(sliderThumb)                                                                  //Houdt de muisknop ingedrukt op een element
                    .MoveByOffset(offsetX, 0)                                                                  //Beweegt aantal pixels naar rechts zoals in percentage
                    .Release()                                                                                 //Laat de muisknop los
                    .Perform();                                                                                //Voert de actie uit

            //Set de current stats, zodat te verifiëren is of de actie gevolgen heeft gehad op de hoogte van de stats
            var currentStats = _commonPage.GetCharacterStats();
            _scenarioContext.Set(currentStats, "AdventurePageStatsCurrent");
        }

        public void AssertStatsWithPreparePlayPage()
        {
            _scenarioContext.Get<CharacterOverviewDto>("PreparePageStats").Should().Be(_scenarioContext.Get<CharacterOverviewDto>("AdventurePageStatsFromStart"));
        }

        public void AssertStatsAreIncreasedByCorrectAmount(int totalAmountIncreasedFromStart)
        {
            var statsFromStart = _scenarioContext.Get<CharacterOverviewDto>("AdventurePageStatsFromStart");
            var statsCurrent = _scenarioContext.Get<CharacterOverviewDto>("AdventurePageStatsCurrent");

            //De vijf integers zijn een X aantal hoger dan bij aanvang op de Adventure page pagina
            statsCurrent.Strength.Should().Be(statsFromStart.Strength + totalAmountIncreasedFromStart);
            statsCurrent.Agility.Should().Be(statsFromStart.Agility + totalAmountIncreasedFromStart);
            statsCurrent.Wisdom.Should().Be(statsFromStart.Wisdom + totalAmountIncreasedFromStart);
            statsCurrent.Magic.Should().Be(statsFromStart.Magic + totalAmountIncreasedFromStart);
            statsCurrent.Level.Should().Be(statsFromStart.Level + totalAmountIncreasedFromStart);
        }


        public void AssertTaskElementIsDisabled(string task)
        {
            IWebElement taskElement;
            bool isDisabled;

            switch (task.ToLower())
            {
                case "clicker":
                    taskElement = btnClickIt;
                    isDisabled = !taskElement.Enabled;
                    break;

                case "uploader":
                    taskElement = fileInput;
                    isDisabled = !taskElement.Enabled;
                    break;

                case "typer":
                    taskElement = txtInpFieldToType;
                    isDisabled = !taskElement.Enabled;
                    break;

                case "slider":
                    taskElement = sliderContainer;
                    string dataDisabled = taskElement.GetAttribute("aria-disabled");
                    //isDisabled = dataDisabled != null && dataDisabled.Equals("true", StringComparison.OrdinalIgnoreCase);
                    isDisabled = !string.IsNullOrEmpty(dataDisabled);
                    break;

                default:
                    throw new ArgumentException($"Invalide task opgegeven: {task}");
            }

            // Check of element disabled is
            Assert.IsTrue(isDisabled, $"Task element '{task}' zou disabled moeten zijn, maar is dat niet.");
        }

        




    }
}
