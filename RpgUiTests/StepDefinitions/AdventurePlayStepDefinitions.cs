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
    public sealed class AdventurePlayStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly ICommonPage _AdventurePlayPage;

        public AdventurePlayStepDefinitions(ScenarioContext scenarioContext, ICommonPage adventurePlayPage)
        {
            _scenarioContext = scenarioContext;
            _AdventurePlayPage = adventurePlayPage;
        }

        




    }
}
