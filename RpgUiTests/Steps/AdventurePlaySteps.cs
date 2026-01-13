using RpgUiTests.Models;
using RpgUiTests.Pages.AdventurePlay;
using RpgUiTests.Pages.Base;
using RpgUiTests.Pages.PreparePage;
using RpgUiTests.Ui;
using RpgUiTests.UI;


namespace RpgUiTests.StepDefinitions
{
    [Binding]
    public sealed class AdventurePlaySteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IPreparePlayPage _PreparePlayPage;
        private readonly IAdventurePlayPage _AdventurePlayPage;
        private readonly IBasePage _BasePage;

        public AdventurePlaySteps(ScenarioContext scenarioContext, IPreparePlayPage preparePlayPage, IAdventurePlayPage adventurePlayPage, IBasePage basePage)
        {
            _scenarioContext = scenarioContext;
            _PreparePlayPage = preparePlayPage;
            _AdventurePlayPage = adventurePlayPage;
            _BasePage = basePage;
        }

        [Given("dat ik de juiste handelingen heb verricht om op de play adventure pagina terecht te komen met character build type {string}")]
        public void GivenDatIkDeJuisteHandelingenHebVerrichtOmOpDePlayAdventurePaginaTerechtTeKomenMetCharacterBuildType(string build)
        {
            var buildType = Enum.TryParse<BuildType>(build, true, out var bt) ? bt : throw new ArgumentException($"Opgegeven build type '{build}' in je feature file is ongeldig.");

            //Bewust vaste hardcoded character name. 
            var characterNameAndBuild = new CharacterOverviewDto
            {
                CharacterName = "Spelersnaam",
                BuildType = buildType
            };

            const string cardNamePreparePlayPage = "Choose a name and build";
            const string cardNameAdventurePlayPage = "Adventure time";

            _BasePage.ClickButtonByText("Click here to play");
            _BasePage.IsOnPage(cardNamePreparePlayPage);
            _PreparePlayPage.ChooseNameAndSelectBuild(characterNameAndBuild);

            var statsCharBuildPreparePage = _BasePage.GetCharacterStats();
            _scenarioContext.Set(statsCharBuildPreparePage, "PreparePageStats");

            _BasePage.ClickButtonByText("Start!");
            _BasePage.IsOnPage(cardNameAdventurePlayPage); //Bevestiging belangrijk, ivm assertion stats tussen prepare en adventure page icm gelijke elementen

            var statsCharBuildAdventurePage = _BasePage.GetCharacterStats();
            _scenarioContext.Set(statsCharBuildPreparePage, "AdventurePageStatsFromStart");
            _scenarioContext.Set(statsCharBuildPreparePage, "AdventurePageStatsCurrent");
        }

        [Given("dat ik alle taken succesvol heb afgerond")]
        public void GivenDatIkAlleTakenSuccesvolHebAfgerond()
        {
            const int numberOfClicksSuccess = 5;
            const string uploadfileSuccess = "cotton candy";
            const string messageSuccess = "Lorem Ipsum";
            const int slidePErcentageSuccess = 100;

            _AdventurePlayPage.ClickTheClickItBtn(numberOfClicksSuccess);
            _AdventurePlayPage.UploadFile(uploadfileSuccess);
            _AdventurePlayPage.TypeMessage(messageSuccess);
            _AdventurePlayPage.SlideToRight(slidePErcentageSuccess);
        }



        [When("de Click it! button {int} keer wordt ingedrukt")]
        public void WhenDeClickItButtonKeerWordtIngedrukt(int amountOfClicks)
        {
            _AdventurePlayPage.ClickTheClickItBtn(amountOfClicks);
        }

        [Then("loopt de dynamische teller in de click it button terug van 5 naar 0 bij elke klik")]
        public void ThenLooptDeDynamischeTellerInDeClickItButtonTerugVanNaarBijElkeKlik()
        {
            _AdventurePlayPage.AssertDynamicTextTellerClickItBtn();
        }



        [Then("verschijnt voor de task {string} het bijbehorende bevestigingsbericht")]
        public void ThenVerschijntVoorDeTaskHetBijbehorendeBevestigingsbericht(string task)
        {
            //Get confirmation message van domain extension die je vervolgens wil asserten
            if (!Enum.TryParse<TaskType>(task, true, out var taskType))
                throw new ArgumentException($"Ongeldige task opgegeven: '{task}'. Geldige waarden zijn: {string.Join(", ", Enum.GetNames(typeof(TaskType)))}");
            var confirmationMessage = ConfirmationMessages.GetMessage(taskType);

            _AdventurePlayPage.AssertConfirmationMessageForFinishedTask(task, confirmationMessage);

            //Later wil je verifiëren of de huidige waarden voor stats en level zijn opgehoogd
            var statsCharBuildPreparePage = _BasePage.GetCharacterStats();
            _scenarioContext.Set(statsCharBuildPreparePage, "AdventurePageStatsCurrent");
        }

