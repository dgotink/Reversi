using Reversi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.AI
{
    public class Collapser<NODE, MOVE> : IGameTreeNode<Collapser<NODE, MOVE>, MoveSequence<MOVE>>
        where NODE : IGameTreeNode<NODE, MOVE>
    {
        private NODE node;

        public Collapser( NODE node )
        {
            this.node = node;
        }

        public IEnumerable<MoveSequence<MOVE>> ValidMoves
        {
            get
            {
                if ( IsLeaf )
                {
                    throw new InvalidOperationException( "Leaf nodes have no children" );
                }
                else
                {
                    return EnumerateValidMoves( node, MoveSequence<MOVE>.Empty, node.CurrentPlayer );
                }
            }
        }

        private IEnumerable<MoveSequence<MOVE>> EnumerateValidMoves( NODE node, MoveSequence<MOVE> prefix, Player player )
        {
            if ( node.IsLeaf || node.CurrentPlayer != player )
            {
                yield return prefix;
            }
            else
            {
                foreach ( var validMove in node.ValidMoves )
                {
                    var child = node[validMove];
                    var extendedPrefix = prefix.Append( validMove );

                    foreach ( var moveSequence in EnumerateValidMoves( child, extendedPrefix, player ) )
                    {
                        yield return moveSequence;
                    }
                }
            }
        }

        public Collapser<NODE, MOVE> this[MoveSequence<MOVE> sequence]
        {
            get
            {
                var current = node;

                foreach ( var move in sequence.Moves )
                {
                    current = current[move];
                }

                return new Collapser<NODE, MOVE>( current );
            }
        }

        public Player CurrentPlayer
        {
            get
            {
                return node.CurrentPlayer;
            }
        }

        public bool IsLeaf
        {
            get
            {
                return node.IsLeaf;
            }
        }
    }
}
