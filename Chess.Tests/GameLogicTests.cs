using System;
using Chess;
using Chess.Pieces;
using NUnit.Framework;

namespace OOPChessProject.Tests
{
    [TestFixture]
    internal class GameLogicTests
    {
        [Test]
        public static void CheckmatePosition_CheckMitigationPossible_False()
        {
            string s = "4k3/8/8/8/8/8/3PPP2/r3K3";
            ChessGame cG = new ChessGame(s, "Eduard", "Felix");
            var x = cG.CheckMitigationPossible();
            Assert.IsFalse(x);
        }
        
        [Test]
        public static void CheckmatePosition_CheckMitigationPossible_True()
        {
            string s = "4k3/8/8/8/8/4P3/3P1P2/r3K3";
            ChessGame cG = new ChessGame(s, "Eduard", "Felix");
            var x = cG.CheckMitigationPossible();
            Assert.IsTrue(x);
        }
        
        
        [Test]
        public static void PiecePinned_FilterMoves_MoveListForPieceEmpty()
        {
            string s = "rnbqkbnr/pppp1ppp/8/4p2Q/4P3/8/PPPP1PPP/RNB1KBNR";
            ChessGame cG = new ChessGame(s, "Eduard", "Felix");
            Console.WriteLine(cG.CurrentChessBoard);
            var moves = Controller.GetMovesForField(cG, new Field("F7"));
            Console.WriteLine(moves);
            var movesfiltered= cG.FilterMoveWhichExposeCheck(moves, Color.Black);
            Console.WriteLine(movesfiltered.Count);

            Assert.AreEqual(0, movesfiltered.Count);

            var moves2 = Controller.GetMovesForField(cG, new Field("A7"));
            var moves2filtered = cG.FilterMoveWhichExposeCheck(moves2, Color.Black);
            Console.WriteLine(moves2filtered.Count);

            Assert.AreEqual(2, moves2filtered.Count);
        }

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
