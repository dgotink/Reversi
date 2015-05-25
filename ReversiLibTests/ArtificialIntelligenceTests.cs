using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reversi.AI;
using Reversi.Domain;

namespace ReversiLibTests
{
    [TestClass]
    public class MinimaxArtificialIntelligenceTests
    {
        [TestMethod]
        public void Minimax1()
        {
            var root = AI( 0 );
            root[0] = P( 1 );

            var ai = CreateMinimax( 0 );

            Assert.AreEqual( 0, ai.FindBestMove( root ) );
        }

        [TestMethod]
        public void MinimaxAB1()
        {
            var root = AI( 0 );
            root[0] = P( 1 );

            var ai = CreateMinimaxAlphaBeta( 0 );

            Assert.AreEqual( 0, ai.FindBestMove( root ) );
        }

        [TestMethod]
        public void Minimax2()
        {
            var root = AI( 0 );
            root[0] = P( 1 );
            root[1] = P( 2 );
            root[2] = P( 3 );

            var ai = CreateMinimax( 0 );

            Assert.AreEqual( 2, ai.FindBestMove( root ) );
        }

        [TestMethod]
        public void MinimaxAB2()
        {
            var root = AI( 0 );
            root[0] = P( 1 );
            root[1] = P( 2 );
            root[2] = P( 3 );

            var ai = CreateMinimaxAlphaBeta( 0 );

            Assert.AreEqual( 2, ai.FindBestMove( root ) );
        }

        [TestMethod]
        public void Minimax3()
        {
            var root = AI( 0 );
            root[0] = P( 1 );
            root[1] = P( 4 );
            root[2] = P( 3 );

            var ai = CreateMinimax( 0 );

            Assert.AreEqual( 1, ai.FindBestMove( root ) );
        }

        [TestMethod]
        public void MinimaxAB3()
        {
            var root = AI( 0 );
            root[0] = P( 1 );
            root[1] = P( 4 );
            root[2] = P( 3 );

            var ai = CreateMinimaxAlphaBeta( 0 );

            Assert.AreEqual( 1, ai.FindBestMove( root ) );
        }

        [TestMethod]
        public void Minimax4()
        {
            var root = AI( 0 );
            root[0] = P( 1 );
            root[1] = P( 2 );
            root[2] = P( 3 );

            var ai = CreateMinimax( 0 );

            Assert.AreEqual( 2, ai.FindBestMove( root ) );
        }

        [TestMethod]
        public void MinimaxAB4()
        {
            var root = AI( 0 );
            root[0] = P( 1 );
            root[1] = P( 2 );
            root[2] = P( 3 );

            var ai = CreateMinimaxAlphaBeta( 0 );

            Assert.AreEqual( 2, ai.FindBestMove( root ) );
        }

        [TestMethod]
        public void Minimax5()
        {
            var root = AI( 0 );
            root[0] = P( 1 );
            root[0][0] = AI( 2 );
            root[1] = P( 2 );
            root[1][0] = AI( 3 );
            root[2] = P( 3 );
            root[2][0] = AI( 4 );

            var ai = CreateMinimax( 0 );

            Assert.AreEqual( 2, ai.FindBestMove( root ) );
        }

        [TestMethod]
        public void MinimaxAB5()
        {
            var root = AI( 0 );
            root[0] = P( 1 );
            root[0][0] = AI( 2 );
            root[1] = P( 2 );
            root[1][0] = AI( 3 );
            root[2] = P( 3 );
            root[2][0] = AI( 4 );

            var ai = CreateMinimaxAlphaBeta( 0 );

            Assert.AreEqual( 2, ai.FindBestMove( root ) );
        }

        [TestMethod]
        public void Minimax6()
        {
            var root = AI( 0 );
            root[0] = P( 1 );
            root[0][0] = AI( 2 );
            root[1] = P( 2 );
            root[1][0] = AI( 3 );
            root[2] = P( 3 );
            root[2][0] = AI( 4 );

            var ai = CreateMinimax( 1 );

            Assert.AreEqual( 2, ai.FindBestMove( root ) );
        }

