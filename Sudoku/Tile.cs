using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    public class Tile
    {
        private readonly List<ITileObserver> _observers = new List<ITileObserver>();

        private static IEnumerable<int> TilePossibleValuesStartingState = Enumerable.Range(1, 9);

        private readonly List<int> _possibleValues = TilePossibleValuesStartingState.ToList();

        public Tile(TileGroup row, TileGroup column, TileGroup quadrant)
        {
            Row = row;
            Column = column;
            Quadrant = quadrant;
        }

        public TileGroup Row { get; }
        public TileGroup Column { get; }
        public TileGroup Quadrant { get; }

        public IReadOnlyCollection<int> PossibleValues => _possibleValues;

        public bool IsSolved => _possibleValues.Count == 1;

        public void Reset()
        {
            _possibleValues.Clear();
            _possibleValues.AddRange(TilePossibleValuesStartingState);
        }

        public int Value
        {
            get
            {
                return PossibleValues.Single();
            }
            set
            {
                foreach (var possibleValue in _possibleValues.ToArray())
                {
                    if (possibleValue != value)
                    {
                        MarkValueAsImpossibleInternal(possibleValue);
                    }
                }
                NotifyObservers();
            }
        }

        public void Register(ITileObserver observer)
        {
            _observers.Add(observer);
        }

        public void MarkValueAsImpossible(int value)
        {
            if (_possibleValues.Contains(value))
            {
                MarkValueAsImpossibleInternal(value);

                NotifyObservers();
            }
        }

        private void MarkValueAsImpossibleInternal(int value)
        {
            _possibleValues.Remove(value);
            Row.TilesPerPossibleValue[value].Remove(this);
            Column.TilesPerPossibleValue[value].Remove(this);
            Quadrant.TilesPerPossibleValue[value].Remove(this);
        }

        private void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Notify(this);
            }
        }

        public override string ToString() => IsSolved ? Value.ToString() : " ";
    }
}
