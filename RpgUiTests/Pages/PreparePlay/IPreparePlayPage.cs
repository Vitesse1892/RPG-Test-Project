using RpgUiTests.Models;

namespace RpgUiTests.Pages.PreparePage
{
    public interface IPreparePlayPage
    {
        void ChooseName(CharacterOverviewDto character);
        void SelectBuild(CharacterOverviewDto character);
        void ChooseNameAndSelectBuild(CharacterOverviewDto character);
        void AssertErrMsgCharacterName(string errorMessage);
        void AssertNameErrorMessageIsNotVisible();
        void AssertDataBindingCharacterName(CharacterOverviewDto expectedData);
        void AssertDataBindingBuildType(CharacterOverviewDto expectedData);
        void AssertStats(CharacterOverviewDto expectedData);
    }
}
