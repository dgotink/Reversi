using Reversi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.AI
{
    public class RandomArtificialIntelligence<NODE, MOVE> : IArtificialIntelligence<NODE, MOVE>
        where NODE : IGameTreeNode<NODE, MOVE>
    {
        private readonly Random random;

        public RandomArtificialIntelligence()
        {
            this.random = new Random();
        }

        public MOVE FindBestMove( NODE gameState )
        {
            var moves = gameState.ValidMoves.ToList();
            var i = random.Next( moves.Count );

            return moves[i];
        }
    }

    public class DirectArtificialIntelligence<NODE, MOVE> : IArtificialIntelligence<NODE, MOVE>
        where NODE : IGameTreeNode<NODE, MOVE>
    {
        private readonly IHeuristic<NODE> heuristic;

        private readonly int ply;

        public DirectArtificialIntelligence( IHeuristic<NODE> heuristic, int ply )
        {
            this.heuristic = heuristic;
            this.ply = ply;
        }

        public MOVE FindBestMove( NODE gameState )
        {
            if ( gameState.IsLeaf )
            {
                throw new ArgumentException( "No moves possible" );
            }
            else
            {
                var bestMove = default( MOVE );
                var bestScore = int.MinValue;

                foreach ( var move in gameState.ValidMoves )
                {
                    var endState = FindBestEndState( gameState[move], ply );
                    var score = heuristic.Score( endState, gameState.CurrentPlayer );

                    if ( score > bestScore )
                    {
                        bestScore = score;
                        bestMove = move;
                    }
                }

                return bestMove;
            }
        }

        private NODE FindBestEndState( NODE gameState, int ply )
        {
            if ( gameState.IsLeaf || ply == 0 )
            {
                return gameState;
            }
            else
            {
                var bestChild = default( NODE );
                var bestScore = int.MinValue;

                foreach ( var move in gameState.ValidMoves )
                {
                    var child = gameState[move];
                    var score = heuristic.Score( child, gameState.CurrentPlayer );

                    if ( score > bestScore )
                    {
                        bestChild = child;
                        bestScore = score;
                    }
                }

                return FindBestEndState( bestChild, ply - 1 );
            }
        }
    }

    public class MinimaxArtificialIntelligence<NODE, MOVE> : IArtificialIntelligence<NODE, MOVE>
        where NODE : IGameTreeNode<NODE, MOVE>
    {
        private readonly IHeuristic<NODE> heuristic;

        private readonly int ply;

        public MinimaxArtificialIntelligence( IHeuristic<NODE> heuristic, int ply )
        {
            this.heuristic = heuristic;
            this.ply = ply;
        }

        public MOVE FindBestMove( NODE gameState )
        {
            if ( gameState.IsLeaf )
            {
                throw new ArgumentException( "No moves possible" );
            }
            else
            {
                var bestMove = default( MOVE );
                var bestScore = int.MinValue;

                foreach ( var move in gameState.ValidMoves )
                {
                    var score = ComputeHeuristic( gameState[move], gameState.CurrentPlayer, ply );

                    if ( score > bestScore )
                    {
                        bestScore = score;
                        bestMove = move;
                    }
                }

                return bestMove;
            }
        }

        private int ComputeHeuristic(NODE gameState, Player ai, int ply)
        {
            if ( gameState.IsLeaf || ply == 0 )
            {
                return heuristic.Score( gameState, ai );
            }
            else
            {
                if ( gameState.CurrentPlayer == ai )
                {
                    return gameState.ValidMoves.Select( move => ComputeHeuristic( gameState[move], ai, ply - 1 ) ).Max();
                }
                else
                {
                    return gameState.ValidMoves.Select( move => ComputeHeuristic( gameState[move], ai, ply - 1 ) ).Min();
                }
            }
        }
    }

    public class MinimaxWithAlphaBetaPruningArtificialIntelligence<NODE, MOVE> : IArtificialIntelligence<NODE, MOVE>
        where NODE : IGameTreeNode<NODE, MOVE>
    {
        private readonly IHeuristic<NODE> heuristic;

        private readonly int ply;

        public MinimaxWithAlphaBetaPruningArtificialIntelligence( IHeuristic<NODE> heuristic, int ply )
        {
            this.heuristic = heuristic;
            this.ply = ply;
        }

        public MOVE FindBestMove( NODE gameState )
        {
            if ( gameState.IsLeaf )
            {
                throw new ArgumentException( "No moves possible" );
            }
            else
            {
                var bestMove = default( MOVE );
                var bestScore = int.MinValue;

                foreach ( var move in gameState.ValidMoves )
                {
                    var score = ComputeHeuristic( gameState[move], gameState.CurrentPlayer, ply, int.MaxValue, int.MinValue );

                    if ( score > bestScore )
                    {
                        bestScore = score;
                        bestMove = move;
                    }
                }

                return bestMove;
            }
        }

        private int ComputeHeuristic( NODE gameState, Player ai, int ply, int alpha, int beta )
        {
            if ( gameState.IsLeaf || ply == 0 )
            {
                return heuristic.Score( gameState, ai );
            }
            else
            {
                if ( gameState.CurrentPlayer == ai )
                {
                    var maxScore = int.MinValue;

                    foreach ( var move in gameState.ValidMoves )
                    {
                        var score = ComputeHeuristic( gameState[move], ai, ply - 1, int.MaxValue, maxScore );

                        maxScore = Math.Max( maxScore, score );

                        if ( maxScore > alpha )
                        {
                            return maxScore;
                        }
                    }

                    return maxScore;

                    // return gameState.ValidMoves.Select( move => ComputeHeuristic( gameState[move], ai, ply - 1 ) ).Max();
                }
                else
                {
                    var minScore = int.MaxValue;

                    foreach ( var move in gameState.ValidMoves )
                    {
                        var score = ComputeHeuristic( gameState[move], ai, ply - 1, minScore, int.MinValue );

                        minScore = Math.Min( minScore, score );

                        if ( minScore < beta )
                        {
                            return minScore;
                        }
                    }

                    return minScore;

                    // return gameState.ValidMoves.Select( move => ComputeHeuristic( gameState[move], ai, ply - 1 ) ).Min();
                }
            }
        }
    }
}
