using RpgUiTests.Models;

namespace RpgUiTests.Pages
{
    public interface IPreparePlayPage
    {
        void ClickClickHereToPlaybtn();
        void ChooseName(CharacterOverviewDto character);
        void SelectBuild(CharacterOverviewDto character);
        void ChooseNameAndSelectBuild(CharacterOverviewDto character);
        void ClickStartBtn();
        void AssertErrMsgCharacterName(string errorMessage);
        void AssertNameErrorMessageIsNotVisible();
        void AssertDataBindingCharacterName(CharacterOverviewDto expectedData);
        void AssertDataBindingBuildType(CharacterOverviewDto expectedData);
        void AssertStats(CharacterOverviewDto expectedData);
        void AssertStat(string statName, string actualValue, int expectedValue);
    }
}
