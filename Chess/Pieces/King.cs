using System.Collections.Generic;

namespace Chess.Pieces
{
    public class King : Piece
    {
        private readonly List<(int, int)> _offset = new List<(int, int)>
        {
            (1, 1),
            (-1, -1),
            (-1, 1),
            (1, -1),
            (1, 0),
            (-1, 0),
            (0, 1),
            (0, -1)
        };

        public King(Color pieceColor) : base(pieceColor)
        {
            //already Implemented
            PrintRepresentation = "KI";
        }

        public override object Clone()
        {
            King king = new King(this.PieceColor);
            return king;
        }

        public override List<Move> GetPossibleMoves(ChessBoard cb, Field currentField)
        {
            return GetPossibleMovesTraversing(cb, _offset, currentField, 1);
        }


    }


}
