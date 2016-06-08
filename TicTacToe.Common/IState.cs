using System;
using System.Collections.Generic;

namespace TicTacToe.Common
{
    public interface IState
    {
        Dictionary<Tuple<int, int>, bool?> Board { get; set; }
        int MoveCount { get; set; }
        List<IMove> Moves { get; set; }
        int Player1 { get; set; }
        int Player2 { get; set; }
        int PlayersTurn { get; set; }
        int? Winner { get; set; }

        void AddMove(IMove move);
        Dictionary<Tuple<int, int>, bool?> Replay();
    }
}