using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using RpgFramework.Driver;
using RpgUiTests.Extensions;
using RpgUiTests.Models;
using RpgUiTests.Pages.Base;
using RpgUiTests.Ui;

namespace RpgUiTests.Pages.AdventurePlay
{
    public class AdventurePlayPage : IAdventurePlayPage
    {
        private readonly IDriverWait _driver;
        private readonly IDriverFixture _driverFixture;
        private readonly ScenarioContext _scenarioContext;
        private readonly IBasePage _basePage;

        public AdventurePlayPage(IDriverWait driver, IDriverFixture driverFixture, ScenarioContext scenarioContext, IBasePage commonPage)
        {
            _driver = driver;
            _driverFixture = driverFixture;
            _scenarioContext = scenarioContext;
            _basePage = commonPage;
        }

        //private IWebElement btnClickIt => _driver.FindElement(By.XPath("//button[contains(normalize-space(.), 'Click me') and contains(normalize-space(.), 'times')]"));
        private IWebElement GetConfirmMsgElm(string task) => _driver.FindElement(By.XPath($"//span[@data-task='{task}']"));
        private IWebElement btnClickIt => _driver.FindElement(By.XPath("//button[contains(normalize-space(.), 'Click me') and contains(normalize-space(.), 'times')]"));
        private IEnumerable<IWebElement> GetConfirmMsgElms(string task) => _driverFixture.Driver.FindElements(By.XPath($"//span[@data-task='{task}']"));
        private IEnumerable<IWebElement> GetBtnElms(string btnTxt) => _driverFixture.Driver.FindElements(By.XPath($"//*[self::a or self::button][contains(text(), '{btnTxt}') or contains(text(), '{btnTxt}')]"));
        private IWebElement fileInput => _driver.FindElement(By.XPath("//input[@type='file' and contains(@class, 'border-input')]"));
        private IWebElement txtInpFieldToType => _driver.FindElement(By.XPath("//input[contains(@class,'rounded-md') and not(@type='file')]"));
        private IWebElement sliderThumb => _driver.FindElement(By.XPath("//span[@role='slider']"));
        private IWebElement sliderTrack => _driver.FindElement(By.XPath("//span[@data-orientation='horizontal' and contains(@class,'w-full')]"));
        private IWebElement sliderContainer => _driver.FindElement(By.XPath("//span[@data-orientation='horizontal' and contains(@class,'touch-none')]"));
        private IWebElement txtMaxLvlConfMsg => _driver.FindElement(By.XPath("//span[contains(text(), \"You've reached the highest level!\")]"));

        private const string EnableElementScript = "arguments[0].disabled = false;";
        private const string ImagesFolder = "TestAssets/Images";

        private static readonly IReadOnlyDictionary<string, string> FileMap =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["cotton candy"] = "cotton candy.jpg",
            ["rock in the ocean"] = "rock in the ocean.jpg"
        };

        private const int MAX_STATS_WAARDE = 10;


        public void ClickTheClickItBtn(int amountOfClicks)
        {
            for (int i = 0; i < amountOfClicks; i++)
            {
                btnClickIt.Click();

                //Set de current stats na iedere klik, zodat te verifiëren is of de actie gevolgen heeft gehad op de hoogte van de stats
                var currentStats = _basePage.GetCharacterStats();
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
            taskMsgElementsCount.Should().Be(0, $"Expected no confirmation messages to be shown for the specified task, but found \"{taskMsgElementsCount}\".");
        }

        public void AssertButtonTextVisibility(string buttonText, bool shouldBeVisible)
        {
            int buttonCount = GetBtnElms(buttonText).Count(e => e.Displayed);

            if (shouldBeVisible)
            {
                buttonCount.Should().Be(1,
                    $"Expected button \"{buttonText}\" to be visible, but it was not.");
            }
            else
            {
                buttonCount.Should().Be(0,
                    $"Expected button \"{buttonText}\" not to be visible, but it was found.");
            }
        }


        public void AssertConfirmationMessageForMaxLevel()
        {
            var expMaxLvlConfMsg = ConfirmationMessages.GetMaxLevelMessage();
            string actMaxLvlConfMsg = txtMaxLvlConfMsg.Text.Trim();
            actMaxLvlConfMsg.Should().Be(expMaxLvlConfMsg, $"Confirmation message issue: Expected message to be \"{expMaxLvlConfMsg}\", but found \"{actMaxLvlConfMsg}\"");

        }

        public void EnableClickerButton()
        {
            ((IJavaScriptExecutor)_driverFixture.Driver).ExecuteScript("arguments[0].disabled=false", btnClickIt);
            btnClickIt.Click();
        }

        public void EnableTyperButtonAndTypeMessage(int iterations, string message)
        {
            for (int i = 0; i < iterations; i++)
            {
                EnableElement(txtInpFieldToType);
                TypeMessage(message);
            }

            var currentStats = _basePage.GetCharacterStats();
            _scenarioContext.Set(currentStats, "AdventurePageStatsCurrent");
        }


        public void EnableUploaderElementAndUploadFile(int iterations, string fileName1, string fileName2)
        {
            var files = new[] { fileName1, fileName2 };

            for (int i = 0; i < iterations; i++)
            {
                foreach (var fileName in files)
                {
                    EnableElement(fileInput);
                    UploadFile(fileName);
                }
            }

            var currentStats = _basePage.GetCharacterStats();
            _scenarioContext.Set(currentStats, "AdventurePageStatsCurrent");
        }


        private void EnableElement(IWebElement element)
        {
            ((IJavaScriptExecutor)_driverFixture.Driver).ExecuteScript(EnableElementScript, element);
        }


        public void AssertAllStatsHaveMaxLevel()
        {
            var statsCurrent = _scenarioContext.Get<CharacterOverviewDto>("AdventurePageStatsCurrent");
            statsCurrent.Strength.Should().Be(MAX_STATS_WAARDE);
            statsCurrent.Agility.Should().Be(MAX_STATS_WAARDE);
            statsCurrent.Wisdom.Should().Be(MAX_STATS_WAARDE);
            statsCurrent.Magic.Should().Be(MAX_STATS_WAARDE);
        }


        public void UploadFile(string fileName)
        {
            if (!FileMap.TryGetValue(fileName, out var resolvedFileName))
            {
                throw new ArgumentException(
                    $"De in de feature file opgegeven fileName '{fileName}' bestaat niet in de files folder. " +
                    $"Toegestane waarden zijn: {string.Join(", ", FileMap.Keys)}");
            }

            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ImagesFolder, resolvedFileName);
            fileInput.SendKeys(filePath);

            var currentStats = _basePage.GetCharacterStats();
            _scenarioContext.Set(currentStats, "AdventurePageStatsCurrent");
        }