        [TestMethod]
        public void MinimaxAB6()
        {
            var root = AI( 0 );
            root[0] = P( 1 );
            root[0][0] = AI( 2 );
            root[1] = P( 2 );
            root[1][0] = AI( 3 );
            root[2] = P( 3 );
            root[2][0] = AI( 4 );

            var ai = CreateMinimaxAlphaBeta( 1 );

            Assert.AreEqual( 2, ai.FindBestMove( root ) );
        }

        [TestMethod]
        public void Minimax7()
        {
            var root = AI( 0 );
            root[0] = P( 1 );
            root[0][0] = AI( 2 );
            root[1] = P( 2 );
            root[1][0] = AI( 5 );
            root[2] = P( 3 );
            root[2][0] = AI( 4 );

            var ai = CreateMinimax( 1 );

            Assert.AreEqual( 1, ai.FindBestMove( root ) );
        }

        [TestMethod]
        public void MinimaxAB7()
        {
            var root = AI( 0 );
            root[0] = P( 1 );
            root[0][0] = AI( 2 );
            root[1] = P( 2 );
            root[1][0] = AI( 5 );
            root[2] = P( 3 );
            root[2][0] = AI( 4 );

            var ai = CreateMinimaxAlphaBeta( 1 );

            Assert.AreEqual( 1, ai.FindBestMove( root ) );
        }

        [TestMethod]
        public void TestMinimaxAndMinimaxAB1()
        {
            var root = AI( 0 );
            root[0] = P( 0 );
            root[0][0] = AI( 3 );
            root[0][1] = AI( 5 );
            root[1] = P( 0 );
            root[1][0] = AI( 2 );
            root[1][1] = AI( 5 );

            AssertExpectedResult( root, 5, 0 );
        }

        [TestMethod]
        public void TestMinimaxAndMinimaxAB2()
        {
            var root = AI( 0 );
            root[0] = P( 0 );
            root[0][0] = AI( 3 );
            root[0][1] = AI( 5 );
            root[1] = P( 0 );
            root[1][0] = AI( 4 );
            root[1][1] = AI( 5 );

            AssertExpectedResult( root, 5, 1 );
        }

        [TestMethod]
        public void TestMinimaxAndMinimaxAB3()
        {
            var root = AI( 0 );
            root[0] = P( 0 );
            root[0][0] = AI( 3 );
            root[0][1] = AI( 5 );
            root[1] = P( 0 );
            root[1][0] = AI( 4 );
            root[1][1] = AI( 5 );
            root[1][2] = AI( 2 );
            root[1][3] = AI( 9 );

            AssertExpectedResult( root, 5, 0 );
        }

        [TestMethod]
        public void TestMinimaxAndMinimaxAB4()
        {
            var root = AI( 0 );
            root[0] = AI( 0 );
            root[0][0] = P( 0 );
            root[0][0][0] = AI( 5 );
            root[1] = P( 0 );
            root[1][0] = P( 3 );

            AssertExpectedResult( root, 5, 0 );
        }

        [TestMethod]
        public void TestMinimaxAndMinimaxAB5()
        {
            var root = AI( 0 );
            root[0] = AI( 0 );
            root[0][0] = P( 0 );
            root[0][0][0] = AI( 5 );
            root[1] = P( 0 );
            root[1][0] = P( 3 );
            root[1][1] = P( 9 );

            AssertExpectedResult( root, 5, 0 );
        }

        [TestMethod]
        public void TestMinimaxAndMinimaxAB6()
        {
            var root = AI( 0 );
            root[0] = AI( 0 );
            root[0][0] = P( 0 );
            root[0][0][0] = AI( 1 );
            root[1] = P( 0 );
            root[1][0] = P( 3 );
            root[1][1] = P( 9 );

            AssertExpectedResult( root, 5, 1 );
        }

