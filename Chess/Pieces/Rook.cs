using System.Collections.Generic;

namespace Chess.Pieces
{
    internal class Rook : Piece
    {
        public Rook(Color pieceColor) : base(pieceColor)
        {
            //already Implemented
            base.PrintRepresentation = "RK";
        }
        public override List<Move> GetPossibleMoves(ChessBoard cb, Field currentField)
        {
            //Init FieldList
            List<Field> fList = new List<Field>();
            var rowOfsetcolOfset = new List<(int, int)>
            {
                (1,  0),
                (-1 , 0),
                (0, 1),
                (0, -1)
            };
            return GetPossibleMovesTraversing(cb, rowOfsetcolOfset, currentField);
        }
        public override object Clone()
        {
            Rook rook = new Rook(this.PieceColor);
            return rook;
        }
    }
}
