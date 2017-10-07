using SodukuConsoleApp.Command;
using System;
using System.Collections.Generic;

namespace SodukuConsoleApp
{
    public class CommandExecutor
    {
        private readonly IReadOnlyCollection<ICommand> _commands;

        public CommandExecutor(IReadOnlyCollection<ICommand> commands)
        {
            _commands = commands;
        }

        public void Run()
        {
            string commandLine;

            while (true)
            {
                commandLine = Console.ReadLine();

                try
                {
                    foreach (var command in _commands)
                    {
                        if (command.Matches(commandLine))
                        {
                            command.Execute(commandLine);
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Invalid command {commandLine}. Exception throw {ex.Message}");
                    throw;
                }
            }
        }
    }
}
