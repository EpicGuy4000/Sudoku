using SodukuConsoleApp.Command;
using Sudoku;
using System;
using System.Collections.Generic;

namespace SodukuConsoleApp
{

    class Program
    {
        private readonly List<ICommand> _commands;
        private readonly Board _board;

        public Program()
        {
            _board = new Board();
            _commands = new List<ICommand>
            {
                new AddTileCommand(_board),
                new CheckBoardStateCommand(_board),
                new ResetBoardCommand(_board),
                new ShowSolutionPathCommand(_board),
                new QuitCommand()
            };
        }

        public void Run()
        {
            Console.WriteLine("Welcome to sudoku console app!");

            var helpCommand = new HelpCommand(_commands);

            _commands.Add(helpCommand);

            helpCommand.Execute(null);

            new CommandExecutor(_commands).Run();
        }

        static void Main(string[] args)
        {
            new Program().Run();
        }
    }
}
