using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess;
using NUnit.Framework;

namespace OOPChessProject.Tests
{
    [TestFixture]
    public class TestCastling
    {
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
            var MovesList = Controller.GetMovesForField(cG, new Field("E1"));
            Assert.AreEqual(4, MovesList.Count);

            cG.CurrentChessBoard.MovePiece(new Field("E1"), new Field("F1"), MovementType.Moving, 3);
            cG.CurrentChessBoard.MovePiece(new Field("F1"), new Field("E1"), MovementType.Moving, 3);
            MovesList = Controller.GetMovesForField(cG, new Field("E1"));
            Assert.AreEqual(2, MovesList.Count);

            string s2 = "r3k2r/pppppppp/8/8/8/8/PPPPPPPP/R3K2R w KQkq";
            ChessGame cG2 = new ChessGame(s2, "Eduard", "Felix");
            cG2.CurrentPlayer = cG.Player2;
            //Castling is coupled with currentplayer Color!!!
            Console.WriteLine(cG2.CurrentChessBoard);
            MovesList = Controller.GetMovesForField(cG2, new Field("E8"));
            Assert.AreEqual(4, MovesList.Count);

            cG2.CurrentChessBoard.MovePiece(new Field("E8"), new Field("F8"), MovementType.Moving, 3);
            cG2.CurrentChessBoard.MovePiece(new Field("F8"), new Field("E8"), MovementType.Moving, 3);
            MovesList = Controller.GetMovesForField(cG2, new Field("E8"));
            Assert.AreEqual(2, MovesList.Count);

        }
    }
}
