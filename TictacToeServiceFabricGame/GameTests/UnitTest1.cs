using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game.Interfaces;

namespace GameTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAddMove()
        {
            var game = new State() { PlayersTurn = 1, Player1 = 1, Player2 = 2 };
            var move = new Move() { X= 20, Y = 20, State = true, MoveOrder = 0, PlayerId = 1};
            game.AddMove(move);
            Assert.AreEqual(true, game.Board[new Tuple<int, int>(move.X, move.Y)].Value);
            Assert.AreEqual(false, game.Board[new Tuple<int, int>(0, 0)].HasValue);           
        }
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAddMoveTwice()
        {
            var game = new State() { PlayersTurn = 1, Player1 = 1, Player2 = 2 };
            var move = new Move() { X = 20, Y = 20, State = true, MoveOrder = 0, PlayerId = 1 };
            game.AddMove(move);
            var move2 = new Move() { X = 20, Y = 20, State = true, MoveOrder = 1, PlayerId = 2 };
            game.AddMove(move2);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestMyTurn()
        {
            var game = new State() { PlayersTurn = 1, Player1 = 1, Player2 = 2 };
            var move = new Move() { X = 20, Y = 20, State = true, MoveOrder = 0, PlayerId = 1 };
            game.AddMove(move);
            var move2 = new Move() { X = 20, Y = 20, State = true, MoveOrder = 1, PlayerId = 1 };
            game.AddMove(move);
        }
    }    
 }
