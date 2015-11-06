using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BecomeSolid.Day2.Commands
{
    class CommandBase : ICommand
    {
        protected Api Bot { get; private set; }

        public CommandBase(Api bot)
        {
            Bot = bot;
        }

        public virtual Task ExecuteAsync(Update update, Dictionary<string, string> parsedMessage)
        {
            throw new NotImplementedException();
        }
    }
}