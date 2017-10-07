using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    public class Board
    {
        public TileGroup[] Rows { get; }

        public TileGroup[] Columns { get; }

        public TileGroup[] Quadrants { get; }

        private readonly SolutionLogger _solutionLogger;

        public Board()
        {
            var singleGroupSolver = new SingleGroupSolver();
            var possibleValueSolver = new PossibleValuesPerGroupSolver();
            _solutionLogger = new SolutionLogger();

            Rows = Enumerable.Range(1, 9).Select(index => new TileGroup(index)).ToArray();
            Columns = Enumerable.Range(1, 9).Select(index => new TileGroup(index)).ToArray();
            Quadrants = Enumerable.Range(1, 9).Select(index => new TileGroup(index)).ToArray();

            for (int rowIndex = 0; rowIndex < 9; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < 9; columnIndex++)
                {
                    var quadrant = GetQuadrant(rowIndex, columnIndex);
                    var tile = new Tile(Rows[rowIndex], Columns[columnIndex], quadrant);
                    tile.Register(_solutionLogger);
                    tile.Register(singleGroupSolver);
                    tile.Register(possibleValueSolver);

                    var quadrantIndex = quadrant.Tiles.TakeWhile(quadrantTile => quadrantTile != null).Count();
                    quadrant[quadrantIndex] = tile;
                    Rows[rowIndex][columnIndex] = tile;
                    Columns[columnIndex][rowIndex] = tile;
                }
            }
        }

        public void Reset()
        {
            _solutionLogger.SolutionPath.Clear();
            foreach (var tileGroup in Rows.Union(Columns).Union(Quadrants))
            {
                tileGroup.Reset();
            }
        }

        public void SetTileValue(int rowIndex, int columnIndex, int value)
        {
            if (value < 1 || value > 9)
            {
                throw new ArgumentException(value.ToString(), nameof(value));
            }
            Rows[rowIndex - 1][columnIndex - 1].Value = value;
        }

        public bool IsSolved => Rows.Aggregate(true, (soFar, currentRow) => soFar && currentRow.IsSolved);

        public IReadOnlyCollection<string> GetSolutionPath => _solutionLogger.SolutionPath;

        private TileGroup GetQuadrant(int rowIndex, int columnIndex)
        {
            int quadrantRow = rowIndex / 3;
            int quadrantColumn = columnIndex / 3;

            var quadrantIndex = quadrantRow * 3 + quadrantColumn;

            return Quadrants[quadrantIndex];
        }

        public override string ToString()
        {
            var separator = "\n-------------------------------------\n";
            var hardSeparator = "\n=====================================\n";

            return $"{Rows[0]}{separator}{Rows[1]}{separator}{Rows[2]}{hardSeparator}{Rows[3]}{separator}{Rows[4]}{separator}{Rows[5]}{hardSeparator}{Rows[6]}{separator}{Rows[7]}{separator}{Rows[8]}";
        }
    }
}
