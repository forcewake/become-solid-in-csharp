using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BecomeSolid.Refactoring.Day1.Commands;
using Telegram.Bot;

namespace BecomeSolid.Refactoring.Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            Run().Wait();
        }

        static async Task Run()
        {
            var bot = new Api("94022128:AAEzeODdvW2yNKkUoeQUbrw2zA63SXatu4I");
             var me = await bot.GetMe();
            var weatherCommand = new WeatherCommand(bot);
            var defaultCommand = new DefaultCommand(bot);
            var commands = new ICommand[] {weatherCommand, defaultCommand };

            Console.WriteLine("Hello my name is {0}", me.Username);

            var offset = 0;

            while (true)
            {
                var updates = await bot.GetUpdates(offset);

                foreach (var update in updates)
                {
                    foreach (var command in commands)
                    {
                        if (command.IsApplicable(update))
                        {
                            var context = new Dictionary<string, object>()
                            {
                                {"message", update.Message.Text},
                                {"update", update}
                            };

                            command.Execute(context);
                        }
                    }

                    offset = update.Id + 1;
                }

                await Task.Delay(1000);
            }
        }
    }
}
