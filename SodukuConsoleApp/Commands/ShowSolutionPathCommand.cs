using Sudoku;
using System;

namespace SodukuConsoleApp.Command
{
    public class ShowSolutionPathCommand : SudokuCommand
    {
        public ShowSolutionPathCommand(Board board) : base(board)
        {
        }

        public override Action<string> Execute => command =>
        {
            foreach (var line in _board.GetSolutionPath)
            {
                Console.WriteLine(line);
            }
        };

        public override string Description => $"Show solution path once the board is solved using \"{CommandPattern}\"";

        protected override string CommandPattern => "path";
    }
}
