using Chess;
using NUnit.Framework;
using System;

namespace OOPChessProject.Tests
{
    [TestFixture]
    internal class TestQueen
    {
        [Test]
        public void QueenDeveloped_GetMovesForField_QueenCanMoveTo9Fields()
        {
            var s = "rnbqkb1r/pppp1ppp/7n/4p2Q/2B1P2N/2N5/PPPP1PPP/R1B1K2R";
            var cG = new ChessGame(s, "Eduard", "Felix");
            Console.WriteLine(cG.CurrentChessBoard);
            var fields = Controller.GetMovesForField(cG, HelperFunctions.StringToField("H5"));
            Assert.AreEqual(10, fields.Count);
        }
    }
}
