using System;
using System.Collections.Generic;
using Chess;
using Chess.Pieces;
using NUnit.Framework;


namespace OOPChessProject.Tests
{
    [TestFixture]
    class TestPawn
    {
        public void Pawn_GetPossibleMoves_FieldOffsetWorks(int fieldOffset, Color color, bool expectedResult)
        {
            var r = 1;
            var targetR = r + fieldOffset;

            if (color == Color.Black)
            {
                r = 6;
                targetR = r - fieldOffset;
            }

            ChessBoard cb = new ChessBoard();

            for (int c = 0; c != 8; c++)
            {
                var field = new Field(r, c);
                var targetField = new Field(targetR, c);
                Piece pawn;
                cb.TryGetPieceFromField(field, out pawn);

                Assert.True(pawn is Pawn);
                Assert.False(pawn.HasMovedOnce);
                Assert.True(pawn.PieceColor == color);

                Piece p;
                cb.TryGetPieceFromField(targetField, out p);

                Assert.True(p is null);


                bool moveFound = false;
                foreach (var move in pawn.GetPossibleMoves(cb))
                {
                    if (move.FromField.Equals(field))
                    {
                        if (move.ToField.Equals(targetField))
                        {
                            moveFound = true;
                            break;
                        }
                    }
                }
                Assert.AreEqual(moveFound, expectedResult);
            }
        }
        [Test]
        public void GetPossibleMoves_PawnSingleMove_Works()
        {
            Pawn_GetPossibleMoves_FieldOffsetWorks(1, Color.White, true);
            Pawn_GetPossibleMoves_FieldOffsetWorks(1, Color.Black, true);
        }
        [Test]
        public void Pawn_GetPossibleMoves_DoubleMoveWorks()
        {
            Pawn_GetPossibleMoves_FieldOffsetWorks(2, Color.White, true);
            Pawn_GetPossibleMoves_FieldOffsetWorks(2, Color.Black, true);
        }
        [Test]
        public void Pawn_GetPossibleMoves_TripleMoveFails()
        {
            //Negativ-Test
            Pawn_GetPossibleMoves_FieldOffsetWorks(3, Color.White, false);
            Pawn_GetPossibleMoves_FieldOffsetWorks(3, Color.Black, false);
        }
        [Test]
        public void TestEnPassant()
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
        public void PawnsCanDoubleStepOnlyInTheBeginning()
        {
            string s = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";
            ChessGame cG = new ChessGame(s, "Eduard", "Felix");
            Field field;
            List<Move> posMoves;
            for (int c = 0; c < 8; c++)
            {
                foreach (var row in new List<int>() { 1, 6 })
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
                foreach (var row in new List<int>() { 2, 5 })
                {
                    field = new Field(row, c);
                    posMoves = Controller.GetMovesForField(cG2, field);
                    Assert.AreEqual(1, posMoves.Count);
                }
            }
        }
    }
}