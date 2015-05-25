using Reversi.AI;
using Reversi.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Domain
{
    internal class ReversiGameTree : IGameTreeNode<ReversiGameTree, Vector2D>
    {
        private readonly GameState state;

        private readonly Dictionary<Vector2D, ReversiGameTree> children;

        internal static ReversiGameTree Create( GameState state )
        {
            return new ReversiGameTree( state.Copy() );
        }

        public static ReversiGameTree Create( Game game )
        {
            return Create( game.State );
        }

        private ReversiGameTree( GameState state )
        {
            this.state = state;

            if ( !state.IsGameOver )
            {
                children = new Dictionary<Vector2D, ReversiGameTree>();

                foreach ( var move in state.ValidMoves )
                {
                    children[move] = null;
                }
            }
            else
            {
                children = null;
            }
        }

        public IEnumerable<Vector2D> ValidMoves
        {
            get
            {
                return state.ValidMoves;
            }
        }

        public ReversiGameTree this[Vector2D move]
        {
            get
            {
                if ( !children.ContainsKey(move))
                {
                    throw new ArgumentException( "Not a valid move" );
                }
                else
                {
                    var child = children[move];

                    if ( child == null )
                    {
                        var copy = state.Copy();
                        copy.MakeMove( move );

                        child = new ReversiGameTree( copy );

                        children[move] = child;
                    }

                    return child;
                }                
            }
        }

        public Player CurrentPlayer
        {
            get
            {
                return state.CurrentPlayer;
            }
        }

        public bool IsLeaf
        {
            get
            {
                return state.IsGameOver;
            }
        }

        public IGrid<Player> Board
        {
            get
            {
                return state.Board;
            }
        }
    }
}
