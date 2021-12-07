using Chess.Pieces;

namespace Chess
{
    public class Player
    {
        public string Name;
        public Color Color;

        public Player(string playerName, Color color)
        {
            this.Name = playerName;
            this.Color = color;
        }


        public override string ToString()
        {
            return this.Name;
        }
    }
}
