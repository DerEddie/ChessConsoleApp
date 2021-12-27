using System.Collections.Generic;

namespace Chess.Pieces
{
    internal class Knight : Piece
    {
        private readonly List<(int, int)> _offset = new List<(int, int)>
        {
            (-2, -1),
            (-2, 1),
            (-1, -2),
            (-1, 2),
            (1,2),
            (1,-2),
            (2,1),
            (2,-1)
        };
        //constructor
        public Knight(Color pieceColor) : base(pieceColor)
        {
            //already Implemented
            PrintRepresentation = "KN";
        }

        public override object Clone()
        {
            var knight = new Knight(this.PieceColor);
            return knight;
        }

        public override List<Move> GetPossibleMoves(ChessBoard cb, Field currentField)
        {


            return GetPossibleMovesTraversing(cb, _offset, currentField, 1);
        }


    }
}
