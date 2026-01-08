using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgUiTests.Domain
{
    public static class TaskTypeExtensions
    {
        public static string GetConfirmationMessage(this TaskType taskType)
        {
            return taskType switch
            {
                TaskType.clicker => "Great job! You levelled up",
                TaskType.uploader => "File selected, level up!",
                TaskType.typer => "Dolar sit amet!",
                TaskType.slider => "Slid to the next level!",
                _ => throw new ArgumentOutOfRangeException(nameof(taskType))
            };
        }
    }
}
