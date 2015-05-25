using Reversi.Cells;
using Reversi.DataStructures;
using Reversi.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace ReversiGUI
{
    public class SquareViewModel
    {
        //***************
        private readonly ISquare square;
        public ISquare Square { get { return square; } }

        public ICell<Player> Owner { get { return square.Owner; } }

        private readonly ICell<bool> isCapturedBy = Cell.Create<bool>(false);
        public ICell<bool> IsCapturedBy { get { return isCapturedBy; } }

        public Vector2D Position { get { return square.Position; } }

        public ICell<bool> IsValidMove { get { return square.IsValidMove; } }

        private readonly PlaceStoneCommand stoneCommand;

        public ICommand StoneCommand { get { return stoneCommand; } }

        //***************
        public SquareViewModel(ISquare pSquare)
        {
            square = pSquare;
            stoneCommand = new PlaceStoneCommand(this);
        }

        public void PlaceStone()
        {
            square.PlaceStone();
        }

        public IEnumerable<Vector2D> CapturedBy()
        {
            return square.CapturedBy();
        }

        //***************
        private class PlaceStoneCommand : ICommand
        {
            private readonly SquareViewModel square;
            public event EventHandler CanExecuteChanged;

            public PlaceStoneCommand(SquareViewModel pSquare)
            {
                square = pSquare;
                square.IsValidMove.PropertyChanged += (sender, args) =>
                {
                    if (CanExecuteChanged != null)
                    {
                        CanExecuteChanged(this, new EventArgs());
                    }
                };
            }

            public bool CanExecute(object parameter)
            {
                return square.IsValidMove.Value;
            }

            public void Execute(object parameter)
            {
                square.PlaceStone();
            }
        }
    }
    //*************************************************************************
    public class RowViewModel
    {
        private readonly IList<SquareViewModel> columns;
        public IList<SquareViewModel> Columns { get { return columns; } }

        public RowViewModel(ISequence<ISquare> pColumns)
        {
            //initialize list
            columns = new List<SquareViewModel>();
            //fill list
            foreach (var pColumn in pColumns.ToEnumerable())
            {
                columns.Add(new SquareViewModel(pColumn));
            }
        }

        public SquareViewModel GetColumn(int column){
            return Columns[column];
        }
    }
    //**************************************************************************
    public class BoardViewModel
    {
        private readonly IList<RowViewModel> rows;
        public IList<RowViewModel> Rows { get { return rows; } }

        public BoardViewModel(IGrid<ISquare> pBoard)
        {
            //initialize list
            rows = new List<RowViewModel>();
            //fill list
            foreach (var row in pBoard.Columns())
            {
                rows.Add(new RowViewModel(row));
            }
        }

        public SquareViewModel getSquare(Vector2D position){
            return Rows[position.X].GetColumn(position.Y);
        }

        public void SetCapturedBy(Vector2D position)
        {
            SquareViewModel square = getSquare(position);
            IEnumerable<Vector2D> positions = square.CapturedBy();
            foreach (RowViewModel row in Rows)
            {
                foreach (SquareViewModel column in row.Columns)
                {
                    if (positions.Contains(column.Position)) column.IsCapturedBy.Value = true;
                    else column.IsCapturedBy.Value = false;
                }
            }
        }

        public void RemoveCapturedBy()
        {
            foreach (RowViewModel row in Rows)
            {
                foreach (SquareViewModel column in row.Columns)
                {
                    column.IsCapturedBy.Value = false;
                }
            }
        }
        
    }
}
