using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Chess;
using NUnit.Framework;

namespace OOPChessProject.Tests
{
    //Bei Schachmatt dürfen keine Züge mehr gemacht werden.
    [TestFixture]
    class UnitTestGameFlow
    {
        [Test]
        public void King()
        {
            //Nach Schach dürfen nur Schach-aufhebende Züge gemacht werden
            string s = "8/NQ4K1/2Prp3/1p3B2/1R2b3/4R3/1PpP4/1q3k2";
            ChessGame cG = new ChessGame(s, "Eduard", "Felix");
            Console.WriteLine(cG.CurrentChessBoard);
            cG.CurrentChessBoard.MovePiece(new Field("E3"), new Field("F3"), MovementType.Moving, 3);
            Console.WriteLine(cG.CurrentChessBoard);
            var res = Controller.GetMovesForField(cG, new Field("F1"));
            Console.WriteLine(res);
            Assert.AreEqual(4, res.Count);
        }

        [Test]
        public void KingShouldNotCaptureProtectedPiece()
        {
            //in der Position sollte der könig nur 4 Felder haben wo er hingehen kann. Den Bauern vor ihm kann er nicht schlagen
            string s = "rnbqkbnr / ppp2ppp / 4p3/3p4/3K4/8/PPPPPPPP/RNBQ1BNR";
            ChessGame cG = new ChessGame(s, "Eduard", "Felix");
            Console.WriteLine(cG.CurrentChessBoard);
            var fields = Controller.GetMovesForField(cG, HelperFunctions.StringToField("D4"));
            Assert.AreEqual(4, fields.Count);
        }

        [Test]
        public void KingCouldCaptureThePawn()
        {
            string s = "rn1qkbnr / pppbpppp / 8 / 3p4 / 3K4 / 8 / PPPPPPPP / RNBQ1BNR";
            ChessGame cG = new ChessGame(s, "Eduard", "Felix");
            Console.WriteLine(cG.CurrentChessBoard);
            var fields = Controller.GetMovesForField(cG, HelperFunctions.StringToField("D4"));
            Assert.AreEqual(6, fields.Count);
        }

        [Test]
        public void testEnPassant()
        {
            //En passant geht nur direkt nach dem langen Zug des gegen.Bauern
            string s = "rn1qkbnr/pppbpppp/8/8/3p4/NP5N/P1PPPPPP/R1BQKB1R w KQkq";
            ChessGame cG = new ChessGame(s, "Eduard", "Felix");
            Console.WriteLine(cG.CurrentChessBoard);
            cG.CurrentChessBoard.MovePiece(new Field("E2"), new Field("E4"), MovementType.DoubleStep, 3);
            Console.WriteLine(cG.CurrentChessBoard);
            var fields = Controller.GetMovesForField(cG, new Field("D4"));
            Assert.AreEqual(2, fields.Count);

            cG.CurrentChessBoard.RemoveSomeGhosts(4);
            Console.WriteLine(cG.CurrentChessBoard);
            fields = Controller.GetMovesForField(cG, HelperFunctions.StringToField("D4"));
            Assert.AreEqual(1, fields.Count);
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

        [Test]
        public void PawnsCanDoubleStepOnlyInTheBeginning()
        {
            string s = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";
            ChessGame cG = new ChessGame(s, "Eduard", "Felix");
            Field field;
            List<Move> posMoves;
            for (int c = 0; c < 8; c++)
            {
                foreach (var row in new List<int>() {1, 6})
                {
                    field = new Field(row, c);
                    posMoves = Controller.GetMovesForField(cG, field);
                    Assert.AreEqual(2, posMoves.Count);
                }
            }

            Console.WriteLine("-------------------------------");

            string s2 = "rnbqkbnr/8/pppppppp/8/8/PPPPPPPP/8/RNBQKBNR w KQkq";
            ChessGame cG2 = new ChessGame(s2, "Eduard", "Felix");
            for (int c = 0; c < 8; c++)
            {
                foreach (var row in new List<int>() {2, 5})
                {
                    field = new Field(row, c);
                    posMoves = Controller.GetMovesForField(cG2, field);
                    Assert.AreEqual(1, posMoves.Count);
                }
            }
        }
    }
}