using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOPChessProject.Pieces;

namespace OOPChessProject
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