        [TestMethod]
        public void TestMinimaxAndMinimaxAB7()
        {
            var root = AI( 0 );
            root[0] = P( 5 );
            root[1] = P( 0 );
            root[1][0] = AI( 7 );
            root[1][1] = AI( 9 );
            root[2] = P( 0 );
            root[2][0] = AI( 4 );

            AssertExpectedResult( root, 5, 1 );
        }

        [TestMethod]
        public void TestMinimaxAndMinimaxAB8()
        {
            var root = AI( 0 );
            root[0] = P( 9 );
            root[1] = P( 0 );
            root[1][0] = AI( 7 );
            root[1][1] = AI( 3 );
            root[2] = P( 0 );
            root[2][0] = AI( 4 );

            AssertExpectedResult( root, 5, 0 );
        }

        [TestMethod]
        public void TestMinimaxAndMinimaxAB9()
        {
            var root = AI( 0 );
            root[0] = P( 8 );
            root[1] = P( 0 );
            root[1][0] = AI( 7 );
            root[1][1] = AI( 3 );
            root[2] = P( 0 );
            root[2][0] = AI( 9 );

            AssertExpectedResult( root, 5, 2 );
        }

        [TestMethod]
        public void TestMinimaxAndMinimaxAB10()
        {
            var root = CreateBinaryTree( 1, 2, 3, 4, 5, 6, 7, 8 );

            AssertExpectedResult( root, 5, 1 );
        }

        [TestMethod]
        public void TestMinimaxAndMinimaxAB11()
        {
            var root = CreateBinaryTree( 5, 6, 7, 8, 1, 2, 3, 4 );

            AssertExpectedResult( root, 5, 0 );
        }

        [TestMethod]
        public void TestMinimaxAndMinimaxAB12()
        {
            var root = CreateBinaryTree( 5, 6, 1, 2, 3, 4, 7, 8 );

            AssertExpectedResult( root, 5, 1 );
        }

        private static GameTreeNode<int> CreateBinaryTree( params int[] data )
        {
            var root = AI( 0 );
            root[0] = P( 0 );
            root[1] = P( 0 );
            root[0][0] = AI( 0 );
            root[0][1] = AI( 0 );
            root[1][0] = AI( 0 );
            root[1][1] = AI( 0 );
            root[0][0][0] = P( data[0] );
            root[0][0][1] = P( data[1] );
            root[0][1][0] = P( data[2] );
            root[0][1][1] = P( data[3] );
            root[1][0][0] = P( data[4] );
            root[1][0][1] = P( data[5] );
            root[1][1][0] = P( data[6] );
            root[1][1][1] = P( data[7] );

            return root;
        }

        [TestMethod]
        [Timeout( 5000 )]
        public void MinimaxABSpeed()
        {
            var root = AI( 0 );
            root[0] = P( 0 );
            root[0][0] = AI( 5 );
            root[1] = P( 1 );
            root[1][0] = AI( 1 );

            var child = CreateRandomTree( new Random( 7878 ), 10 );
            for ( var i = 1; i != 100; ++i )
            {
                root[1][i] = child;
            }

            var ai = CreateMinimaxAlphaBeta( 100 );

            Assert.AreEqual( 0, ai.FindBestMove( root ) );
        }

        private void AssertExpectedResult( GameTreeNode<int> root, int ply, int expected )
        {
            var ai1 = CreateMinimax( ply );
            var ai2 = CreateMinimaxAlphaBeta( ply );

            var m1 = ai1.FindBestMove( root );
            var m2 = ai2.FindBestMove( root );

            Assert.AreEqual( expected, m1 );
            Assert.AreEqual( expected, m2 );
        }

