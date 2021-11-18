using System;
using System.Collections.Generic;
using System.ComponentModel;
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


        public override List<Move> getPossibleMoves(ChessBoard cb)
        {
            //Init FieldList
            List<Field> fList = new List<Field>();

            var rowOfsetcolOfset = new List<(int, int)>
            {
                (1, 1),
                (-1, -1),
                (-1, 1),
                (1, -1),
                (1, 0),
                (-1, 0),
                (0, 1),
                (0, -1)
            };
            Helper.ColorSwapper(Color.White);
            return getPossibleFieldsTraversingPieces(cb, rowOfsetcolOfset, false);
        }


/*
        public List<Move> filterMoves(List<Move> moveList)
        {
            foreach (var move in moveList)
            {
                foreach EnemyPiece()
                {
                    foreach possibleField EnemyPiece
                    {
                        // <--+#+#+-->
                    }
                }
            }
        }
*/

        //TODO Iterate over all pices of one kind -> Create a set so no duplicates for the fields those pieces attack. --> consider special case the pawn who moves and attacks differently

    }


}
