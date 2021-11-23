
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
            Field f = new Field(row._1, col.A);
            Assert.AreEqual(4, 5);
        }
    }
}
