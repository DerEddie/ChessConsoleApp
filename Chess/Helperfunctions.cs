using System;
using Chess.Pieces;

namespace Chess
{
    public static class HelperFunctions
    {
        public static Color ColorSwapper(Color c)
        {
            if (c == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }
        public static Field StringToField(string s)
        {
            // String to Field Methode
            var c = (int)(Col)Enum.Parse(typeof(Col), s[0].ToString());
            var r = (int)(Row)Enum.Parse(typeof(Row), "_" + s[1]);
            var of = new Field(r, c);
            return of;
        }
    }





}
