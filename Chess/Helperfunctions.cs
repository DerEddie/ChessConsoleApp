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
            int c = (int)(Col)Enum.Parse(typeof(Col), s[0].ToString());
            int r = (int)(Row)Enum.Parse(typeof(Row), "_" + s[1]);
            Field of = new Field(r, c);
            return of;
        }
    }

}
