using System;
using System.Collections.Generic;

namespace Game.Interfaces
{
    public interface IState
    {
        Dictionary<Tuple<int, int>, bool?> Board { get; set; }
        int MoveCount { get; set; }
        List<Move> Moves { get; set; }
        int Player1 { get; set; }
        int Player2 { get; set; }
        int PlayersTurn { get; set; }
        int? Winner { get; set; }

        void AddMove(Move move);
        Dictionary<Tuple<int, int>, bool?> Replay();
    }
}