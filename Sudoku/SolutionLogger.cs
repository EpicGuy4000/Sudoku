using System.Collections.Generic;
namespace Sudoku
{
    public class SolutionLogger : ITileObserver
    {
        public SolutionLogger()
        {
            SolutionPath = new List<string>();
        }

        public List<string> SolutionPath { get; }

        public void Notify(Tile tile)
        {
            if (tile.IsSolved)
            {
                SolutionPath.Add($"Setting tile {tile.Row.Index} {tile.Column.Index} to value {tile.Value}");
            }
        }
    }
}
