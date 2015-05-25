using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.AI
{
    public class MoveSequence<MOVE>
    {
        public static MoveSequence<MOVE> Empty
        {
            get
            {
                return new MoveSequence<MOVE>();
            }
        }

        public static MoveSequence<MOVE> FromMoves( params MOVE[] moves )
        {
            var sequence = Empty;

            foreach ( var move in moves )
            {
                sequence = sequence.Append( move );
            }

            return sequence;
        }

        private class Node
        {
            private readonly Node previous;

            private readonly MOVE move;

            public Node( Node previous, MOVE move )
            {
                this.previous = previous;
                this.move = move;
            }

            public IEnumerable<MOVE> ToEnumerable
            {
                get
                {
                    if ( previous != null )
                    {
                        foreach ( var move in previous.ToEnumerable )
                        {
                            yield return move;
                        }
                    }

                    yield return this.move;
                }
            }

            public Node Previous { get { return previous; } }

            public MOVE Move { get { return move; } }
        }

        private readonly Node node;

        public MoveSequence()
            : this( null )
        {
            // NOP
        }

        private MoveSequence( Node node )
        {
            this.node = node;
        }

        public int Length
        {
            get
            {
                var result = 0;
                var current = node;

                while ( current != null )
                {
                    result++;
                    current = current.Previous;
                }

                return result;
            }
        }

        public MOVE this[int index]
        {
            get
            {
                index = Length - index - 1;
                var current = node;

                while ( index > 0 )
                {
                    current = current.Previous;
                    index--;
                }

                return current.Move;
            }
        }

        public IEnumerable<MOVE> Moves
        {
            get
            {
                if ( node != null )
                {
                    return this.node.ToEnumerable;
                }
                else
                {
                    return Enumerable.Empty<MOVE>();
                }
            }
        }

        public MoveSequence<MOVE> Append( MOVE move )
        {
            return new MoveSequence<MOVE>( new Node( this.node, move ) );
        }        

        public override string ToString()
        {
            return string.Join( "-", Moves.Select( x => x != null ? x.ToString() : "null" ) );
        }

        public override int GetHashCode()
        {
            var result = 0;

            foreach ( var move in Moves )
            {
                if ( move != null )
                {
                    result ^= move.GetHashCode();
                }
            }

            return result;
        }

        public override bool Equals( object obj )
        {
            return Equals( obj as MoveSequence<MOVE> );
        }

        public bool Equals( MoveSequence<MOVE> sequence )
        {
            var myNode = this.node;
            var hisNode = sequence.node;

            while ( myNode != null )
            {
                if ( hisNode == null )
                {
                    return false;
                }
                else if ( !AreEqualMoves( myNode.Move, hisNode.Move ) )
                {
                    return false;
                }
                else
                {
                    myNode = myNode.Previous;
                    hisNode = hisNode.Previous;
                }
            }

            return hisNode == null;
        }

        private static bool AreEqualMoves( MOVE x, MOVE y )
        {
            if ( x == null )
            {
                return y == null;
            }
            else
            {
                return x.Equals( y );
            }
        }

        public bool IsEmpty
        {
            get
            {
                return this.node == null;
            }
        }        
    }
}
