using RpgUiTests.UI;

namespace RpgUiTests.Ui
{
    public static class ConfirmationMessages
    {
        public static string GetMessage(TaskType taskType)
        {
            if (ConfirmationMessagesRepository.Messages.TryGetValue(taskType, out var message))
                return message;

            throw new ArgumentOutOfRangeException(nameof(taskType), taskType, $"TaskType '{taskType}' heeft geen bijbehorende bevestigingsbericht in de repository.");
        }

        public static string GetMaxLevelMessage() => ConfirmationMessagesRepository.MaxLevelMessage;
    }
}
