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
    public sealed class PreparePlayStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IPreparePlayPage _PreparePlayPage;
        private readonly IBasePage _BasePage;

        public PreparePlayStepDefinitions(ScenarioContext scenarioContext, IPreparePlayPage preparePlayPage, IBasePage basePage)
        {
            _scenarioContext = scenarioContext;
            _PreparePlayPage = preparePlayPage;
            _BasePage = basePage;
        }


        [Given("dat ik de character name invul en een build selecteer met onderstaande details")]
        [When("dat ik de character name invul en een build selecteer met onderstaande details")]
        public void GivenDatIkDeCharacterNameInvulEnEenBuildSelecteerMetOnderstaandeDetails(Table table)
        {
            var characterNameAndBuild = table.CreateInstance<CharacterOverviewDto>();

            _PreparePlayPage.ChooseNameAndSelectBuild(characterNameAndBuild);
        }

        [Then("zie ik geen character name error message")]
        public void ThenZieIkGeenCharacterNameErrorMessage()
        {
            _PreparePlayPage.AssertNameErrorMessageIsNotVisible();
        }


        [Then("zie ik de character name error message: {string}")]
        public void ThenZieIkDeCharacterNameErrorMessage(string expErrMsg)
        {
            _PreparePlayPage.AssertErrMsgCharacterName(expErrMsg);
        }

        [Then("zie ik de juiste character name, build type en stats waarden in het overzicht")]
        public void ThenZieIkDeJuisteCharacterNameBuildTypeEnStatsWaardenInHetOverzicht(Table table)
        {
            // Data uit Gherkin, stats en level zijn nog leeg
            var input = table.CreateInstance<CharacterOverviewDto>();

            // Verwachte stats vanuit Domain
            var expectedStats = BuildStatsRepository.Stats[input.BuildType];

            // Combineer tot expected overview
            var expectedOverview = input with
            {
                Strength = expectedStats.Strength,
                Agility = expectedStats.Agility,
                Wisdom = expectedStats.Wisdom,
                Magic = expectedStats.Magic,
                Level = expectedStats.Level
            };

            _PreparePlayPage.AssertDataBindingCharacterName(expectedOverview);
            _PreparePlayPage.AssertDataBindingBuildType(expectedOverview);
            _PreparePlayPage.AssertStats(expectedOverview);
        }




    }
}
