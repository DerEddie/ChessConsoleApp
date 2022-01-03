using Chess.Pieces;

namespace Chess
{
    public class Player
    {
        public string Name;
        public Color Color;
        public int TimeLeftInSeconds;
        public Player(string playerName, Color color, int timeLeftInSeconds)
        {
            this.Name = playerName;
            this.Color = color;
            this.TimeLeftInSeconds = timeLeftInSeconds;
        }
        public override string ToString()
        {
            return this.Name;
        }
    }
}
