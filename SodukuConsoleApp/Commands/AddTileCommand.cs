using Sudoku;
using System;
using System.Text.RegularExpressions;

namespace SodukuConsoleApp.Command
{
    public class AddTileCommand : SudokuCommand
    {
        private const string RowIndexGroupName = "rowIndex";
        private const string ColumnIndexGroupName = "columnIndex";
        private const string TileValueGroupName = "tileValue";

        protected override string CommandPattern => @"add (?<" + RowIndexGroupName + @">\d) (?<" + ColumnIndexGroupName + @">\d) (?<" + TileValueGroupName + @">\d)";

        public AddTileCommand(Board board) : base(board)
        {
        }

        public override Action<string> Execute => command =>
        {
            var arguments = Regex.Match(command, CommandPattern);

            var rowIndex = int.Parse(arguments.Groups[RowIndexGroupName].Value);
            var columnIndex = int.Parse(arguments.Groups[ColumnIndexGroupName].Value);
            var value = int.Parse(arguments.Groups[TileValueGroupName].Value);

            _board.SetTileValue(rowIndex, columnIndex, value);
        };

        public override string Description => $"Add values using \"{CommandPattern}\"";
    }
}
