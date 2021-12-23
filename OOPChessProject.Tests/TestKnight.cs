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
    class TestKnight
    {
        [Test]
        public void KnightDeveloped_GetMovesForField_KnightCanMoveTo3Fields()
        {
            string s = "rnbqkb1r/pppp1ppp/7n/4p2Q/2B1P2N/2N5/PPPP1PPP/R1B1K2R";
            ChessGame cG = new ChessGame(s, "Eduard", "Felix");
            Console.WriteLine(cG.CurrentChessBoard);
            var fields = Controller.GetMovesForField(cG, HelperFunctions.StringToField("H4"));
            Assert.AreEqual(3, fields.Count);
        }
    }
}
