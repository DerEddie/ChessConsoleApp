using System;
using System.Collections.Generic;

namespace OOPChessProject.Pieces
{
    class King : Piece
    {
        
        public King(Field aField, Color aPieceColor, bool aisAlive = true) : base(aField, aPieceColor, aisAlive = true)
        {
            //already Implemented
            base.PrintRepresentation = "KI";
        }

        public void checkCastlingOptions(ChessBoard cb)
        {

        }

        public override List<Move> getPossibleMoves(ChessBoard cb, bool isrecursive)
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
            if(isrecursive)
            {
                return FilterMoves(getPossibleFieldsTraversingPieces(cb, rowOfsetcolOfset, true), cb);
            }
            else
            {
                return getPossibleFieldsTraversingPieces(cb, rowOfsetcolOfset, false);
            }
            
            return FilterMoves(getPossibleFieldsTraversingPieces(cb, rowOfsetcolOfset, false), cb);
        }


        //takes the kings movelist and checks whether enemy pieces attack this field
        public List<Move> FilterMoves(List<Move> moveList, ChessBoard cb)
        {
            HashSet<Field> forbiddenFields = new HashSet<Field>();
            var enemyList = cb.getAllPiecesOfColor(Helper.ColorSwapper(this.PieceColor));

            foreach (var piece in enemyList)
            {
                //get possible moves from that piece and get the destination field
                var moves = piece.getPossibleMoves(cb, false);

                foreach (var m in moves)
                {
                    forbiddenFields.Add(m.ToField);
                }
            }
            
            Console.WriteLine("ListForbidden");
            foreach (var field in forbiddenFields)
            {
                Console.WriteLine(field);
            }

            foreach (var move in moveList)
            {

                //getting the enemy Pieces (opposite color)
                if (forbiddenFields.Contains(move.ToField))
                {
                    moveList.Remove(move);
                }
                
            }
            return moveList;
        }


        //TODO Iterate over all pices of one kind -> Create a set so no duplicates for the fields those pieces attack. --> consider special case the pawn who moves and attacks differently

    }


}
