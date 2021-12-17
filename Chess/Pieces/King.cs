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

        public King(Field position, Color pieceColor) : base(position, pieceColor)
        {
            //already Implemented
            PrintRepresentation = "KI";
        }

        public override object Clone()
        {
            King king = new King(this.CurrentField, this.PieceColor);
            return king;
        }

        public override List<Move> GetPossibleMoves(ChessBoard cb)
        {
            return GetPossibleMovesTraversing(cb, _offset, 1);
        }


    }


}
