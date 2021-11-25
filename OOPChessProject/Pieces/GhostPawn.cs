using System.Collections.Generic;

namespace OOPChessProject.Pieces
{
    public class GhostPawn : Piece
    {
        //Creating a ghost pawn to implement en passant correctly.
        //The ghostpawn is tied to an actual pawn so both can disapear when ghost pawn gets captured.
        //GhostPawn wont block own movement options since it is already gone once the enemy has moved,

        public int IterationOfCreation;
        public Pawn TheRealPawn;

        public GhostPawn(int iterOfCreation, Field position, Color pieceColor,   bool aisAlive = true) : base(position, pieceColor, aisAlive = true)
        {
            //already Implemented
            IterationOfCreation = iterOfCreation;
            base.PrintRepresentation = "xx";
        }

        //TODO (1) consider en passant. Create Ghost Instance - new Class with Iteration and Pawn as Field,
        //TODO (2) selfdestruct after one Iter. if destroyed by enemy destroy the Pawn as Field


        public override List<Move> getPossibleMoves(ChessBoard cb)
        {
            //Init FieldList
            List<Field> fList = new List<Field>();

            var rowOfsetcolOfset = new List<(int, int)>
            {

            };
            return getPossibleMovesTraversing(cb, rowOfsetcolOfset, 1);
        }

        public override object Clone()
        {
            GhostPawn pawn = new GhostPawn(this.IterationOfCreation,CurrField, this.PieceColor);
            return pawn;
        }

    }
}