using Sudoku;
using System;

namespace SodukuConsoleApp.Command
{
    public class CheckBoardStateCommand : SudokuCommand
    {
        public CheckBoardStateCommand(Board board) : base(board)
        {
        }

        public override Action<string> Execute => command => Console.WriteLine(_board);

        public override string Description => $"Check the board state using \"{CommandPattern}\"";

        protected override string CommandPattern => "check";
    }
}
