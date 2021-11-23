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


        public Dictionary<string, Piece> deepCopy(Dictionary<string, Piece> mydict)
        {
            Dictionary<string, Piece> clone = new Dictionary<string, Piece>();
            foreach (var kvpair in mydict)
            {

                //TODO implement the dictionary copy correct: currently Problem to copy a piece: do really need a CopyCOnstructor for every piece???
                //clone.Add(kvpair.Key, new Piece);
            }

            return mydict;
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
