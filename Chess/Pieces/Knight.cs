using System.Collections.Generic;

namespace Chess.Pieces
{
    class Knight : Piece
    {
        List<(int, int)> rowOfsetcolOfset = new List<(int, int)>
        {
            (-2, -1), //2 hoch 1 links
            (-2, 1), // 2 hoch 1 rechts
            (-1, -2),  // 1 hoch  2 links
            (-1, 2), // 1 hoch 2 rechts
            (1,2), // 1 runter 2 rechts
            (1,-2), // 1 runter 2 links
            (2,1),
            (2,-1)
        };
        //constructor
        public Knight(Field position, Color pieceColor, bool aisAlive = true) : base(position, pieceColor, aisAlive = true)
        {
            //already Implemented
            base.PrintRepresentation = "KN";
        }

        public override object Clone()
        {
            Knight knight = new Knight(this.CurrField, this.PieceColor);
            return knight;
        }

        public override List<Move> getPossibleMoves(ChessBoard cb)
        {


            return GetPossibleMovesTraversing(cb, rowOfsetcolOfset, 1);
        }


    }
}
