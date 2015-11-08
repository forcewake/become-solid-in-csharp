using System;
using System.Collections.Generic;
using System.Linq;
using BecomeSolid.Refactoring.Day1.Builders;
using BecomeSolid.Refactoring.Day1.Services;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BecomeSolid.Refactoring.Day1.Commands
{
    class WeatherCommand : ICommand
    {
        private Api _api;

        WeatherService weatherService = new WeatherService();
        MessageBuilder messageBuilder = new MessageBuilder();

        public WeatherCommand(Api api)
        {
            _api = api;
        }

        public async void Execute(Dictionary<string, object> context)
        {
            var inputMessage = context["message"] as string;
            var update = context["update"] as Update;

            var messageParts = inputMessage.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var city = messageParts.Length == 1 ? "Minsk" : messageParts.Skip(1).First();

            var weather = weatherService.GetWeatherForCity(city);

            Console.WriteLine(string.Format("temp is: {0}", weather.Description));

            var message = messageBuilder
                .InCity(weather.Name)
                .WithDescription(weather.Description)
                .WithTemperature(weather.Temperature)
                .Build();

            var t = await _api.SendTextMessage(update.Message.Chat.Id, message);
            Console.WriteLine("Echo Message: {0}", message);
        }

        public bool IsApplicable(Update update)
        {
            var messageText = update.Message.Text;
            var isTextMessage = update.Message.Type == MessageType.TextMessage;
            return isTextMessage && messageText.ToLowerInvariant().StartsWith("/weather");
        }
    }
}