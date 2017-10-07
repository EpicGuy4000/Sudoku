using SodukuConsoleApp.Command;
using System;
using System.Collections.Generic;
using System.Linq;

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
                    var commandToExecute = _commands.FirstOrDefault(command => command.Matches(commandLine));

                    if (commandToExecute != null)
                    {
                        commandToExecute.Execute(commandLine);
                    }
                    else
                    {
                        Console.WriteLine($"Invalid command {commandLine}.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Invalid command {commandLine}. Exception throw {ex.Message}");
                }
            }
        }
    }
}
