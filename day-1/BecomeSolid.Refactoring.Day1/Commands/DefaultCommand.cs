using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BecomeSolid.Refactoring.Day1.Commands
{
    class DefaultCommand : ICommand
    {
        private Api _api;


        public DefaultCommand(Api api)
        {
            _api = api;
        }
        public async void Execute(Dictionary<string, object> context)
        {
            var update = context["update"] as Update;

            await _api.SendChatAction(update.Message.Chat.Id, ChatAction.Typing);
            await Task.Delay(2000);
            var t = await _api.SendTextMessage(update.Message.Chat.Id, update.Message.Text);
            Console.WriteLine("Echo Message: {0}", update.Message.Text);

        }

        public bool IsApplicable(Update update)
        {
            return true;
        }
    }
}