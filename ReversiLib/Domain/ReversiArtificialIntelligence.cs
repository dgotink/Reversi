using Reversi.AI;
using Reversi.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Domain
{
    public class ReversiArtificialIntelligence
    {
        public static ReversiArtificialIntelligence CreateRandom()
        {
            var ai = new RandomArtificialIntelligence<ReversiGameTree, Vector2D>();

            return new ReversiArtificialIntelligence( ai );
        }

        public static ReversiArtificialIntelligence CreateDirect( IReversiHeuristic heuristic, int ply )
        {
            var adapter = new ReversiHeuristicAdapter( heuristic );
            var ai = new DirectArtificialIntelligence<ReversiGameTree, Vector2D>( adapter, ply );

            return new ReversiArtificialIntelligence( ai );
        }

        public static ReversiArtificialIntelligence CreateMinimax( IReversiHeuristic heuristic, int ply )
        {
            var adapter = new ReversiHeuristicAdapter( heuristic );
            var ai = new MinimaxArtificialIntelligence<ReversiGameTree, Vector2D>( adapter, ply );

            return new ReversiArtificialIntelligence( ai );
        }

        public static ReversiArtificialIntelligence CreateMinimaxWithAlphaBetaPruning( IReversiHeuristic heuristic, int ply )
        {
            var adapter = new ReversiHeuristicAdapter( heuristic );
            var ai = new MinimaxWithAlphaBetaPruningArtificialIntelligence<ReversiGameTree, Vector2D>( adapter, ply );

            return new ReversiArtificialIntelligence( ai );
        }

        private readonly IArtificialIntelligence<ReversiGameTree, Vector2D> ai;

        internal ReversiArtificialIntelligence( IArtificialIntelligence<ReversiGameTree, Vector2D> ai )
        {
            this.ai = ai;
        }

        public Vector2D FindBestMove( Game game )
        {
            var node = ReversiGameTree.Create( game );

            return ai.FindBestMove( node );
        }
    }

    internal class ReversiHeuristicAdapter : IHeuristic<ReversiGameTree>
    {
        private readonly IReversiHeuristic heuristic;

        public ReversiHeuristicAdapter( IReversiHeuristic heuristic )
        {
            this.heuristic = heuristic;
        }

        public int Score( ReversiGameTree node, Player player )
        {
            return heuristic.Score( node.Board, player );
        }
    }
}
