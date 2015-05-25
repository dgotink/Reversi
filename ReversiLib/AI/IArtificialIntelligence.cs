using Reversi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.AI
{
    public interface IArtificialIntelligence<NODE, MOVE>
        where NODE : IGameTreeNode<NODE, MOVE>
    {
        MOVE FindBestMove( NODE gameState );
    }
}
