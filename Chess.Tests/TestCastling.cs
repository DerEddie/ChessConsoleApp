using System;
using Chess;
using NUnit.Framework;
namespace OOPChessProject.Tests
{
    [TestFixture]
    public class TestCastling
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
        
        [Test]
        public void CastlingDone_getPossibleMoves_RookWhichCastledCanMove()
        {
            string s = "rnbqk2r/pppp1ppp/7n/2b1p2Q/2B1P2N/2N5/PPPP1PPP/R1B1K2R";
            ChessGame cG = new ChessGame(s, "Eduard", "Felix");
            Console.WriteLine(cG.CurrentChessBoard);
            cG.CurrentChessBoard.MovePiece(new Field("E1"), new Field("G1"), MovementType.CastleShort, 3);
            Console.WriteLine(cG.CurrentChessBoard);
            var fields = Controller.GetMovesForField(cG, HelperFunctions.StringToField("F1"));
            Console.WriteLine(fields.Count);
        }
        [Test]
        public void getMovesForField_CastlingWhenKingMoved_isReduced()
        {
            string s = "r3k2r/pppppppp/8/8/8/8/PPPPPPPP/R3K2R w KQkq";
            ChessGame cG = new ChessGame(s, "Eduard", "Felix");
            Console.WriteLine(cG.CurrentChessBoard);
            var movesList = Controller.GetMovesForField(cG, new Field("E1"));
            Assert.AreEqual(4, movesList.Count);

            cG.CurrentChessBoard.MovePiece(new Field("E1"), new Field("F1"), MovementType.Moving, 3);
            cG.CurrentChessBoard.MovePiece(new Field("F1"), new Field("E1"), MovementType.Moving, 3);
            movesList = Controller.GetMovesForField(cG, new Field("E1"));
            Assert.AreEqual(2, movesList.Count);

            string s2 = "r3k2r/pppppppp/8/8/8/8/PPPPPPPP/R3K2R w KQkq";
            ChessGame cG2 = new ChessGame(s2, "Eduard", "Felix");
            cG2.CurrentPlayer = cG.Player2;
            //Castling is coupled with currentplayer Color!!!
            Console.WriteLine(cG2.CurrentChessBoard);
            movesList = Controller.GetMovesForField(cG2, new Field("E8"));
            Assert.AreEqual(4, movesList.Count);

            cG2.CurrentChessBoard.MovePiece(new Field("E8"), new Field("F8"), MovementType.Moving, 3);
            cG2.CurrentChessBoard.MovePiece(new Field("F8"), new Field("E8"), MovementType.Moving, 3);
            movesList = Controller.GetMovesForField(cG2, new Field("E8"));
            Assert.AreEqual(2, movesList.Count);
        }
    }
}
