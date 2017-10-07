using System.Linq;

namespace Sudoku
{
    public class PointingPairSolver : ITileObserver
    {
        private readonly Board _board;

        public PointingPairSolver(Board board)
        {
            _board = board;
        }
        
        public void Notify(Tile tile)
        {
            var valuesUniqueToTileGroupWithinQuadrant = _board.Quadrants
                .SelectMany(quadrant => quadrant.TilesPerPossibleValue.Where(kvp => kvp.Value.Aggregate(kvp.Value.First().Row, (sharedRow, quadrantTile) => quadrantTile.Row.Equals(sharedRow) ? sharedRow : null) != null)
                .Select(kvp => new ValueTileGroupQuadrantTuple(kvp.Key, kvp.Value.First().Row, quadrant)))
                .ToArray();

            foreach (var value in valuesUniqueToTileGroupWithinQuadrant)
            {
                var tilesOutsideOfQuadrant = value.TileGroup.TilesPerPossibleValue[value.Value]
                    .Where(tileWithValue => tileWithValue.Quadrant != value.Quadrant)
                    .ToArray();

                foreach (var tileOutsideOfQuadrant in tilesOutsideOfQuadrant)
                {
                    tileOutsideOfQuadrant.MarkValueAsImpossible(value.Value);
                }
            }
            
            var valuesUniqueToQuadrantWithinTileGroup = _board.Rows.Union(_board.Columns)
                .SelectMany(tileGroup => tileGroup.TilesPerPossibleValue.Where(kvp => kvp.Value.Aggregate(kvp.Value.First().Quadrant, (sharedQuadrant, tileGroupTile) => tileGroupTile.Quadrant.Equals(sharedQuadrant) ? sharedQuadrant : null) != null)
                .Select(kvp => new ValueTileGroupQuadrantTuple(kvp.Key, tileGroup, kvp.Value.First().Quadrant)))
                .ToArray();

            foreach (var value in valuesUniqueToQuadrantWithinTileGroup)
            {
                var tilesOutsideOfTileGroup = value.Quadrant.TilesPerPossibleValue[value.Value]
                    .Where(tileWithValue => tileWithValue.Row != value.TileGroup && tileWithValue.Column != value.TileGroup)
                    .ToArray();

                foreach (var tileOutsideOfTileGroup in tilesOutsideOfTileGroup)
                {
                    tileOutsideOfTileGroup.MarkValueAsImpossible(value.Value);
                }
            }
        }

        private class ValueTileGroupQuadrantTuple
        {
            public int Value { get; }

            public TileGroup TileGroup { get; }

            public TileGroup Quadrant { get; }

            public ValueTileGroupQuadrantTuple(int value, TileGroup tileGroup, TileGroup quadrant)
            {
                Value = value;
                TileGroup = tileGroup;
                Quadrant = quadrant;
            }
        }
    }
}
