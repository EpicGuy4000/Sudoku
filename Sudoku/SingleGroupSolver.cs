using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    public class SingleGroupSolver : ITileObserver
    {
        public void Notify(Tile tile)
        {
            if (!tile.IsSolved)
            {
                return;
            }

            var tiles = new List<Tile>(tile.Row.Tiles).Union(tile.Column.Tiles).Union(tile.Quadrant.Tiles);

            foreach (var neighborhoodTile in tiles)
            {
                if (!neighborhoodTile.IsSolved)
                {
                    neighborhoodTile.MarkValueAsImpossible(tile.Value);
                }
            }
        }
    }
}
