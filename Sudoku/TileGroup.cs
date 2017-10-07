using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    public class TileGroup
    {
        private Tile[] _tiles;

        public TileGroup(int index)
        {
            Index = index;
            _tiles = new Tile[9];
            TilesPerPossibleValue = new Dictionary<int, HashSet<Tile>>();

            for (int i = 1; i < 10; i++)
            {
                TilesPerPossibleValue.Add(i, new HashSet<Tile>());
            }
        }

        public int Index { get; }

        public Tile this[int index]
        {
            get
            {
                return _tiles[index];
            }
            set
            {
                _tiles[index] = value;

                foreach (var possibleValue in value.PossibleValues)
                {
                    TilesPerPossibleValue[possibleValue].Add(value);
                }
            }
        }

        public void Reset()
        {
            foreach (var tile in _tiles)
            {
                tile.Reset();

                foreach (var possibleValue in tile.PossibleValues)
                {
                    TilesPerPossibleValue[possibleValue].Add(tile);
                }
            }
        }

        public IReadOnlyCollection<Tile> Tiles => _tiles;

        public bool IsSolved => _tiles.Aggregate(true, (allAreSolved, tile) => allAreSolved && tile.IsSolved);

        public override string ToString() => $" {this[0]} | {this[1]} | {this[2]} || {this[3]} | {this[4]} | {this[5]} || {this[6]} | {this[7]} | {this[8]} ";

        public Dictionary<int, HashSet<Tile>> TilesPerPossibleValue { get; }
    }
}
