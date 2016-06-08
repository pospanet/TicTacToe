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
                PlayersTurn = Player1;
            }

            // todo check state and setup winner
            if (IsWin(move))
            {
                Winner = move.PlayerId;
            }
        }

        private bool IsWin(Move lastMove)
        {
            int count = 0;

            // jdeme doprava
            int x = lastMove.X;
            int y = lastMove.Y;
            while (x < X_MAX && Board[new Tuple<int, int>(x++, y)] == lastMove.State && count < 5)
            {
                count++;
            }

            // jdeme doleva
            x = lastMove.X - 1;
            y = lastMove.Y;
            while (x >= 0 && Board[new Tuple<int, int>(x--, y)] == lastMove.State && count < 5)
            {
                count++;
            }

            if (count == 5) return true;

            count = 0;
            // jdeme dolů
            x = lastMove.X;
            y = lastMove.Y;
            while (y < Y_MAX && Board[new Tuple<int, int>(x, y++)] == lastMove.State && count < 5)
            {
                count++;
            }

            // jdeme nahoru
            x = lastMove.X;
            y = lastMove.Y - 1;
            while (y >= 0 && Board[new Tuple<int, int>(x, y--)] == lastMove.State && count < 5)
            {
                count++;
            }

            if (count == 5) return true;

            count = 0;
            // diagonála
            x = lastMove.X;
            y = lastMove.Y;
            while (x < X_MAX && y < Y_MAX && Board[new Tuple<int, int>(x++, y++)] == lastMove.State && count < 5)
            {
                count++;
            }

            x = lastMove.X - 1;
            y = lastMove.Y - 1;
            while (x >= 0 && y >= 0 && Board[new Tuple<int, int>(x--, y--)] == lastMove.State && count < 5)
            {
                count++;
            }

            if (count == 5) return true;

            count = 0;
            // diagonála 2
            x = lastMove.X;
            y = lastMove.Y;
            while (x >= 0 && y < Y_MAX && Board[new Tuple<int, int>(x--, y++)] == lastMove.State && count < 5)
            {
                count++;
            }

            x = lastMove.X + 1;
            y = lastMove.Y - 1;
            while (x < X_MAX && y >= 0 && Board[new Tuple<int, int>(x++, y--)] == lastMove.State && count < 5)
            {
                count++;
            }

            if (count == 5) return true;

            return false;
        }

    }
}