using System.Collections.Generic;

namespace OOPChessProject.Pieces
{
    class Rook : Piece
    {
        

        public Rook(Field aField, Color aPieceColor, bool aisAlive = true) : base(aField, aPieceColor, aisAlive = true)
        {
            //already Implemented
            base.PrintRepresentation = "RK";
        }

        public override List<Move> getPossibleMoves(ChessBoard cb, bool isrecursive)
        {
            //Init FieldList
            List<Field> fList = new List<Field>();

            var rowOfsetcolOfset = new List<(int, int)>
            {
                (1,  0),
                (-1 , 0),
                (0, 1),
                (0, -1)
            };
            return getPossibleFieldsTraversingPieces(cb, rowOfsetcolOfset);
        }
    }
}
