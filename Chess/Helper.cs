using Chess.Pieces;

namespace Chess
{
    public class Helper
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
    }
}
