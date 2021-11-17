using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPChessProject
{
    class King : Piece
    {
        
        public King(Field aField, Color aPieceColor, bool aisAlive = true) : base(aField, aPieceColor, aisAlive = true)
        {
            //already Implemented
            base.PrintRepresentation = "KI";
        }


        public override List<Field> getPossibleFields(ChessBoard cb)
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
            return getPossibleFieldsTraversingPieces(cb, rowOfsetcolOfset, false);
        }

        //TODO Iterate over all pices of one kind -> Create a set so no duplicates for the fields those pieces attack. --> consider special case the pawn who moves and attacks differntly
    }

}
