using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOPChessProject;

namespace OOPChessProject
{
class Program
    {
        /* Mistakes were made:
        
         -Because of the pawn where capturing and moving is different a data type which could distinguish would be useful...

        Fazit:
        -Vorher über Sepzialfälle Gedanken machen...!
         


         
         */

        public static void Main(string[] args)
        {
            //Array
            var rc_offset = new int[,] { { -1, 0 } };
            Console.WriteLine(rc_offset[0,0]);


            Controller c = new Controller();
            c.MainGameLoop();
            


        }
    }
}
