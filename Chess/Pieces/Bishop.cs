using System.Collections.Generic;

namespace Chess.Pieces
{
    class Bishop: Piece
    {
        
        public Bishop(Color pieceColor) : base(pieceColor)
        {
            PrintRepresentation = "BS";
        }

        public override List<Move> GetPossibleMoves(ChessBoard cb, Field currentField)
        {
            var rowColOffset = new List<(int, int)>
            {
                (1 , 1), 
                (-1, -1), 
                (-1, 1), 
                (1, -1) 
            };
            return GetPossibleMovesTraversing(cb, rowColOffset, currentField);
        }

        public override object Clone()
        {
            Bishop bishop = new Bishop(this.PieceColor);
            return bishop;
        }
    }
}
