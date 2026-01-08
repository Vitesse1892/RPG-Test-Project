using FluentAssertions;
using Io.Cucumber.Messages.Types;
using Reqnroll;
using RpgFramework;
using RpgUiTests.Domain;
using RpgUiTests.Models;
using RpgUiTests.Pages;
using System;


namespace RpgUiTests.StepDefinitions
{
    [Binding]
    public sealed class AdventurePlayStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IPreparePlayPage _PreparePlayPage;
        private readonly IAdventurePlayPage _AdventurePlayPage;
        private readonly ICommonPage _CommonPage;

        public AdventurePlayStepDefinitions(ScenarioContext scenarioContext, IPreparePlayPage preparePlayPage, IAdventurePlayPage adventurePlayPage, ICommonPage commonPage)
        {
            _scenarioContext = scenarioContext;
            _PreparePlayPage = preparePlayPage;
            _AdventurePlayPage = adventurePlayPage;
            _CommonPage = commonPage;
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

            _PreparePlayPage.ClickClickHereToPlaybtn();
            _CommonPage.IsOnPage(cardNamePreparePlayPage);
            _PreparePlayPage.ChooseNameAndSelectBuild(characterNameAndBuild);

            var statsCharBuildPreparePage = _CommonPage.GetCharacterStats();
            _scenarioContext.Set(statsCharBuildPreparePage, "PreparePageStats"); 

            _PreparePlayPage.ClickStartBtn();
            _CommonPage.IsOnPage(cardNameAdventurePlayPage); //Bevestiging belangrijk, ivm assertion stats tussen prepare en adventure page icm gelijke elementen

            var statsCharBuildAdventurePage = _CommonPage.GetCharacterStats();
            _scenarioContext.Set(statsCharBuildPreparePage, "AdventurePageStatsFromStart");
            _scenarioContext.Set(statsCharBuildPreparePage, "AdventurePageStatsCurrent");
        }


        [When("de Click it! button {int} keer wordt ingedrukt")]
        public void WhenDeClickItButtonKeerWordtIngedrukt(int amountOfClicks)
        {
            _AdventurePlayPage.ClickTheClickItBtn(amountOfClicks);
        }


        [Then("verschijnt voor de task {string} het bijbehorende bevestigingsbericht")]
        public void ThenVerschijntVoorDeTaskHetBijbehorendeBevestigingsbericht(string task)
        {
            //Get confirmation message van domain extension die je vervolgens wil asserten
            if (!Enum.TryParse<TaskType>(task, true, out var taskType))
                throw new ArgumentException($"Ongeldige task opgegeven: '{task}'. Geldige waarden zijn: {string.Join(", ", Enum.GetNames(typeof(TaskType)))}");
            var confirmationMessage = taskType.GetConfirmationMessage();

            _AdventurePlayPage.AssertConfirmationMessageForFinishedTask(task, confirmationMessage);

            //Later wil je verifiëren of de huidige waarden voor stats en level zijn opgehoogd
            var statsCharBuildPreparePage = _CommonPage.GetCharacterStats();
            _scenarioContext.Set(statsCharBuildPreparePage, "AdventurePageStatsCurrent");
        }



        [When("een bestand wordt geüpload")]
        public void WhenEenBestandWordtGeupload()
        {
            _AdventurePlayPage.UploadFile();
        }

        [When("het bericht {string} wordt getypt")]
        public void WhenHetBerichtWordtGetypt(string message)
        {
            _AdventurePlayPage.TypeMessage(message);
        }


        [When("de slider voor {int} procent naar rechts wordt geschoven")]
        public void WhenDeSliderVoorProcentNaarRechtsWordtGeschoven(int percentage)
        {
            //Ik wil ivm robuustheid van de methode een percentage die een veelvoud van 10 is, omdat een percentage van 98 of 99 procent ook al kan leiden tot de confirmation message
            if (percentage < 0 || percentage > 100) throw new ArgumentOutOfRangeException(nameof(percentage), $"Percentage moet tussen 0 en 100 liggen. Invoer: {percentage}"); // Check of het percentage tussen 0 en 100 ligt
            if (percentage % 10 != 0) throw new ArgumentException($"Percentage moet een veelvoud van 10 zijn (0, 10, 20, …, 100). Invoer: {percentage}"); // Check of het percentage een veelvoud van 10 is

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

        [Then("is het element van de task {string} disabled")]
        public void ThenIsHetElementVanDeTaskDisabled(string task)
        {
            _AdventurePlayPage.AssertTaskElementIsDisabled(task);
        }




    }
}
