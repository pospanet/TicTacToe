using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Common
{
    public class GameMove
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool State { get; set; }
        public int MoveOrder { get; set; }

        public int PlayerId { get; set; }
    }
}
