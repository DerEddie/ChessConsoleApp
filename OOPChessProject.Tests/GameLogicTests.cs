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
    internal class GameLogicTests
    {
        [Test]
        public static void BoardWithoutMovesForWhite_MovesAvailable_NoMoves()
        {
            string s = "rnbqkbnr/pppp1ppp/8/4p2Q/2B1P3/8/PPPP1PPP/RNB1K1NR";
            ChessGame cG = new ChessGame(s, "Eduard", "Felix");
            Console.WriteLine(cG.CurrentChessBoard);
            cG.CurrentChessBoard.MovePiece(new Field("H5"), new Field("F7"), MovementType.Capturing, 5);
            Console.WriteLine(cG.CurrentChessBoard);
            cG.CurrentPlayer = cG.Player2;
            var x = cG.CheckMitigationPossible();
            Assert.IsFalse(x);
        }


        [Test]
        public static void CheckMateSituation_TestCheckMitigationPossible_KingIsCheckMated()
        {
            string s = "rnbqkbnr/pppp1ppp/8/4p2Q/2B1P3/8/PPPP1PPP/RNB1K1NR";
            ChessGame cG = new ChessGame(s, "Eduard", "Felix");
            Console.WriteLine(cG.CurrentChessBoard);
            cG.CurrentChessBoard.MovePiece(new Field("H5"), new Field("F7"),MovementType.Capturing,5);
            Console.WriteLine(cG.CurrentChessBoard);
            cG.CurrentPlayer = cG.Player2;
            var x = cG.CheckMitigationPossible();
            Assert.IsFalse(x);
        }

        [Test]
        public static void CheckMateSituation_TestCheckMitigationPossible_KingIsNotCheckMate()
        {
            string s = "rnbqkb1r/pppp1ppp/7n/4p2Q/2B1P3/2N5/PPPP1PPP/R1B1K1NR";
            ChessGame cG = new ChessGame(s, "Eduard", "Felix");
            Console.WriteLine(cG.CurrentChessBoard);
            cG.CurrentChessBoard.MovePiece(new Field("H5"), new Field("F7"), MovementType.Capturing, 5);
            Console.WriteLine(cG.CurrentChessBoard);
            cG.CurrentPlayer = cG.Player2;
            var x = cG.CheckMitigationPossible();
            Assert.IsTrue(x);
        }


        [Test]
        public static void BoardWithCHeck_isCheckedByColor_CheckDetected()
        {
            string s = "rnbqkb1r/pppp2pp/5p1n/4p2Q/2B1P3/2N5/PPPP1PPP/R1B1K1NR";
            ChessGame cG = new ChessGame(s, "Eduard", "Felix");
            Console.WriteLine(cG);
            var x = cG.CurrentChessBoard.IsCheckedByColor(Chess.Pieces.Color.White);
            Console.WriteLine(cG);
            Console.WriteLine(x);
            Assert.IsTrue(x);
        }

        [Test]
        public static void BoardWithCHeck_isCheckedByColor_NoCheckDetected()
        {
            string s = "rnbqkb1r/pppp1ppp/7n/4p2Q/2B1P3/2N5/PPPP1PPP/R1B1K1NR";
            ChessGame cG = new ChessGame(s, "Eduard", "Felix");
            Console.WriteLine(cG);
            var x = cG.CurrentChessBoard.IsCheckedByColor(Chess.Pieces.Color.White);
            Console.WriteLine(cG);
            Console.WriteLine(x);
            Assert.IsFalse(x);
        }
    }
}
