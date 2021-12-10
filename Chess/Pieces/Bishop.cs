using System.Collections.Generic;

namespace Chess.Pieces
{
    class Bishop: Piece
    {
        
        public Bishop(Field position, Color pieceColor) : base(position, pieceColor)
        {
            PrintRepresentation = "BS";
        }

        public override List<Move> GetPossibleMoves(ChessBoard cb)
        {
            var rowColOffset = new List<(int, int)>
            {
                (1 , 1), 
                (-1, -1), 
                (-1, 1), 
                (1, -1) 
            };
            return GetPossibleMovesTraversing(cb, rowColOffset);
        }

        public override object Clone()
        {
            Bishop bishop = new Bishop(this.CurrField, this.PieceColor);
            return bishop;
        }
    }
}
