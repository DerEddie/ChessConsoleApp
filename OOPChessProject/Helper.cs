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
    //internal Dictionary<string, Piece> clone = new Dictionary<string, Piece>();
            //foreach(KeyValuePair<string, Piece> pair in mydict)






    /*
        public IDictionary<TKey, TValue> Clone()
        {
            CloneableDictionary<TKey, TValue> clone = new CloneableDictionary<TKey, TValue>();

            foreach (KeyValuePair<TKey, TValue> pair in this)
            {
                clone.Add(pair.Key, (TValue) pair.Value.Clone());
            }

            return clone;
        }
    */
}
