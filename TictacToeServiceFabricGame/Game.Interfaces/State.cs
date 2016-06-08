using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Interfaces
{
    public class State
    {
        public const int X_MAX = 50;
        public const int Y_MAX = 50;

        public int MoveCount { get; set; }
        public int PlayersTurn { get; set; }
        public int Player1 { get; set; }
        public int Player2 { get; set; }
        public int? Winner { get; set; }
        public List<Move> Moves { get; set; }
        public Dictionary<Tuple<int, int>, bool?> Board { get; set; }

        public State()
        {
            Moves = new List<Move>();
            Board = new Dictionary<Tuple<int, int>, bool?>();
            for (int x = 0; x < X_MAX; x++)
            {
                for (int y = 0; y < Y_MAX; y++)
                {
                    Board.Add(new Tuple<int, int>(x, y), null);
                }
            }
        }
        public Dictionary<Tuple<int, int>, bool?> Replay()
        {
            var board = new Dictionary<Tuple<int, int>, bool?>();
            foreach (var move in Moves.OrderBy(x=>x.MoveOrder))
            {
                board[new Tuple<int, int>(move.X, move.Y)] = move.State;
            }
            return board;
        }

        public void AddMove(Move move)
        {
            // is player's turn?
            if(move.PlayerId != PlayersTurn)
            {
                throw new ApplicationException("Not your turn");
            }
            if(Board[new Tuple<int, int>(move.X, move.Y)] != null || Winner != null)
            {
                throw new ApplicationException("Invalid move");
            }

            Board[new Tuple<int, int>(move.X, move.Y)] = move.State;
            // switch players
            if (PlayersTurn == Player1)
            {
                PlayersTurn = Player2;
            }
            else
            {
                PlayersTurn = Player2;
            }

            PlayersTurn = move.PlayerId;
            // todo check state and setup winner
        }
        
    }
}