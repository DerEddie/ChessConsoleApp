
using NUnit.Framework;
using System;

namespace OOPChessProject.Tests
{
    //decorator = special attribute
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            ChessGame ChessGame = new ChessGame("Eduard", "Lukas");
            string board = ChessGame.currentChessBoard.ToString();
            Console.Write(board.Length);
            Assert.AreEqual(board.Length, 494);
        }

        [Test]
        public void TestMethod2()
        {
            Field f = new Field(2, 3);
            Assert.AreEqual(4, 5);
        }

        [Test]
        public void TestBoardCopy()
        {
            ChessBoard chessBoard = new ChessBoard();
            ChessBoard copyBoard = new ChessBoard(chessBoard);
            copyBoard.MovePiece(new Field(1,4), new Field(3, 4));

            Console.WriteLine(chessBoard);
            Console.WriteLine(copyBoard);

            Assert.AreNotEqual(copyBoard.ToString(),chessBoard.ToString());
        }
    }
}
