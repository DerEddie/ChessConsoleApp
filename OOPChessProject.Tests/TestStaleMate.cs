using Chess;
using NUnit.Framework;
using System;

namespace OOPChessProject.Tests
{
    [TestFixture]
    class TestStaleMate
    {
        [Test]
        public void KingSurrounded_GetMovesForField_CantMoveStaleMate()
        {
            string s = "4k3/8/8/8/3r1r2/4p3/4P3/4K3";
            ChessGame cG = new ChessGame(s, "Eduard", "Felix");
            Console.WriteLine(cG.CurrentChessBoard);
            var fields = Controller.GetMovesForField(cG, HelperFunctions.StringToField("E1"));
            Console.WriteLine(fields);
            Assert.AreEqual(0, fields.Count);
        }

    }
}
