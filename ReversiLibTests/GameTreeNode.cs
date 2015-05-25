using Reversi.AI;
using Reversi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiLibTests
{
    public class GameTreeNode<MOVE> : IGameTreeNode<GameTreeNode<MOVE>, MOVE>
    {
        private readonly Dictionary<MOVE, GameTreeNode<MOVE>> children;

        private readonly Player player;

        private readonly int heuristic;

        public GameTreeNode( Player player, int heuristic )
        {
            this.children = new Dictionary<MOVE, GameTreeNode<MOVE>>();
            this.player = player;
            this.heuristic = heuristic;
        }

        public GameTreeNode<MOVE> this[MOVE move]
        {
            get
            {
                if ( !children.ContainsKey( move ) )
                {
                    throw new ArgumentException();
                }
                else
                {
                    return children[move];
                }
            }
            set
            {
                children[move] = value;
            }
        }

        public void AddChild( MOVE move, GameTreeNode<MOVE> child )
        {
            this[move] = child;
        }

        public IEnumerable<MOVE> ValidMoves
        {
            get
            {
                if ( IsLeaf )
                {
                    throw new InvalidOperationException();
                }
                else
                {
                    return children.Keys;
                }
            }
        }

        GameTreeNode<MOVE> IGameTreeNode<GameTreeNode<MOVE>, MOVE>.this[MOVE move]
        {
            get
            {
                return this[move];
            }
        }

        public Player CurrentPlayer
        {
            get
            {
                if ( IsLeaf )
                {
                    throw new InvalidOperationException();
                }
                else
                {
                    return player;
                }
            }
        }

        public bool IsLeaf
        {
            get
            {
                return children.Count == 0;
            }
        }

        public int Heuristic
        {
            get
            {
                return heuristic;
            }
        }

        public override string ToString()
        {
            var prefix = this.player == Player.ONE ? "P" : "AI";
            var children = string.Join( ", ", this.children.Select( child => child.ToString() ) );

            return string.Format( "{0}[{1}:{2}]", prefix, heuristic, children );
        }
    }

    public class Heuristic<MOVE> : IHeuristic<GameTreeNode<MOVE>>
    {
        public int Score( GameTreeNode<MOVE> node, Player player )
        {
            if ( player == Player.ONE )
            {
                return -node.Heuristic;
            }
            else
            {
                return node.Heuristic;
            }
        }
    }
}
