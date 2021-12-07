using System.Collections.Generic;

namespace Chess.Pieces
{
    class Bishop: Piece
    {
        
        public Bishop(Field position, Color pieceColor, bool aisAlive = true) : base(position, pieceColor, aisAlive = true)
        {
            base.PrintRepresentation = "BS";
        }

        public override List<Move> getPossibleMoves(ChessBoard cb)
        {
            //Init FieldList
            List<Field> fList = new List<Field>();

            var rowOfsetcolOfset = new List<(int, int)>
            {
                (1 , 1), 
                (-1, -1), 
                (-1, 1), 
                (1, -1) 
            };
            return GetPossibleMovesTraversing(cb, rowOfsetcolOfset);
        }

        public override object Clone()
        {
            Bishop bishop = new Bishop(this.CurrField, this.PieceColor);
            return bishop;
        }
    }
}
