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
    public sealed class PreparePlayStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IPreparePlayPage _PreparePlayPage;

        public PreparePlayStepDefinitions(ScenarioContext scenarioContext, IPreparePlayPage preparePlayPage)
        {
            _scenarioContext = scenarioContext;
            _PreparePlayPage = preparePlayPage;
        }

        [Given("dat ik op de Click here to play button klik")]
        public void GivenDatIkOpDeClickHereToPlayButtonKlik()
        {
            _PreparePlayPage.ClickClickHereToPlaybtn();
        }


        [Given("dat ik de character name invul en een build selecteer met onderstaande details")]
        [When("dat ik de character name invul en een build selecteer met onderstaande details")]
        public void GivenDatIkDeCharacterNameInvulEnEenBuildSelecteerMetOnderstaandeDetails(Table table)
        {
            var characterNameAndBuild = table.CreateInstance<CharacterOverviewDto>();

            _PreparePlayPage.ChooseNameAndSelectBuild(characterNameAndBuild);

            _scenarioContext.Set(characterNameAndBuild);
        }

        [Then("zie ik geen character name error message")]
        public void ThenZieIkGeenCharacterNameErrorMessage()
        {
            _PreparePlayPage.AssertNameErrorMessageIsNotVisible();
        }


        [When("ik op de Start! button klik")]
        public void WhenIkOpDeStartButtonKlik()
        {
            _PreparePlayPage.ClickStartBtn();
        }

        [Then("zie ik de character name error message: {string}")]
        public void ThenZieIkDeCharacterNameErrorMessage(string expErrMsg)
        {
            _PreparePlayPage.AssertErrMsgCharacterName(expErrMsg);
        }

        [Then("zie ik de juiste character name, build type en stats waarden in het overzicht")]
        public void ThenZieIkDeJuisteCharacterNameBuildTypeEnStatsWaardenInHetOverzicht(Table table)
        {
            var expData = table.CreateInstance<CharacterOverviewDto>();

            _PreparePlayPage.AssertDataBindingCharacterName(expData);
            _PreparePlayPage.AssertDataBindingBuildType(expData);
            _PreparePlayPage.AssertStats(expData);

            _scenarioContext.Set(expData);
        }




    }
}
