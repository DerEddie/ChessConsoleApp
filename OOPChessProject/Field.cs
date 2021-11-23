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
        _1= 0,
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
        A = 0,
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
        public int FieldRow; //row = 0 => 1
        public int FieldCol; //col = 0 => A




        //creating a constructor for a field instance
        public Field(int aFieldRow, int aFieldCol)
        {
            FieldRow = aFieldRow;
            FieldCol = aFieldCol;          
        }


        public override string ToString()
        {
            return String.Format("{1}{0}", (row)this.FieldRow ,  (col)this.FieldCol);
        }


        //define Getter And Setter for the FieldRow and FieldCol because the Piece wants to know its location
        public int fieldRow   // property
        {
            get { return FieldRow; }   // get method
            set { FieldRow = value; }  // set method
        }

        public int fieldCol   // property
        {
            get { return FieldCol; }   // get method
            set { FieldCol = value; }  // set method
        }

        public Tuple<int, int> fieldToNum()
        {
            int r = this.fieldRow;
            int c = this.fieldCol;

            //convert from enum to int
            int r_nr = (int)r;
            int c_nr = (int)c;
            return new Tuple<int, int>(r_nr, c_nr);
        }

        
    }
}
