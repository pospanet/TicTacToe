using System;
using System.Collections.Generic;

namespace TicTacToe.Common
{
    public interface IState
    {
        Dictionary<Tuple<int, int>, bool?> Board { get; }
        IPlayer Player1 { get; }
        IPlayer Player2 { get; }
        IPlayer PlayersTurn { get; }
        IPlayer Winner { get; }
    }
}