using TicTacToe.Common;

namespace Game.Interfaces
{
    public class Move : IMove
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool State { get; set; }
        public int MoveOrder { get; set; }

        public IPlayer PlayerId { get; set; }
    }
}