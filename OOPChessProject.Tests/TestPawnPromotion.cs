using System;
using Chess;
using NUnit.Framework;

namespace OOPChessProject.Tests
{
    [TestFixture]
    public class TestPawnPromotion
    {
        [Test]
        public void PawnInFrontOfFinishLine_GetPossibleMoves_1Move()
        {
            var s = "r1bqk2r/pP1pp1Pp/2n5/8/8/8/P1p1P1pP/R3K2R";
            var cG = new ChessGame(s, "Eduard", "Felix");
            Console.WriteLine(cG.CurrentChessBoard);
            var fields = Controller.GetMovesForField(cG, HelperFunctions.StringToField("C2"));
            Assert.AreEqual(1, fields.Count);
        }

        [Test]
        public void PawnInFrontOfKing__UpdateGamestate_NoCheck()
        {
            var s = "r1bqk2r/pP1p2Pp/2n5/8/8/8/P1p1p1pP/R3K2R";
            var cG = new ChessGame(s, "Eduard", "Felix");
            Console.WriteLine(cG.CurrentChessBoard);
            Controller.UpdateGameState(cG);
            Console.WriteLine(cG.GameState);
            Assert.IsTrue(cG.GameState == GameState.Running);
        }

        [Test]
        public void KnightInFrontOfKing_UpdateGamestate_Check()
        {
            var s = "r1bqk2r/pP1p2Pp/8/8/8/3n4/P1p1p1pP/R3K2R";
            var cG = new ChessGame(s, "Eduard", "Felix");
            Console.WriteLine(cG.CurrentChessBoard);
            Controller.UpdateGameState(cG);
            Console.WriteLine(cG.GameState);
            Assert.IsTrue(cG.GameState == GameState.Check);
        }
    }
}