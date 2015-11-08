using System.Collections.Generic;
using Telegram.Bot.Types;

namespace BecomeSolid.Refactoring.Day1
{
    public interface ICommand
    {
        void Execute(Dictionary<string, object> context);
        bool IsApplicable(Update update);

    }
}