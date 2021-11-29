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


        public Field(string s)
        {
            
            
            int Number = int.Parse(s[1].ToString())-1;

            FieldRow = Number;
            FieldCol = (int) (col) Enum.Parse(typeof(col), s[0].ToString());
        }


        //creating a constructor for a field instance
        public Field(int aFieldRow, int aFieldCol)
        {
            FieldRow = aFieldRow;
            FieldCol = aFieldCol;          
        }


        public override string ToString()
        {
            return String.Format("{1}{0}", this.FieldRow+1 ,  (col)this.FieldCol);
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


    }
}
