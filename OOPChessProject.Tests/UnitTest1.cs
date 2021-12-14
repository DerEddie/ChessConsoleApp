
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
            var ChessGame = new ChessGame("Eduard", "Lukas");
            var board = ChessGame.CurrentChessBoard.ToString();
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
            cb.TryCastleLong(Color.White, out var move);
            Console.WriteLine(move);

            cb.MovePiece(new Field(0, 4), new Field(3, 4), MovementType.Moving, 0);
            cb.MovePiece(new Field(3, 4), new Field(0, 4), MovementType.Moving, 0);
            //second test shouldnt because piece was moved back and forth.
            Console.WriteLine(cb);
            cb.TryCastleLong(Color.White, out move);
            Console.WriteLine(move);
        }

        [Test]
        public void TestCastleShort()
        {
            //Open Space for white
            string s1 = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQK2R";
            ChessGame cg1 = new ChessGame(s1, "E", "K");
            Console.WriteLine(cg1.CurrentChessBoard);
            Console.WriteLine(cg1.CurrentChessBoard.TryCastleShort(Color.White));

            //Open Space for black
            string s2 = "rnbqk2r/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";
            ChessGame cg2 = new ChessGame(s2, "E", "K");
            Console.WriteLine(cg2.CurrentChessBoard);
            Console.WriteLine(cg2.CurrentChessBoard.TryCastleShort(Color.Black));
            Console.WriteLine(cg2.CurrentChessBoard.TryCastleShort(Color.White));
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


            ChessGame cInit = new ChessGame("A", "B");
            //Console.WriteLine(cG.currentChessBoard);
            //Console.WriteLine(cGinit.currentChessBoard);

            Assert.AreEqual(cG.CurrentChessBoard.ToString(), cInit.CurrentChessBoard.ToString());
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
        public void getMovesForField_Castling_isReduced()
        {
            string s = "r3k2r/pppppppp/8/8/8/8/PPPPPPPP/R3K2R w KQkq";
            ChessGame cG = new ChessGame(s, "Eduard", "Felix");
            Console.WriteLine(cG.CurrentChessBoard);
            var MovesList  = Controller.GetMovesForField(cG, new Field("E1"));
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
