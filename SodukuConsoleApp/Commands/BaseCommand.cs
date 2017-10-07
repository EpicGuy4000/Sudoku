using System;
using System.Text.RegularExpressions;

namespace SodukuConsoleApp.Command
{
    public abstract class BaseCommand : ICommand
    {
        public abstract Action<string> Execute { get; }

        public abstract string Description { get; }

        public bool Matches(string commandString)
            => Regex.IsMatch(commandString.ToLowerInvariant(), CommandPattern);

        protected abstract string CommandPattern { get; }
    }
}
