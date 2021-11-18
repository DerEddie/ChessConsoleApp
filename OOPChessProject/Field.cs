using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace OOPChessProject
{
    //make public if other classes should use it.
    public enum row
    {
        _1,
        _2,
        _3,
        _4,
        _5,
        _6,
        _7,
        _8
    };
}

// (int)row._1, (row)0
namespace OOPChessProject
{
    public enum col
    {
        A,
        B,
        C,
        D,
        E,
        F,
        G,
        H
    };

    public class Field
    {
        public row FieldRow;
        public col FieldCol;




        //creating a constructor for a field instance
        public Field(row aFieldRow, col aFieldCol)
        {
            FieldRow = aFieldRow;
            FieldCol = aFieldCol;          
        }


        public override string ToString()
        {
            return String.Format("ROW:{0},COL:{1}",this.FieldRow.ToString() ,  this.FieldCol.ToString());
        }


        //define Getter And Setter for the FieldRow and FieldCol because the Piece wants to know its location
        public row fieldRow   // property
        {
            get { return FieldRow; }   // get method
            set { FieldRow = value; }  // set method
        }

        public col fieldCol   // property
        {
            get { return FieldCol; }   // get method
            set { FieldCol = value; }  // set method
        }

        public Tuple<int, int> fieldToNum()
        {
            row r = this.fieldRow;
            col c = this.fieldCol;

            //convert from enum to int
            int r_nr = (int)r;
            int c_nr = (int)c;
            return new Tuple<int, int>(r_nr, c_nr);
        }

        
    }
}