        [Then("verschijnt het max level bevestigingsbericht")]
        public void ThenVerschijntHetMaxLevelBevestigingsbericht()
        {
            _AdventurePlayPage.AssertConfirmationMessageForMaxLevel();
        }

        [When("de clicker blokkade wordt uitgeschakeld")]
        public void WhenDeClickerBlokkadeWordtUitgeschakeld()
        {
            _AdventurePlayPage.EnableClickerButton();
        }


        [When("in totaal {int} keer de typer blokkade wordt uitgeschakeld en vervolgens het bericht {string} wordt getypt")]
        public void WhenInTotaalKeerDeTyperBlokkadeWordtUitgeschakeldEnVervolgensHetBerichtWordtGetypt(int iterations, string message)
        {
            _AdventurePlayPage.EnableTyperButtonAndTypeMessage(iterations, message);
        }


        [Then("zijn de waarden voor alle stats gelijk aan level 10")]
        public void ThenZijnDeWaardenVoorAlleStatsGelijkAanLevel()
        {
            _AdventurePlayPage.AssertAllStatsHaveMaxLevel();
        }


        [When("het {string} bestand wordt geüpload")]
        public void WhenHetBestandWordtGeupload(string fileName)
        {
            _AdventurePlayPage.UploadFile(fileName);
        }

        [When("gedurende {int} iteraties wordt de uploaderblokkade uitgeschakeld met uploaden {string} gevolgd door {string} bestand")]
        public void WhenGedurendeIteratiesWordtDeUploaderblokkadeUitgeschakeldMetUploadenGevolgdDoorBestand(int aantalIteraties, string fileName1, string fileName2)
        {
            _AdventurePlayPage.EnableUploaderElementAndUploadFile(aantalIteraties, fileName1, fileName2);
        }



        [When("het bericht {string} wordt getypt")]
        public void WhenHetBerichtWordtGetypt(string message)
        {
            _AdventurePlayPage.TypeMessage(message);
        }


        [When("de slider voor {int} procent naar rechts wordt geschoven")]
        public void WhenDeSliderVoorProcentNaarRechtsWordtGeschoven(int percentage)
        {
            _AdventurePlayPage.SlideToRight(percentage);
        }

        [Then("komen de getoonde stats op de adventure page overeen met de getoonde stats van de prepare page")]
        public void ThenKomenDeGetoondeStatsOpDeAdventurePageOvereenMetDeGetoondeStatsVanDePreparePage()
        {
            _AdventurePlayPage.AssertStatsWithPreparePlayPage();
        }


        [Then("zijn de waarden voor stats en level {int} hoger dan bij aanvang op de adventure play page pagina")]
        public void ThenZijnDeWaardenVoorStatsEnLevelHogerDanBijAanvangOpDeAdventurePlayPagePagina(int totalAmountIncreasedFromStart)
        {
            _AdventurePlayPage.AssertStatsAreIncreasedByCorrectAmount(totalAmountIncreasedFromStart);
        }



        [Then("verschijnt voor de task {string} geen bevestigingsbericht")]
        public void ThenVerschijntVoorDeTaskGeenBevestigingsbericht(string task)
        {
            if (!Enum.TryParse<TaskType>(task, true, out _)) throw new ArgumentException($"Ongeldige task opgegeven in feature file '{task}'.");
            _AdventurePlayPage.AssertTaskConfirmationMessageForSpecificTaskIsNotVisible(task);
        }

        [Then("is het element van de task {string} enabled")]
        public void ThenIsHetElementVanDeTaskEnabled(string task)
        {
            _AdventurePlayPage.AssertTaskElementState(task, shouldBeEnabled: true);
        }

        [Then("is het element van de task {string} disabled")]
        public void ThenIsHetElementVanDeTaskDisabled(string task)
        {
            _AdventurePlayPage.AssertTaskElementState(task, shouldBeEnabled: false);
        }



        [Then(@"is de ""(.*)"" button (wel|niet) zichtbaar")]
        public void ThenIsDeButtonWelOfNietZichtbaar(string buttonText, string zichtbaar)
        {
            _AdventurePlayPage.AssertButtonTextVisibility(
                buttonText,
                zichtbaar == "wel"
            );
        }
    }
}
