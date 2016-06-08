using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Common;

namespace Game.Interfaces
{
    public class State : IState
    {
        public const int X_MAX = 50;
        public const int Y_MAX = 50;

        public int MoveCount { get; set; }
        public int PlayersTurn { get; set; }
        public int Player1 { get; set; }
        public int Player2 { get; set; }
        public int? Winner { get; set; }
        public List<IMove> Moves { get; set; }
        public Dictionary<Tuple<int, int>, bool?> Board { get; set; }
        

        public State()
        {
            Moves = new List<IMove>();
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

        public void AddMove(IMove move)
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
                PlayersTurn = Player1;
            }
            
            // todo check state and setup winner
        }
        
    }
}