using Reversi.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Domain
{
    public interface IReversiHeuristic
    {
        int Score( IGrid<Player> board, Player player );
    }

    public class StoneCountHeuristic : IReversiHeuristic
    {
        public int Score( IGrid<Player> board, Player player )
        {
            return board.Count( player ) - board.Count( player.Other );
        }
    }

    public class WeightedStoneCountHeuristic : IReversiHeuristic
    {
        private readonly int cornerWeight;

        private readonly int rimWeight;

        public WeightedStoneCountHeuristic(int cornerWeight = 4, int rimWeight = 2)
        {
            this.cornerWeight = cornerWeight;
            this.rimWeight = rimWeight;
        }

        public int Score( IGrid<Player> board, Player player )
        {
            var result = 0;

            foreach ( var p in board.AllPositions() )
            {
                var owner = board[p];

                if ( owner != null )
                {
                    var factor = owner == player ? 1 : -1;
                    var weight = Weight( board, p );

                    result = result + factor * weight;
                }
            }

            return result;
        }

        private int Weight( IGrid<Player> board, Vector2D p )
        {
            if ( board.InCorner( p ) )
            {
                return cornerWeight;
            }
            else if ( board.OnRim( p ) )
            {
                return rimWeight;
            }
            else
            {
                return 1;
            }
        }
    }
}
