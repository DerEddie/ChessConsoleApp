using System;
using Chess;
using NUnit.Framework;


namespace OOPChessProject.Tests
{
    //Bei Schachmatt dürfen keine Züge mehr gemacht werden.
    [TestFixture]
    public class UnitTestGameFlow
    {
        [Test]
        public void King()
        {
            //Nach Schach dürfen nur Schach-aufhebende Züge gemacht werden
            string s = "8/NQ4K1/2Prp3/1p3B2/1R2b3/4R3/1PpP4/1q3k2";
            ChessGame cG = new ChessGame(s, "Eduard", "Felix");
            Console.WriteLine(cG.CurrentChessBoard);
            var res = Controller.GetMovesForField(cG, new Field("F1"));
            Console.WriteLine(res);
            Assert.AreEqual(3, res.Count);
        }
        [Test]
        public void KingShouldNotCaptureProtectedPiece()
        {
            //in der Position sollte der könig nur 4 Felder haben wo er hingehen kann. Den Bauern vor ihm kann er nicht schlagen
            string s = "rnbqkbnr / ppp2ppp / 4p3/3p4/3K4/8/PPPPPPPP/RNBQ1BNR";
            ChessGame cG = new ChessGame(s, "Eduard", "Felix");
            Console.WriteLine(cG.CurrentChessBoard);
            var fields = Controller.GetMovesForField(cG, HelperFunctions.StringToField("D4"));
            Console.WriteLine(fields);
            Assert.AreEqual(4, fields.Count);
        }

        [Test]
        public void CanOnlySelectPieceWithRightColor()
        {
            string s = "rn1qkbnr/pppbpppp/8/8/3p4/NP5N/P1PPPPPP/R1BQKB1R w KQkq";
            ChessGame cG = new ChessGame(s, "Eduard", "Felix");
            Console.WriteLine(cG.CurrentChessBoard);
            //its white's turn but we try to move black
            var of = HelperFunctions.StringToField("D4");
            Assert.IsFalse(Controller.IsFieldValid(cG, of));
            //we try to move white
            of = HelperFunctions.StringToField("D2");
            Assert.IsTrue(Controller.IsFieldValid(cG, of));
        }

        //Precondition_TargetTestFunction_ExpectedResult
        [Test]
        public void GetMovesForKing_inCheck_cantStepBack()
        {
            ChessGame cG = new ChessGame("rnbqkbn1/pppppppp/8/8/7N/8/PPPPrPPP/RQB3K1", "A", "B");
            Console.WriteLine(cG.CurrentChessBoard);
            cG.CurrentChessBoard.MovePiece(new Field("E2"), new Field("E1"), MovementType.Moving,43);
            var moves = Controller.GetMovesForField(cG, new Field("G1"));
            Assert.AreEqual(moves.Count, 0);
        }

    }
}