using System;

namespace SodukuConsoleApp.Command
{
    public class QuitCommand : BaseCommand
    {
        public override Action<string> Execute => command => Environment.Exit(0);

        public override string Description => $"Quit using \"{CommandPattern}\"";

        protected override string CommandPattern => @"quit";
    }
}
