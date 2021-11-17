using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPChessProject
{
    class Pawn : Piece
    {

        public Pawn(Field aField, Color aPieceColor, bool aisAlive = true) : base(aField, aPieceColor, aisAlive)
        {
            //already Implemented
            base.PrintRepresentation= "PW";
        }

        public override List<Field> getPossibleFields(ChessBoard cb)
        {
            //Since Pawns move only forward, We need to know whether piece is black or white
            var rc_offset = new int[,] { { -1, 0 }};
            

            List<Field> fList = new List<Field>();

            row r = CurrField.fieldRow;
            col c = CurrField.fieldCol;

            //convert from enum to int
            int r_nr = (int)r;
            int c_nr = (int)c;

            //Pawn move in different direction depending on color
            int directionFactor;
            //use the chessboard
            if (this.PieceColor == Color.White)
            {
                directionFactor = -1;

            }
            else
            {
                directionFactor = 1;
            }

            //convert back to enum with modified
            row r1 = (row)r_nr + 1*(-directionFactor);
            col c1 = (col)c_nr;
            Field f = new Field(r1, c1);
            //check whether Field is Empty
            if (cb.IsFieldEmpty(f)) fList.Add(f);

            //Check the double-step
            if (this.CurrField.FieldRow == row._2 | this.CurrField.FieldRow ==  row._7)
            {
                r1 = (row)r_nr + 2*(-directionFactor);
                c1 = (col)c_nr;
                f = new Field(r1, c1);
                if (cb.IsFieldEmpty(f)) fList.Add(f);
            }

            //TODO Pawn can only attack opposite color..



            return fList;
        }
    }
}
