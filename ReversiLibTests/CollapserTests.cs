using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reversi.AI;
using System.Collections.Generic;
using Reversi.Domain;

namespace ReversiLibTests
{
    [TestClass]
    public class CollapserTests
    {
        [TestMethod]
        public void Test1()
        {
            TestCollapser( () =>
            {
                return AI();
            }, () =>
            {
                return AIN();
            } );
        }

        [TestMethod]
        public void Test2()
        {
            TestCollapser( () =>
            {
                var root = AI();
                root[0] = P();

                return root;
            }, () =>
            {
                var root = AIN();
                root[S( 0 )] = PN();

                return root;
            } );
        }

        [TestMethod]
        public void Test3()
        {
            TestCollapser( () =>
            {
                var root = AI();
                var sub = ( root[0] = AI() );
                sub[0] = P();

                return root;
            }, () =>
            {
                var root = AIN();
                root[S( 0, 0 )] = PN();

                return root;
            } );
        }

        [TestMethod]
        public void Test4()
        {
            TestCollapser( () =>
            {
                var root = AI();
                var sub = ( root[0] = AI() );
                sub[1] = P();

                return root;
            }, () =>
            {
                var root = AIN();
                root[S( 0, 1 )] = PN();

                return root;
            } );
        }

        [TestMethod]
        public void Test5()
        {
            TestCollapser( () =>
            {
                var root = AI();
                root[0] = P();
                root[1] = P();

                return root;
            }, () =>
            {
                var root = AIN();
                root[S( 0 )] = PN();
                root[S( 1 )] = PN();

                return root;
            } );
        }

        [TestMethod]
        public void Test6()
        {
            TestCollapser( () =>
            {
                var root = AI();
                root[0] = AI();
                root[1] = P();
                root[0][0] = P();

                return root;
            }, () =>
            {
                var root = AIN();
                root[S( 0, 0 )] = PN();
                root[S( 1 )] = PN();

                return root;
            } );
        }

        [TestMethod]
        public void Test7()
        {
            TestCollapser( () =>
            {
                var root = AI();
                root[0] = AI();
                root[1] = P();
                root[0][2] = P();

                return root;
            }, () =>
            {
                var root = AIN();
                root[S( 0, 2 )] = PN();
                root[S( 1 )] = PN();

                return root;
            } );
        }

        [TestMethod]
        public void Test8()
        {
            TestCollapser( () =>
            {
                var root = AI();
                root[0] = P();
                root[0][0] = AI();
                root[0][0][0] = P();

                return root;
            }, () =>
            {
                var root = AIN();
                root[S( 0 )] = PN();
                root[S( 0 )][S( 0 )] = AIN();
                root[S( 0 )][S( 0 )][S( 0 )] = PN();

                return root;
            } );
        }

        [TestMethod]
        public void Test9()
        {
            TestCollapser( () =>
            {
                var root = AI();
                root[0] = P();
                root[0][0] = AI();
                root[0][0][0] = AI();
                root[0][0][0][0] = P();

                return root;
            }, () =>
            {
                var root = AIN();
                root[S( 0 )] = PN();
                root[S( 0 )][S( 0 )] = AIN();
                root[S( 0 )][S( 0 )][S( 0, 0 )] = PN();

                return root;
            } );
        }

        private static void TestCollapser( Func<GameTreeNode<int>> originalFactory, Func<GameTreeNode<MoveSequence<int>>> expectedFactory )
        {
            GameTreeNode<int> original = originalFactory();
            GameTreeNode<MoveSequence<int>> expected = expectedFactory();
            var actual = new Collapser<GameTreeNode<int>, int>( original );

            AssertEqualTrees( expected, actual );
        }

        private static void AssertEqualTrees<NODE1, NODE2, MOVE>( IGameTreeNode<NODE1, MOVE> a, IGameTreeNode<NODE2, MOVE> b )
            where NODE1 : IGameTreeNode<NODE1, MOVE>
            where NODE2 : IGameTreeNode<NODE2, MOVE>
        {
            Assert.AreEqual( a.IsLeaf, b.IsLeaf, "Nodes should be both leaf or nonleaf" );

            if ( !a.IsLeaf )
            {
                var aMoves = new HashSet<MOVE>( a.ValidMoves );
                var bMoves = new HashSet<MOVE>( b.ValidMoves );

                Assert.IsTrue( aMoves.SetEquals( bMoves ), "Nodes should have same valid move set" );

                foreach ( var move in aMoves )
                {
                    AssertEqualTrees( a[move], b[move] );
                }
            }
        }

        private static GameTreeNode<int> AI()
        {
            return new GameTreeNode<int>( Player.TWO, 0 );
        }

        private static GameTreeNode<int> P()
        {
            return new GameTreeNode<int>( Player.ONE, 0 );
        }

        private static GameTreeNode<MoveSequence<int>> AIN()
        {
            return new GameTreeNode<MoveSequence<int>>( Player.TWO, 0 );
        }

        private static GameTreeNode<MoveSequence<int>> PN()
        {
            return new GameTreeNode<MoveSequence<int>>( Player.ONE, 0 );
        }

        private static MoveSequence<int> S( params int[] ns )
        {
            return MoveSequence<int>.FromMoves( ns );
        }
    }
}
