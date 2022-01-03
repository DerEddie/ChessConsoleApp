using System;
using Chess.Pieces;
namespace Chess
{
    public static class HelperFunctions
    {
        public static Color OppositeColor(Color c)
        {
            return c == Color.White ? Color.Black : Color.White;
        }
        public static Field StringToField(string s)
        {
            // example: Input: "E1", returns Field-Instance
            var c = (int)(Col)Enum.Parse(typeof(Col), s[0].ToString());
            var r = (int)(Row)Enum.Parse(typeof(Row), "_" + s[1]);
            var of = new Field(r, c);
            return of;
        }
    }
    

}
