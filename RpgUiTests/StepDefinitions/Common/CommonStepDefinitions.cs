using FluentAssertions;
using Io.Cucumber.Messages.Types;
using Reqnroll;
using RpgFramework;
using RpgUiTests.Models;
using RpgUiTests.Pages;
using System;


namespace RpgUiTests.StepDefinitions
{
    [Binding]
    public sealed class CommonStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly ICommonPage _CommonPage;


        public CommonStepDefinitions(ScenarioContext scenarioContext, ICommonPage commonPage)
        {
            _scenarioContext = scenarioContext;
            _CommonPage = commonPage;

        }


        [Then("wordt de juiste pagina getoond met de card {string}")]
        public void ThenWordtDeJuistePaginaGetoondMetDeCard(string cardName)
        {
            _CommonPage.IsOnPage(cardName).Should().BeTrue($"Expected play page with card '{cardName}' to be shown");
        }


    }
}
