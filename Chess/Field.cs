using System;

namespace Chess
{
    //make public if other classes should use it.
    public enum Row
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

// (int)row._1, (row)0
    public enum Col
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
        public readonly int FieldRow; //row = 0 => 1
        public readonly int FieldCol; //col = 0 => A
        public Field(string s)
        {
            
            
            var number = int.Parse(s[1].ToString())-1;

            FieldRow = number;
            FieldCol = (int) (Col) Enum.Parse(typeof(Col), s[0].ToString());
        }
        
        //creating a constructor for a field instance
        public Field(int aFieldRow, int aFieldCol)
        {
            FieldRow = aFieldRow;
            FieldCol = aFieldCol;          
        }


        public override string ToString()
        {
            return $"{(Col) this.FieldCol}{this.FieldRow + 1}";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            Field f = obj as Field;

            return f != null && ((f.FieldCol == this.FieldCol) && (f.FieldRow == this.FieldRow));


        }

        protected bool Equals(Field other)
        {
            return FieldRow == other.FieldRow && FieldCol == other.FieldCol;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (FieldRow * 397) ^ FieldCol;
            }
        }

    }
}