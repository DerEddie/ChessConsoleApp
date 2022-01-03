using System.Collections.Generic;

namespace Chess.Pieces
{
    class Rook : Piece
    {
        public Rook(Field position, Color pieceColor) : base(position, pieceColor)
        {
            //already Implemented
            base.PrintRepresentation = "RK";
        }

        public override List<Move> GetPossibleMoves(ChessBoard cb)
        {
            //Init FieldList
            List<Field> fList = new List<Field>();

            var rowOffsetColOffset = new List<(int, int)>
            {
                (1,  0),
                (-1 , 0),
                (0, 1),
                (0, -1)
            };
            return GetPossibleMovesTraversing(cb, rowOffsetColOffset);
        }

        public override object Clone()
        {
            Rook rook = new Rook(this.CurrentField, this.PieceColor);
            return rook;
        }
    }
}
