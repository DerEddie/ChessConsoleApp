using System.Collections.Generic;

namespace Chess.Pieces
{
    internal class Queen: Piece
    {
        public Queen(Field position, Color pieceColor, bool aisAlive = true) : base(position, pieceColor)
        {
            //already Implemented
            PrintRepresentation = "QN";
        }

        public override List<Move> GetPossibleMoves(ChessBoard cb)
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
            return GetPossibleMovesTraversing(cb, offset);
        }

        public override object Clone()
        {
            var queen = new Queen(this.CurrentField, this.PieceColor);
            return queen;
        }
    }
}