        public void TypeMessage(string message)
        {
            txtInpFieldToType.ClearAndEnterText(message);

            //Set de current stats, zodat te verifiëren is of de actie gevolgen heeft gehad op de hoogte van de stats
            var currentStats = _basePage.GetCharacterStats();
            _scenarioContext.Set(currentStats, "AdventurePageStatsCurrent");
        }

        public void AssertDynamicTextTellerClickItBtn()
        {
            int totalClicks = 5;

            string initialExpectedText = $"Click me {totalClicks} times";
            string initialActualText = btnClickIt.Text;
            if (initialActualText != initialExpectedText) throw new Exception($"Initiële tekst klopt niet. Verwacht: '{initialExpectedText}', maar kreeg: '{initialActualText}'");

            for (int i = 0; i < totalClicks; i++)
            {
                btnClickIt.Click();
                Thread.Sleep(100);

                string expectedText = $"Click me {totalClicks - i - 1} times";
                string actualText = btnClickIt.Text;
                if (actualText != expectedText) throw new Exception($"Tekst klopt niet. Verwacht: '{expectedText}', maar kreeg: '{actualText}'");
            }
        }


        public void SlideToRight(int percentage)
        {
            //Ik wil ivm robuustheid van de methode een percentage die een veelvoud van 10 is, omdat een percentage van 98 of 99 procent ook al kan leiden tot de confirmation message
            if (percentage < 0 || percentage > 100) throw new ArgumentOutOfRangeException(nameof(percentage), $"Percentage moet tussen 0 en 100 liggen. Invoer: {percentage}"); // Check of het percentage tussen 0 en 100 ligt
            if (percentage % 10 != 0) throw new ArgumentException($"Percentage moet een veelvoud van 10 zijn (0, 10, 20, …, 100). Invoer: {percentage}"); // Check of het percentage een veelvoud van 10 is

            Actions actions = new Actions(_driverFixture.Driver);

            int trackWidth = sliderTrack.Size.Width;                                                           // dit geeft breedte in pixels 
            int offsetX = (int)Math.Round(trackWidth * (percentage / 100.0), MidpointRounding.AwayFromZero);   //Procentueel naar rechts bewegen 

            actions.ClickAndHold(sliderThumb)                                                                  //Houdt de muisknop ingedrukt op een element
                    .MoveByOffset(offsetX, 0)                                                                  //Beweegt aantal pixels naar rechts zoals in percentage
                    .Release()                                                                                 //Laat de muisknop los
                    .Perform();                                                                                //Voert de actie uit

            //Set de current stats, zodat te verifiëren is of de actie gevolgen heeft gehad op de hoogte van de stats
            var currentStats = _basePage.GetCharacterStats();
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


        public void AssertTaskElementState(string task, bool shouldBeEnabled)
        {
            IWebElement taskElement;
            bool isEnabled;

            switch (task.ToLower())
            {
                case "clicker":
                    taskElement = btnClickIt;
                    isEnabled = taskElement.Enabled;
                    break;

                case "uploader":
                    taskElement = fileInput;
                    isEnabled = taskElement.Enabled;
                    break;

                case "typer":
                    taskElement = txtInpFieldToType;
                    isEnabled = taskElement.Enabled;
                    break;

                case "slider":
                    taskElement = sliderContainer;
                    string ariaDisabled = taskElement.GetAttribute("aria-disabled");
                    isEnabled = !(ariaDisabled?.Equals("true", StringComparison.OrdinalIgnoreCase) == true);
                    break;

                default:
                    throw new ArgumentException($"Invalide task opgegeven: {task}");
            }

            // Check of element disabled is
            Assert.AreEqual(shouldBeEnabled, isEnabled, $"Task element '{task}' zou {(shouldBeEnabled ? "enabled" : "disabled")} moeten zijn, maar is dat niet.");
            //(isDisabled, $"Task element '{task}' zou disabled moeten zijn, maar is dat niet.");
        }


    }
}
