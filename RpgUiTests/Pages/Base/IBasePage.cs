using RpgUiTests.Models;

namespace RpgUiTests.Pages.Base
{
    public interface IBasePage
    {
        public void ClickButtonByText(string btnTxt);
        bool IsOnPage(string cardName);
        CharacterOverviewDto GetCharacterStats();
    }
}
