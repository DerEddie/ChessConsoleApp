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
        

    public static void Main(string[] args)
        {
            //Array
            var rc_offset = new int[,] { { -1, 0 } };
            Console.WriteLine(rc_offset[0,0]);


            Controller c = new Controller();
            c.Doit();
            


        }
    }
}
