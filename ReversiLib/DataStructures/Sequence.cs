using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.DataStructures
{
    /// <summary>
    /// A ISequence is similar to an array, but it differs
    /// that the range of valid indices is not necessarily 0 to Length-1.
    /// Instead, the valid range is [Start, End) (not the End itself is not a valid index).
    /// </summary>
    /// <typeparam name="T">Type of the elements.</typeparam>
    public interface ISequence<out T>
    {
        /// <summary>
        /// Retrieves the element at the given index.
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>Element at given index</returns>
        T this[int index] { get; }

        /// <summary>
        /// Lowest valid index
        /// </summary>
        int Start { get; }

        /// <summary>
        /// This is the index of the element one beyond the bounds of the array.
        /// Indexing with End will generate an exception.
        /// </summary>
        int End { get; }

        /// <summary>
        /// Number of items in the sequence.
        /// </summary>
        int Length { get; }
    }

    /// <summary>
    /// Helper methods.
    /// </summary>
    public static class Sequence
    {
        public static Sequence<char> FromString( string str )
        {
            return new Sequence<char>( str.ToCharArray() );
        }
    }

    /// <summary>
    /// Extension methods on ISequence. These methods are available
    /// on objects of type ISequence. For example, you can write <code>seq.IsValidIndex(4)</code>.
    /// </summary>
    public static class ISequenceExtensions
    {
        /// <summary>
        /// Checks if the given index is valid.
        /// </summary>
        /// <typeparam name="T">Type of the elements in the sequence</typeparam>
        /// <param name="xs">Sequence</param>
        /// <param name="index">Index to be checked</param>
        /// <returns>True if the index is valid, false otherwise.</returns>
        public static bool IsValidIndex<T>( this ISequence<T> xs, int index )
        {
            return xs.Start <= index && index < xs.End;
        }

        /// <summary>
        /// Checks whether the sequence is empty.
        /// </summary>
        /// <typeparam name="T">Type of the elements in the sequence</typeparam>
        /// <param name="xs">Sequence.</param>
        /// <returns>True if the sequence is empty, false otherwise.</returns>
        public static bool IsEmpty<T>( this ISequence<T> xs )
        {
            return xs.Length == 0;
        }

        /// <summary>
        /// Returns an <code>IEnumerable</code> of valid indices.
        /// </summary>
        /// <typeparam name="T">Type of elements in the sequence.</typeparam>
        /// <param name="xs">Sequence.</param>
        /// <returns>All valid indices, in increasing order.</returns>
        public static IEnumerable<int> Indices<T>( this ISequence<T> xs )
        {
            return Enumerable.Range( xs.Start, xs.Length );
        }

        /// <summary>
        /// Converts the sequence to an <code>IEnumerable</code>.
        /// This makes it possible to use a foreach loop on a sequence.
        /// </summary>
        /// <typeparam name="T">Type of elements in the sequence.</typeparam>
        /// <param name="xs">Sequence.</param>
        /// <returns>An <code>IEnumerable</code> which enumerates the elements of this sequence in the same order.</returns>
        public static IEnumerable<T> ToEnumerable<T>( this ISequence<T> xs )
        {
            return xs.Indices().Select( i => xs[i] );
        }

        /// <summary>
        /// Given a sequence of Ts this method builds a new sequence which
        /// is the result of sequentially applying the given <paramref name="func" /> to each T
        /// </summary>
        /// <example>
        /// This example shows how to square each number in a sequence.
        /// <code>
        /// var ns = new Sequence&lt;int&gt;(1, 2, 3, 4);
        /// var squares = ns.Map( n => n * n );
        /// </code>
        /// </example>
        /// <typeparam name="T">Types of the items in the original sequence.</typeparam>
        /// <typeparam name="R">Types of the items in the generated sequence.</typeparam>
        /// <param name="xs">Original sequence.</param>
        /// <param name="func">Function to apply on each item of the original sequence.</param>
        /// <returns>A sequence consisting of the results of applying <paramref name="func" />
        /// to each of the elements in the original sequence <paramref name="xs"/>.</returns>
        public static ISequence<R> Map<T, R>( this ISequence<T> xs, Func<T, R> func )
        {
            return new Sequence<R>( xs.Start, xs.Length, i => func( xs[i] ) );
        }

        /// <summary>
        /// This method is similar to <see cref="Map"/>: it applies a function <paramref name="func"/>
        /// to each item in a given sequence. However, instead of storing the results in a new sequence,
        /// it generates a "virtual sequence" that keeps a reference to the original sequence and
        /// upon indexing, will fetch the corresponding item from the original sequence
        /// and return the result of applying <paramref name="func" /> upon it.
        /// The main difference is that sequences produced by this method remain synchronized
        /// with the original array.
        /// </summary>
        /// <example>
        /// <code>
        /// // Create a sequence of mutable items [0,1,2,3]
        /// var seq = new Sequence&lt;IVar&lt;int&gt;&gt;( 0, 4, i =&gt; new Var(i) );
        /// 
        /// // Create a virtual mapping [0,1,4,9]
        /// var squares = seq.VirtualMap( n =&gt; n.Value * n.Value );
        /// 
        /// // Modify the original array
        /// seq[0].Value = 10;
        /// 
        /// // squares now contains [100,1,4,9]
        /// </code>
        /// </example>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="xs"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static ISequence<R> VirtualMap<T, R>( this ISequence<T> xs, Func<T, R> func )
        {
            return new VirtualSequence<R>( xs.Start, xs.Length, i => func( xs[i] ) );
        }

        /// <summary>
        /// Takes a slice from a sequence. The slice remains synchronized with the original array, i.e.
        /// changes in the original array are visible in the slice.
        /// </summary>
        /// <typeparam name="T">Type of the items in the sequence.</typeparam>
        /// <param name="xs">Sequence.</param>
        /// <param name="start">Starting index.</param>
        /// <param name="length">Length of the slice.</param>
        /// <returns>A slice of the original sequence.</returns>
        public static ISequence<T> VirtualSlice<T>( this ISequence<T> xs, int start, int length )
        {
            if ( length < 0 )
            {
                throw new ArgumentOutOfRangeException( "length" );
            }
            else if ( start + length >= xs.Length )
            {
                throw new ArgumentException( "length" );
            }
            else
            {
                return new VirtualSequence<T>( length, i => xs[start + i] );
            }
        }

        /// <summary>
        /// Slice from <paramref name="start"/> till end. The slice remains synchronized with the original sequence.
        /// </summary>
        /// <typeparam name="T">Type of the items in the sequence.</typeparam>
        /// <param name="xs">Original sequence.</param>
        /// <param name="start">Starting index.</param>
        /// <returns>Slice.</returns>
        public static ISequence<T> VirtualSlice<T>( this ISequence<T> xs, int start )
        {
            return xs.VirtualSlice( start, xs.Length - start );
        }

        /// <summary>
        /// Finds the index of the first element satisfying <paramref name="predicate"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// var seq = new Sequence&lt;int&gt;(1,2,3,4);
        /// 
        /// // Find index of first item divisible by 3
        /// var i = seq.FindIndex( n => n % 3 == 0 );
        /// </code>
        /// </example>
        /// <typeparam name="T">Type of the items in the sequence.</typeparam>
        /// <param name="xs">Sequence.</param>
        /// <param name="predicate">Predicate.</param>
        /// <returns>Index of first item satisfying <paramref name="predicate"/> or <code>null</code> if no such item exists.</returns>
        public static int? FindIndex<T>( this ISequence<T> xs, Func<T, bool> predicate )
        {
            foreach ( var i in xs.Indices() )
            {
                var item = xs[i];

                if ( predicate( item ) )
                {
                    return i;
                }
            }

            return null;
        }

        /// <summary>
        /// Finds the index of the first item equal to <paramref name="x"/>.
        /// </summary>
        /// <typeparam name="T">Type of the items in the sequence.</typeparam>
        /// <param name="xs">Sequence.</param>
        /// <param name="x">Item to look for.</param>
        /// <returns>Index of first item equal to <paramref name="x"/> if no such item exists.</returns>
        /// <returns></returns>
        public static int? FindIndex<T>( this ISequence<T> xs, T x )
        {
            return xs.FindIndex( y => x == null ? y == null : x.Equals( y ) );
        }

        /// <summary>
        /// Checks whether the sequences containing equal items in equal positions.
        /// </summary>
        /// <typeparam name="T">Type of the items.</typeparam>
        /// <param name="xs">First sequence.</param>
        /// <param name="ys">Second sequence.</param>
        /// <returns>True if the sequences contain the same items in the same places, false otherwise.</returns>
        public static bool EqualItems<T>( this ISequence<T> xs, ISequence<T> ys )
        {
            if ( xs == null || ys == null )
            {
                throw new ArgumentNullException();
            }
            else
            {
                if ( xs.Start != ys.Start || xs.End != ys.End )
                {
                    return false;
                }
                else
                {
                    foreach ( var index in xs.Indices() )
                    {
                        if ( !xs[index].Equals( ys[index] ) )
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }
        }

        public static int ComputeHashCode<T>( this ISequence<T> xs )
        {
            return xs.ToEnumerable().Select( x => x.GetHashCode() ).Aggregate( 0, ( x, y ) => x ^ y );
        }

        /// <summary>
        /// Given a sequence of strings, this method concatenates these strings into one
        /// single string in which all sequence strings appear in the same order
        /// as in the sequence, separated by <paramref name="sperator"/>.
        /// </summary>
        /// <param name="xs">Sequence.</param>
        /// <param name="separator">Separator.</param>
        /// <returns>Concatenation of strings using <paramref name="separator"/> as separator.</returns>
        public static string Join( this ISequence<string> xs, string separator )
        {
            return String.Join( separator, xs.ToEnumerable() );
        }

        /// <summary>
        /// Iterates over each item the sequence and applies <paramref name="action"/> on each of them, in order.
        /// </summary>
        /// <typeparam name="T">Type of the items in the sequence.</typeparam>
        /// <param name="xs">Sequence.</param>
        /// <param name="action">Action to be performed on each item.</param>
        public static void Each<T>( this ISequence<T> xs, Action<T> action )
        {
            foreach ( var x in xs.ToEnumerable() )
            {
                action( x );
            }
        }
    }

    public abstract class SequenceBase<T> : ISequence<T>
    {
        public abstract int Start { get; }

        public abstract int End { get; }

        public abstract int Length { get; }

        public abstract T this[int index] { get; }

        public override bool Equals( object obj )
        {
            return Equals( obj as ISequence<T> );
        }

        public bool Equals( ISequence<T> xs )
        {
            if ( xs == null )
            {
                return false;
            }
            else
            {
                return this.EqualItems( xs );
            }
        }

        public override int GetHashCode()
        {
            return this.ComputeHashCode();
        }

        public override string ToString()
        {
            return string.Format( "[{0}]", this.VirtualMap( x => x.ToString() ).Join( ", " ) );
        }
    }

    public class Sequence<T> : SequenceBase<T>
    {
        private readonly int startIndex;

        private readonly T[] items;

        public Sequence( int startIndex, int length, Func<int, T> initializer )
        {
            if ( length < 0 )
            {
                throw new ArgumentOutOfRangeException( "length", "Must be positive" );
            }
            else
            {
                this.startIndex = startIndex;
                items = ( from i in Enumerable.Range( 0, length )
                          select initializer( i ) ).ToArray();
            }
        }

        public Sequence( int startIndex, int length, T initialValue = default(T) )
            : this( startIndex, length, i => initialValue )
        {
            // NOP
        }

        public Sequence( int length, Func<int, T> initializer )
            : this( 0, length, initializer )
        {
            // NOP
        }

        public Sequence( int length, T initialValue = default(T) )
            : this( length, i => initialValue )
        {
            // NOP
        }

        public Sequence( params T[] xs )
            : this( xs.Length, i => xs[i] )
        {
            // NOP
        }

        public override T this[int index]
        {
            get
            {
                return items[index - startIndex];
            }
        }

        public override int Start
        {
            get
            {
                return startIndex;
            }
        }

        public override int End
        {
            get
            {
                return startIndex + Length;
            }
        }

        public override int Length
        {
            get
            {
                return items.Length;
            }
        }
    }

    public class VirtualSequence<T> : SequenceBase<T>
    {
        private readonly int startIndex;

        private readonly int length;

        private readonly Func<int, T> fetcher;

        public VirtualSequence( int startIndex, int length, Func<int, T> fetcher )
        {
            this.startIndex = startIndex;
            this.length = length;
            this.fetcher = fetcher;
        }

        public VirtualSequence( int length, Func<int, T> fetcher )
            : this( 0, length, fetcher )
        {
            // NOP
        }

        public override int Length
        {
            get
            {
                return length;
            }
        }

        public override int Start
        {
            get
            {
                return startIndex;
            }
        }

        public override int End
        {
            get
            {
                return startIndex + length;
            }
        }

        public override T this[int index]
        {
            get
            {
                if ( !this.IsValidIndex( index ) )
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                {
                    return fetcher( index );
                }
            }
        }
    }
}
