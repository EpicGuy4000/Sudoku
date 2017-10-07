using System;
using System.Collections.Generic;
using System.Linq;

namespace SodukuConsoleApp.Command
{
    public class HelpCommand : BaseCommand
    {
        private readonly ICommand[] _commands;

        public HelpCommand(IEnumerable<ICommand> commands)
        {
            _commands = commands.Union(new[] { this }).ToArray();
        }

        public override Action<string> Execute => command => Console.WriteLine(string.Join("\n", _commands.Select(registeredCommand => registeredCommand.Description)));

        public override string Description => $"Use \"{CommandPattern}\" to show help about other methods";

        protected override string CommandPattern => "help";
    }
}
