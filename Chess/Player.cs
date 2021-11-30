using OOPChessProject;
namespace Chess
{
    public class Player
    {
        public string Name;
        public Color Color;
        int SecondsLeftOnTheClock;

        public Player(string name, Color color, int secondsLeftOnTheClock)
        {
            Name = name;
            Color = color;
            SecondsLeftOnTheClock = secondsLeftOnTheClock;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
