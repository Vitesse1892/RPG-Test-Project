using RpgUiTests.Models;

namespace RpgUiTests.Pages
{
    public interface IAdventurePlayPage
    {
        void ClickTheClickItBtn(int amountOfCliks);
        void AssertConfirmationMessageForFinishedTask(string task, string confirmationMessage);
        void UploadFile();
        void TypeMessage(string message);
        void SlideToRight(int percentage);
        void AssertStatsWithPreparePlayPage();
        void AssertStatsAreIncreasedByCorrectAmount(int totalAmountIncreasedFromStart);
        void AssertTaskConfirmationMessageForSpecificTaskIsNotVisible(string task);
        void AssertTaskElementIsDisabled(string task);
    }
}
