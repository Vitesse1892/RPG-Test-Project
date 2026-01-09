using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgUiTests.Ui
{
    public static class ConfirmationMessagesRepository
    {
        public static readonly Dictionary<TaskType, string> Messages = new()
        {
            [TaskType.clicker] = "Great job! You levelled up",
            [TaskType.uploader] = "File selected, level up!",
            [TaskType.typer] = "Dolar sit amet!",
            [TaskType.slider] = "Slid to the next level!"
        };

        public const string MaxLevelMessage = "You've reached the highest level!";
    }
}
