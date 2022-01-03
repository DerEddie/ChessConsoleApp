using Chess.Pieces;

namespace Chess
{
    public class Player
    {
        private readonly string m_Name;
        public readonly Color Color;
        public int TimeLeftInSeconds;
        public Player(string playerMName, Color color, int timeLeftInSeconds)
        {
            this.m_Name = playerMName;
            this.Color = color;
            this.TimeLeftInSeconds = timeLeftInSeconds;
        }
        public override string ToString()
        {
            return this.m_Name;
        }
    }
}
