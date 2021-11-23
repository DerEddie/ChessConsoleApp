using System;
using System.Collections.Generic;

namespace OOPChessProject.Pieces
{
    class King : Piece
    {

        List<(int, int)> rowOfsetcolOfset = new List<(int, int)>
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

        public King(Field position, Color pieceColor, bool aisAlive = true) : base(position, pieceColor, aisAlive = true)
        {
            //already Implemented
            base.PrintRepresentation = "KI";
        }



        public override List<Move> getPossibleMoves(ChessBoard cb)
        {
            //Init FieldList
            List<Field> fList = new List<Field>();

            
            return getPossibleMovesTraversing(cb, rowOfsetcolOfset, false);
        }

        #region SomeComplicated stuff
        /*
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
        */
        #endregion
        //TODO Iterate over all pices of one kind -> Create a set so no duplicates for the fields those pieces attack. --> consider special case the pawn who moves and attacks differently

    }


}
