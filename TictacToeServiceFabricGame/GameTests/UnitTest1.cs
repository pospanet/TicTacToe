using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game.Interfaces;
using TicTacToe.Common;

namespace GameTests
{
    public class Player : IPlayer
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
    }
    [TestClass]
    public class UnitTest1
    {
        IPlayer p1 = new Player() { Id = Guid.NewGuid(), DisplayName = "1" };
        IPlayer p2 = new Player() { Id = Guid.NewGuid(), DisplayName = "1" };

        [TestMethod]
        public void TestAddMove()
        {
            var game = new State() { PlayersTurn = p1, Player1 = p1, Player2 = p2 };
            var move = new Move() { X = 20, Y = 20, State = true, MoveOrder = 0, PlayerId = p1 };
            game.AddMove(move);
            Assert.AreEqual(true, game.Board[new Tuple<int, int>(move.X, move.Y)].Value);
            Assert.AreEqual(false, game.Board[new Tuple<int, int>(0, 0)].HasValue);
        }
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAddMoveTwice()
        {
            var game = new State() { PlayersTurn = p1, Player1 = p1, Player2 = p2 };
            var move = new Move() { X = 20, Y = 20, State = true, MoveOrder = 0, PlayerId = p1 };
            game.AddMove(move);
            var move2 = new Move() { X = 20, Y = 20, State = true, MoveOrder = 1, PlayerId = p2 };
            game.AddMove(move2);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestMyTurn()
        {
            var game = new State() { PlayersTurn = p1, Player1 = p1, Player2 = p2 };
            var move = new Move() { X = 20, Y = 20, State = true, MoveOrder = 0, PlayerId = p1 };
            game.AddMove(move);
            var move2 = new Move() { X = 20, Y = 20, State = true, MoveOrder = 1, PlayerId = p1 };
            game.AddMove(move);
        }

        [TestMethod]
        public void TestWinner_Horizontal()
        {
            var game = new State()
            {
                PlayersTurn = p1,
                Player1 = p1,
                Player2 = p2
            };

            game.AddMove(new Move() { X = 1, Y = 1, State = true, MoveOrder = 0, PlayerId = p1 });
            game.AddMove(new Move() { X = 20, Y = 20, State = false, MoveOrder = 1, PlayerId = p2 });
            game.AddMove(new Move() { X = 1, Y = 2, State = true, MoveOrder = 2, PlayerId = p1 });
            game.AddMove(new Move() { X = 21, Y = 20, State = false, MoveOrder = 3, PlayerId = p2 });
            game.AddMove(new Move() { X = 1, Y = 3, State = true, MoveOrder = 4, PlayerId = p1 });
            game.AddMove(new Move() { X = 22, Y = 20, State = false, MoveOrder = 5, PlayerId = p2 });
            game.AddMove(new Move() { X = 1, Y = 4, State = true, MoveOrder = 6, PlayerId = p1 });
            game.AddMove(new Move() { X = 23, Y = 20, State = false, MoveOrder = 7, PlayerId = p2 });
            game.AddMove(new Move() { X = 1, Y = 5, State = true, MoveOrder = 8, PlayerId = p1 });

            Assert.AreEqual(1, game.Winner);
        }

        [TestMethod]
        public void TestWinner_Diagonal()
        {
            var game = new State()
            {
                PlayersTurn = p1,
                Player1 = p1,
                Player2 = p2
            };

            game.AddMove(new Move() { X = 1, Y = 1, State = true, MoveOrder = 0, PlayerId = p1 });
            game.AddMove(new Move() { X = 20, Y = 20, State = false, MoveOrder = 1, PlayerId = p2 });
            game.AddMove(new Move() { X = 2, Y = 2, State = true, MoveOrder = 2, PlayerId = p1 });
            game.AddMove(new Move() { X = 21, Y = 20, State = false, MoveOrder = 3, PlayerId = p2 });
            game.AddMove(new Move() { X = 3, Y = 3, State = true, MoveOrder = 4, PlayerId = p1 });
            game.AddMove(new Move() { X = 22, Y = 20, State = false, MoveOrder = 5, PlayerId = p2 });
            game.AddMove(new Move() { X = 4, Y = 4, State = true, MoveOrder = 6, PlayerId = p1 });
            game.AddMove(new Move() { X = 23, Y = 20, State = false, MoveOrder = 7, PlayerId = p2 });
            game.AddMove(new Move() { X = 5, Y = 5, State = true, MoveOrder = 8, PlayerId = p1 });

            Assert.AreEqual(1, game.Winner);
        }
    }    
 }
