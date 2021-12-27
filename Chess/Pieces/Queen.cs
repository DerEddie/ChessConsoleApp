using System.Collections.Generic;

namespace Chess.Pieces
{
    internal class Queen: Piece
    {
        public Queen(Color pieceColor) : base(pieceColor)
        {
            //already Implemented
            PrintRepresentation = "QN";
        }

        public override List<Move> GetPossibleMoves(ChessBoard cb, Field currentField)
        {
            var offset = new List<(int, int)>
            {
                (1 , 1),
                (-1, -1),
                (-1, 1),
                (1, -1),
                (1,  0),
                (-1 , 0),
                (0, 1),
                (0, -1)
            };
            return GetPossibleMovesTraversing(cb, offset, currentField);
        }

        public override object Clone()
        {
            var queen = new Queen(this.PieceColor);
            return queen;
        }
    }
}
