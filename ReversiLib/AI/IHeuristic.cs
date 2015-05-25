using Reversi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.AI
{
    public interface IHeuristic<NODE>
    {
        int Score( NODE node, Player player );
    }
}
