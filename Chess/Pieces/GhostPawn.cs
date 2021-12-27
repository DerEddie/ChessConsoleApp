using System.Collections.Generic;

namespace Chess.Pieces
{
    public class GhostPawn : Piece
    {
        //Creating a ghost pawn to implement "en passant" correctly.
        //The ghost-pawn is tied to an actual pawn so both can disappear when ghost pawn gets captured.
        //GhostPawn wont block own movement options since it is already gone once the enemy has moved,

        public int IterationOfCreation;
        public Pawn TheRealPawn;

        public GhostPawn(Pawn p, int iterationOfCreation, Color pieceColor) : base(pieceColor)
        {
            //already Implemented
            IterationOfCreation = iterationOfCreation;
            PrintRepresentation = "xx";
            TheRealPawn = p;
        }


        public override List<Move> GetPossibleMoves(ChessBoard cb, Field currentField)
        {
            var rowColOffset = new List<(int, int)>();
            return GetPossibleMovesTraversing(cb, rowColOffset, currentField, 1);
        }

        public override object Clone()
        {
            GhostPawn pawn = new GhostPawn(this.TheRealPawn, this.IterationOfCreation, this.PieceColor);
            return pawn;
        }

    }
}