using Sudoku;

namespace SodukuConsoleApp.Command
{
    public abstract class SudokuCommand : BaseCommand
    {
        protected Board _board;

        public SudokuCommand(Board board)
        {
            _board = board;
        }
    }
}