        [TestMethod]
        public void RandomMinimaxAndMinimaxABComparison1()
        {
            var random = new Random( 7214689 );

            for ( var i = 0; i != 50; ++i )
            {
                var root = CreateRandomTree( random, 1 );
                var ply = random.Next( 4 );
                var minimax = CreateMinimax( ply );
                var minimaxAB = CreateMinimaxAlphaBeta( ply );

                var minimaxMove = minimax.FindBestMove( root );
                var minimaxABMove = minimaxAB.FindBestMove( root );

                Assert.AreEqual( minimaxMove, minimaxABMove );
            }
        }

        [TestMethod]
        public void RandomMinimaxAndMinimaxABComparison2()
        {
            var random = new Random( 14154 );

            for ( var i = 0; i != 50; ++i )
            {
                var root = CreateRandomTree( random, 2 );
                var ply = random.Next( 4 );
                var minimax = CreateMinimax( ply );
                var minimaxAB = CreateMinimaxAlphaBeta( ply );

                var minimaxMove = minimax.FindBestMove( root );
                var minimaxABMove = minimaxAB.FindBestMove( root );

                Assert.AreEqual( minimaxMove, minimaxABMove );
            }
        }

        [TestMethod]
        public void RandomMinimaxAndMinimaxABComparison3()
        {
            var random = new Random( 7875 );

            for ( var i = 0; i != 100; ++i )
            {
                var root = CreateRandomTree( random, random.Next( 5 ) + 1 );
                var ply = random.Next( 4 );
                var minimax = CreateMinimax( ply );
                var minimaxAB = CreateMinimaxAlphaBeta( ply );

                var minimaxMove = minimax.FindBestMove( root );
                var minimaxABMove = minimaxAB.FindBestMove( root );

                Assert.AreEqual( minimaxMove, minimaxABMove );
            }
        }

        private static GameTreeNode<int> CreateRandomTree( Random random )
        {
            var depth = random.Next( 5 );

            return CreateRandomTree( random, depth );
        }

        private static GameTreeNode<int> CreateRandomTree( Random random, int depth )
        {
            var root = CreateRandomNode( random );

            if ( depth > 0 )
            {
                var childCount = random.Next( 8 ) + 1;

                for ( var i = 0; i != childCount; ++i )
                {
                    var child = CreateRandomTree( random, depth - 1 );

                    root.AddChild( i, child );
                }
            }

            return root;
        }

        private static GameTreeNode<int> CreateRandomStructuredTree( Random random, int depth, bool ai )
        {
            var rootHeuristic = random.Next( 100 );
            var root = ai ? AI( rootHeuristic ) : P( rootHeuristic );

            if ( depth > 0 )
            {
                var childCount = random.Next( 8 ) + 1;

                for ( var i = 0; i != childCount; ++i )
                {
                    var child = CreateRandomStructuredTree( random, depth - 1, !ai );

                    root.AddChild( i, child );
                }
            }

            return root;
        }

        private static GameTreeNode<int> CreateRandomNode( Random random )
        {
            var heads = random.Next( 2 ) == 0;
            var heuristic = random.Next( 100 );

            if ( heads )
            {
                return AI( heuristic );
            }
            else
            {
                return P( heuristic );
            }
        }

        private static GameTreeNode<int> AI( int heuristic )
        {
            return new GameTreeNode<int>( Player.TWO, heuristic );
        }

        private static GameTreeNode<int> P( int heuristic )
        {
            return new GameTreeNode<int>( Player.ONE, heuristic );
        }

        private static IHeuristic<GameTreeNode<int>> CreateHeuristic()
        {
            return new Heuristic<int>();
        }

        private static IArtificialIntelligence<GameTreeNode<int>, int> CreateMinimax( int ply )
        {
            var heuristic = CreateHeuristic();

            return new MinimaxArtificialIntelligence<GameTreeNode<int>, int>( heuristic, ply );
        }

        private static IArtificialIntelligence<GameTreeNode<int>, int> CreateMinimaxAlphaBeta( int ply )
        {
            var heuristic = CreateHeuristic();

            return new MinimaxWithAlphaBetaPruningArtificialIntelligence<GameTreeNode<int>, int>( heuristic, ply );
        }
    }
}
