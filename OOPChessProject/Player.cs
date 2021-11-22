using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPChessProject
{
    public class Player
    {
        public string Name;
        public Color Color;
        int SecondsLeftOnTheClock;
        //adding a matchhistory
        public Player(string name, Color color)
        {
            Name = name;
            Color = color;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
