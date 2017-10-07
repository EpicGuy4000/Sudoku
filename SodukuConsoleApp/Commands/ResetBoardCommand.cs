using Sudoku;
using System;

namespace SodukuConsoleApp.Command
{
    public class ResetBoardCommand : SudokuCommand
    {
        public ResetBoardCommand(Board board) : base(board)
        {
        }

        public override Action<string> Execute => command => _board.Reset();

        public override string Description => $"Reset the board state using \"{CommandPattern}\"";

        protected override string CommandPattern => "reset";
    }
}
