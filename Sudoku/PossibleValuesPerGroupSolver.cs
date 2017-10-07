using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    public class PossibleValuesPerGroupSolver : ITileObserver
    {
        public void Notify(Tile tile)
        {
            var solvables = GetPairsOfSolvables(tile.Row).Union(GetPairsOfSolvables(tile.Column)).Union(GetPairsOfSolvables(tile.Quadrant));

            foreach (var solvable in solvables)
            {
                var solvableTile = solvable.Value;

                if (!solvableTile.IsSolved)
                {
                    solvableTile.Value = solvable.Key;
                }
            }
        }

        private IEnumerable<KeyValuePair<int, Tile>> GetPairsOfSolvables(TileGroup tileGroup) =>
            tileGroup.TilesPerPossibleValue.Where(kvp => kvp.Value.Count == 1)
                .Select(kvp => new KeyValuePair<int, Tile>(kvp.Key, kvp.Value.First()));
    }
}
