using FluentAssertions;
using Io.Cucumber.Messages.Types;
using Reqnroll;
using RpgFramework;
using RpgUiTests.Models;
using RpgUiTests.Pages.Base;
using RpgUiTests.Pages.PreparePage;
using RpGuiTests.Domain;
using System;



namespace RpgUiTests.StepDefinitions
{
    [Binding]
    public sealed class BaseStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IBasePage _basePage;
        private readonly IPreparePlayPage _preparePlayPage;


        public BaseStepDefinitions(ScenarioContext scenarioContext, IBasePage basePage, IPreparePlayPage preparePlayPage)
        {
            _scenarioContext = scenarioContext;
            _basePage = basePage;
            _preparePlayPage = preparePlayPage;
        }


        [Given("ik op de button klik met de tekst {string}")]
        [When("ik op de button klik met de tekst {string}")]
        public void GivenIkOpDeButtonKlikMetDeTekst(string buttonText)
        {
            _basePage.ClickButtonByText(buttonText);
            //Als button tekst gelijk is aan "Play again" leg dan de nieuwe beginwaarden voor stats vast
            
            if (buttonText.Equals("Play again", StringComparison.OrdinalIgnoreCase))
            {
                //Leg nieuwe beginwaarden vast indien je opnieuw gaat spelen
                var currentStats = _basePage.GetCharacterStats();
                _scenarioContext.Set(currentStats, "AdventurePageStatsCurrent");

            }
        }


        [Then("wordt de juiste pagina getoond met de card {string}")]
        public void ThenWordtDeJuistePaginaGetoondMetDeCard(string cardName)
        {
            _basePage.IsOnPage(cardName).Should().BeTrue($"Expected play page with card '{cardName}' to be shown");
        }

        

        [Then("zie ik character {string} geselecteerd met de juiste startwaarden voor stats")]
        public void ThenZieIkCharacterGeselecteerdMetDeJuisteStartwaardenVoorStats(string buildTypeText)
        {
            if (!Enum.TryParse<BuildType>(buildTypeText, ignoreCase: true, out var buildType))
                throw new ArgumentException($"Onbekend build type: '{buildTypeText}'");

            var expectedStats = BuildStatsRepository.Stats[buildType];

            // Combineer tot expected overview
            var expectedOverview = new CharacterOverviewDto
            {
                BuildType = buildType,
                Strength = expectedStats.Strength,
                Agility = expectedStats.Agility,
                Wisdom = expectedStats.Wisdom,
                Magic = expectedStats.Magic,
                Level = expectedStats.Level
            };

            _preparePlayPage.AssertDataBindingBuildType(expectedOverview);
            _preparePlayPage.AssertStats(expectedOverview);
        }
    }
}
