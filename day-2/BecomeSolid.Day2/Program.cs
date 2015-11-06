using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BecomeSolid.Day2.Commands;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BecomeSolid.Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                 .MinimumLevel.Debug()
                 .WriteTo.ColoredConsole()
                 .CreateLogger();

            var dwellerBot = new DwellerBot();
            dwellerBot.Run().Wait();
        }
    }

    public class DwellerBot
    {
        internal const string BotName = @"@DwellerBot";
        internal const string OwnerUsername = "angelore";
        internal const int OwnerId = 99541817;

        private readonly Api _bot;

        private Random _rng;

        internal int Offset;
        internal DateTime LaunchTime;
        internal int CommandsProcessed;
        internal int ErrorCount;
        internal bool IsOnline = true;

        internal Dictionary<string, ICommand> Commands { get; set; }

        public DwellerBot()
        {
            _rng = new Random();

            // Get bot api token
            _bot = new Api("");

            Offset = 0;
            CommandsProcessed = 0;
            ErrorCount = 0;
            LaunchTime = DateTime.Now.AddHours(3);

            Commands = new Dictionary<string, ICommand>
            {
                {@"/rate", new RateNbrbCommand(_bot)},
            };
        }

        public async Task Run()
        {
            var me = await _bot.GetMe();

            Log.Logger.Information("{0} is online and fully functional." + Environment.NewLine, me.Username);

            while (IsOnline)
            {
                Update[] updates = new Update[0];
                try
                {
                    updates = await _bot.GetUpdates(Offset);
                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, "An error has occured while receiving updates.");
                    ErrorCount++;
                }

                foreach (var update in updates)
                {
                    if (update.Message.Text != null)
                    {
                        Log.Logger.Debug("A message in chat {0} from user {1}: {2}", update.Message.Chat.Id, update.Message.From.Username, update.Message.Text);

                        Dictionary<string, string> parsedMessage = new Dictionary<string, string>();
                        try
                        {
                            parsedMessage = ParseCommand(update.Message.Text);
                            parsedMessage.Add("interpretedCommand", InterpretCommand(parsedMessage["command"]));

                        }
                        catch (Exception ex)
                        {
                            Log.Logger.Error(ex, "An error has occured during message parsing.");
                            ErrorCount++;
                        }
                        if (Commands.ContainsKey(parsedMessage["command"]))
                        {
                            try
                            {
                                await Commands[parsedMessage["command"]].ExecuteAsync(update, parsedMessage);
                                CommandsProcessed++;
                            }
                            catch (Exception ex)
                            {
                                Log.Logger.Error(ex, "An error has occured during {0} command.", parsedMessage["command"]);
                                ErrorCount++;
                            }
                        }
                        else if (Commands.ContainsKey(parsedMessage["interpretedCommand"]))// Check if the command mas typed in a russian layout
                        {
                            try
                            {
                                await Commands[parsedMessage["interpretedCommand"]].ExecuteAsync(update, parsedMessage);
                                CommandsProcessed++;
                            }
                            catch (Exception ex)
                            {
                                Log.Logger.Error(ex, "An error has occured during {0} command.", parsedMessage["interpretedCommand"]);
                                ErrorCount++;
                            }
                        }
                    }

                    Offset = update.Id + 1;
                }

                await Task.Delay(1000);
            }
        }

        private readonly Regex _fullCommandRegex = new Regex(@"(?<=^/\w+)@\w+"); // Returns bot name from command (/com@botname => @botname)
        private readonly Regex _commandRegex = new Regex(@"^/\w+"); // Returns command (/com => /com)

        internal Dictionary<string, string> ParseCommand(string input)
        {
            var result = new Dictionary<string, string>();

            if (_commandRegex.IsMatch(input))
            {
                var fullCommandRegexIsMatch = _fullCommandRegex.IsMatch(input);
                Match fullCommandRegexMatch = null;
                if (!fullCommandRegexIsMatch || (fullCommandRegexMatch = _fullCommandRegex.Match(input)).Value == BotName)
                {
                    var commmandMatch = _commandRegex.Match(input);
                    result.Add("command", commmandMatch.Value);
                    if (!fullCommandRegexIsMatch)
                    {
                        var startIndex = commmandMatch.Index + commmandMatch.Length + 1;
                        if (input.Length > startIndex)
                            result.Add("message", input.Substring(startIndex));
                    }
                    else
                    {
                        var startIndex = fullCommandRegexMatch.Index + fullCommandRegexMatch.Length + 1;
                        if (input.Length > startIndex)
                            result.Add("message", input.Substring(startIndex));
                    }
                }
            }

            if (!result.ContainsKey("command"))
                result.Add("command", string.Empty);

            return result;
        }

        public static bool IsUserOwner(User user)
        {
            if (user.Id == OwnerId && user.Username.Equals(OwnerUsername))
                return true;

            return false;
        }

        // Temporary place for mistyped command converter. After moving to new architecture, it will be moved to a separate command
        public static string InterpretCommand(string inputCommand)
        {
            var rusCharSet = @"абвгдеёжзийклмнопрстуфхцчшщъьыэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЬЫЭЮЯ\""№;:?/.,";
            var engCharSet = @"f,dult`;pbqrkvyjghcnea[wxio]ms'.zF<DULT~:PBQRKVYJGHCNEA{WXIO}MS'>Z\@#$^&|/?";

            var a = rusCharSet.Length;
            var b = engCharSet.Length;

            inputCommand = inputCommand.ToLower();
            //List<char> outputSymbols = new List<char>();
            string result = "/";

            for (var i = 1; i < inputCommand.Length; i++)
            {
                //outputSymbols.Add(rusCharSet[rusCharSet.IndexOf(inputCommand[i])]);
                var ind = rusCharSet.IndexOf(inputCommand[i]);
                if (ind < 0)
                    return "";

                result += engCharSet[ind];
            }

            return result;
        }
    }
}
