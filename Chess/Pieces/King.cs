using System;
using System.Collections.Generic;
using Chess;

namespace OOPChessProject.Pieces
{
    public class King : Piece
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

        public override object Clone()
        {
            King king = new King(this.CurrField, this.PieceColor);
            return king;
        }

        public override List<Move> getPossibleMoves(ChessBoard cb)
        {
            //Init FieldList
            List<Field> fList = new List<Field>();

            
            return GetPossibleMovesTraversing(cb, rowOfsetcolOfset, 1);
        }


    }


}
