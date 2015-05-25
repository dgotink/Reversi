using Reversi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.AI
{
    public interface IGameTreeNode<out NODE, MOVE> where NODE : IGameTreeNode<NODE, MOVE>
    {
        IEnumerable<MOVE> ValidMoves { get; }

        NODE this[MOVE move] { get; }

        Player CurrentPlayer { get; }

        bool IsLeaf { get; }
    }
}
