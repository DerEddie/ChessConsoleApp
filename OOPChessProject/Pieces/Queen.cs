using System.Collections.Generic;

namespace OOPChessProject.Pieces
{
    class Queen: Piece
    {

        public Queen(Field position, Color aPieceColor, bool aisAlive = true) : base(position, aPieceColor, aisAlive = true)
        {
            //already Implemented
            base.PrintRepresentation = "QN";
        }

        public override List<Move> getPossibleMoves(ChessBoard cb, bool isrecursive)
        {
            //Init FieldList
            List<Field> fList = new List<Field>();

            var rowOfsetcolOfset = new List<(int, int)>
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
            return getPossibleMovesTraversing(cb, rowOfsetcolOfset);
        }

    }
}
