using Chess;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPChessProject.Tests
{
    [TestFixture]
    class TestBishop
    {
        [Test]
        public void BishopDeveloped_GetMovesForField_BishopCanMoveTo9Fields()
        {
            string s = "rnbqkbnr/pppp1ppp/8/4p2Q/2B1P3/8/PPPP1PPP/RNB1K1NR";
            ChessGame cG = new ChessGame(s, "Eduard", "Felix");
            Console.WriteLine(cG.CurrentChessBoard);
            var fields = Controller.GetMovesForField(cG, HelperFunctions.StringToField("C4"));
            Assert.AreEqual(9, fields.Count);
        }
    }
}
