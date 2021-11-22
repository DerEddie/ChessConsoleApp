using System.Collections.Generic;

namespace OOPChessProject.Pieces
{
    class Bishop: Piece
    {
        
        public Bishop(Field aField, Color aPieceColor, bool aisAlive = true) : base(aField, aPieceColor, aisAlive = true)
        {
            base.PrintRepresentation = "BS";
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
                (1, -1) 
            };
            return getPossibleFieldsTraversingPieces(cb, rowOfsetcolOfset);
        }


    }
}
