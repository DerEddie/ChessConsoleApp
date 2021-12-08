
using NUnit.Framework;
using System;
using Chess;
using Chess.Pieces;

namespace OOPChessProject.Tests
{
    //decorator = special attribute
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            ChessGame ChessGame = new ChessGame("Eduard", "Lukas");
            string board = ChessGame.CurrentChessBoard.ToString();
            Console.Write(board.Length);
            Assert.AreEqual(board.Length, 494);
        }



        [Test]
        public void TestBoardCopy()
        {
            ChessBoard chessBoard = new ChessBoard();
            ChessBoard copyBoard = new ChessBoard(chessBoard);
            copyBoard.MovePiece(new Field(1,4), new Field(3, 4), MovementType.Moving, 0);

            Console.WriteLine(chessBoard);
            Console.WriteLine(copyBoard);

            Assert.AreNotEqual(copyBoard.ToString(),chessBoard.ToString());
        }

        [Test]
        public void TestLongCastle()
        {

            ChessBoard cb = new ChessBoard();
            cb.MovePiece(new Field(0,1), new Field(5,1), MovementType.Moving, 0);
            cb.MovePiece(new Field(0, 2), new Field(5, 2), MovementType.Moving, 0);
            cb.MovePiece(new Field(0, 3), new Field(5, 3), MovementType.Moving, 0);
            //First test should be true
            Console.WriteLine(cb);
            Console.WriteLine(cb.CastleLong(Color.White));

            cb.MovePiece(new Field(0, 4), new Field(3, 4), MovementType.Moving, 0);
            cb.MovePiece(new Field(3, 4), new Field(0, 4), MovementType.Moving, 0);
            //second test shouldnt because piece was moved back and forth.
            Console.WriteLine(cb);
            Console.WriteLine(cb.CastleLong(Color.White));
        }

        [Test]
        public void TestCastleShort()
        {
            //Open Space for white
            string s1 = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQK2R";
            ChessGame cg1 = new ChessGame(s1, "E", "K");
            Console.WriteLine(cg1.CurrentChessBoard);
            Console.WriteLine(cg1.CurrentChessBoard.CastleShort(Color.White));

            //Open Space for black
            string s2 = "rnbqk2r/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";
            ChessGame cg2 = new ChessGame(s2, "E", "K");
            Console.WriteLine(cg2.CurrentChessBoard);
            Console.WriteLine(cg2.CurrentChessBoard.CastleShort(Color.Black));
            Console.WriteLine(cg2.CurrentChessBoard.CastleShort(Color.White));
            //
        }

        [Test]
        public void cGame()
        {
            ChessGame cG = new ChessGame("A","B");
            cG.CurrentChessBoard.MovePiece(new Field(1,3), new Field(3,3), MovementType.Moving, 0);
            cG.CurrentChessBoard.MovePiece(new Field(7, 2), new Field(3, 1), MovementType.Moving, 0);
            //Console.WriteLine(cG.currentChessBoard);
            //Console.WriteLine(cG.currentChessBoard.isChecked(Color.Black));
            bool shouldBeCheck = cG.CurrentChessBoard.IsChecked(Color.Black);

            cG.CurrentChessBoard.MovePiece(new Field(1, 2), new Field(2, 2), MovementType.Moving, 0);
            //Console.WriteLine(cG.currentChessBoard);
            //Console.WriteLine(cG.currentChessBoard.isChecked(Color.Black));
            bool shouldntBeCheck = cG.CurrentChessBoard.IsChecked(Color.Black);

            bool checkAndnoCheckAfterBlock = (shouldBeCheck == true) && (shouldntBeCheck == false);
            Assert.IsTrue(checkAndnoCheckAfterBlock);
        }

        [Test]
        public void testCastleShort()
        {


        }

        [Test]
        public void fen_notation_tester()
        {
            string s = "rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR";
            ChessGame cG = new ChessGame(s, "Eduard", "Felix");
            Console.WriteLine(cG.CurrentChessBoard);
        }

        [Test]
        public void initBoardWithFEN()
        {
            string s = "rnbqkbnr / pppppppp / 8 / 8 / 8 / 8 / PPPPPPPP / RNBQKBNR";
            ChessGame cG = new ChessGame(s, "Eduard", "Felix");


            ChessGame cGinit = new ChessGame("A", "B");
            //Console.WriteLine(cG.currentChessBoard);
            //Console.WriteLine(cGinit.currentChessBoard);

            Assert.AreEqual(cG.CurrentChessBoard.ToString(), cGinit.CurrentChessBoard.ToString());
        }

        [Test]
        public void testCastlingLong()
        {
            string s = "r3kbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQK2R";
            ChessGame cG = new ChessGame(s, "Eduard", "Felix");
            Console.WriteLine(cG.CurrentChessBoard);
            cG.CurrentChessBoard.MovePiece(new Field("E1"), new Field("G1"), MovementType.CastleShort, 5);
            Console.WriteLine(cG.CurrentChessBoard);
        }

        public void testCastlingShort()
        {
            string s = "r3kbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQK2R";
            ChessGame cG = new ChessGame(s, "Eduard", "Felix");
            Console.WriteLine(cG.CurrentChessBoard);
            cG.CurrentChessBoard.MovePiece(new Field("E1"), new Field("G1"), MovementType.CastleShort, 5);
            Console.WriteLine(cG.CurrentChessBoard);
        }

        [Test]
        public void TestFieldConstructor()
        {

            Field f = new Field("E1");
            Console.WriteLine(f);
        }

        [Test]
        public void TryCheckmate()
        {
            string s = "rnbqkbnr/2pppppppp / 8 / 8 / 8 / 8 / 2PPPPPP / 6K1";
            ChessGame cG = new ChessGame(s, "Eduard", "Felix");
            Console.WriteLine(cG.CurrentChessBoard);
        }
    }
}
