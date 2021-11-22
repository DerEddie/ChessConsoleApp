using System.Collections.Generic;

namespace OOPChessProject.Pieces
{
    class Knight : Piece
    {
        //constructor
        public Knight(Field aField, Color aPieceColor, bool aisAlive = true) : base(aField, aPieceColor, aisAlive = true)
        {
            //already Implemented
            base.PrintRepresentation = "KN";
        }

        
        public override List<Move> getPossibleMoves(ChessBoard cb, bool isrecursive)
        {
            var rowOfsetcolOfset = new List<(int, int)>
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

            return getPossibleFieldsTraversingPieces(cb, rowOfsetcolOfset, false);
        }


    }
}
