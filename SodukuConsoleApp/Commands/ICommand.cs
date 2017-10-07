using System;

namespace SodukuConsoleApp.Command
{
    public interface ICommand
    {
        bool Matches(string commandString);

        Action<string> Execute { get; }

        string Description { get; }
    }
}
