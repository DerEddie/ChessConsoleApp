using System.Collections.Generic;

namespace Chess.Pieces
{
    public class GhostPawn : Piece
    {
        //Creating a ghost pawn to implement en passant correctly.
        //The ghostpawn is tied to an actual pawn so both can disapear when ghost pawn gets captured.
        //GhostPawn wont block own movement options since it is already gone once the enemy has moved,

        public int IterationOfCreation;
        public Pawn TheRealPawn;

        public GhostPawn(Pawn p, int iterOfCreation, Field position, Color pieceColor,   bool aisAlive = true) : base(position, pieceColor, aisAlive = true)
        {
            //already Implemented
            IterationOfCreation = iterOfCreation;
            base.PrintRepresentation = "xx";
            TheRealPawn = p;
        }


        public override List<Move> getPossibleMoves(ChessBoard cb)
        {
            //Init FieldList
            List<Field> fList = new List<Field>();

            var rowOfsetcolOfset = new List<(int, int)>
            {

            };
            return GetPossibleMovesTraversing(cb, rowOfsetcolOfset, 1);
        }

        public override object Clone()
        {
            GhostPawn pawn = new GhostPawn(this.TheRealPawn, this.IterationOfCreation,CurrField, this.PieceColor);
            return pawn;
        }

    }
}