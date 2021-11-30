namespace OOPChessProject
{
    internal class Program
    {
        /* Mistakes were made:        
         -Because of the pawn where capturing and moving is different a data type which could distinguish would be useful...
        Fazit:
        -Vorher über Sepzialfälle Gedanken machen...!                  
         */

        public static void Main(string[] args)
        {
            Controller c = new Controller();
            c.MainGameLoop();
        }
    }
}
