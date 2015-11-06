using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace BecomeSolid.Day2
{
    public interface ICommand
    {
        Task ExecuteAsync(Update update, Dictionary<string, string> parsedMessage);
    }
}
