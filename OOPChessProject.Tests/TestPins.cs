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
    class TestPins
    {
        [Test]
        public void PawnsPinned_GetMovesForField_CantExposeKingByMoving()
        {
            string s = "4k3/8/4r3/b7/5n2/4P3/3P1P2/4K3";
            ChessGame cG = new ChessGame(s, "Eduard", "Felix");
            Console.WriteLine(cG.CurrentChessBoard);
            var fields = Controller.GetMovesForField(cG, HelperFunctions.StringToField("D2"));
            Assert.AreEqual(0, fields.Count);
        }
        
        [Test]
        public void PawnsPinned_GetMovesForField_CantExposeKingByCapturing()
        {
            string s = "4k3/8/4r3/b7/5n2/4P3/3P1P2/4K3";
            ChessGame cG = new ChessGame(s, "Eduard", "Felix");
            Console.WriteLine(cG.CurrentChessBoard);
            var fields = Controller.GetMovesForField(cG, HelperFunctions.StringToField("E3"));
            Assert.AreEqual(1, fields.Count);
        }
    }
}
