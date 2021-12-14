using Chess;
using Chess.Pieces;
using NUnit.Framework;


namespace OOPChessProject.Tests
{
    [TestFixture]
    class UnitTestPieces
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

    }
}