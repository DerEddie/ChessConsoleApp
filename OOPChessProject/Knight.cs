using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPChessProject
{
    class Knight : Piece
    {
        //fields
        

        //constructor
        public Knight(Field aField, Color aPieceColor, bool aisAlive = true) : base(aField, aPieceColor, aisAlive = true)
        {
            //already Implemented
            base.PrintRepresentation = "KN";
        }

        
        public override List<Field> getPossibleFields(ChessBoard cb)
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

            //Init FieldList
            List<Field> fList = new List<Field>();
            //TODO fix hitting own piece
            // get current field
            var rowNumColNum = CurrField.fieldToNum();

            //create fields and append to the List
            int r = rowNumColNum.Item1;
            int c = rowNumColNum.Item2;


            int r_n;
            int c_n;
            foreach (var os in rowOfsetcolOfset)
            {
                r_n = r + os.Item1;
                c_n = c + os.Item2;

                if (cb.isRowAndColStillBoard(r_n, c_n)) 
                {
                    Field f_n = new Field((row)r_n, (col)c_n);
                    fList.Add(f_n);
                }
            }

            return fList;
        }


    }
}
