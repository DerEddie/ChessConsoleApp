using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOPChessProject.Pieces;

namespace OOPChessProject
{
    public enum gameState
    {
        Running,
        Check,
        Checkmate,
        Draw
    }

    public class ChessGame
    {
        //carries basic information and information for the flow control
        public ChessBoard currentChessBoard;
        public List<Move> movesHistory;
        public Player Player1;
        public Player Player2;
        public Player CurrentPlayer;
        public gameState GameState;
        public int TurnCounter;
        




        public ChessGame(string player1name, string player2name)
        {

            //Create Players
            Player1 = new Player(player1name, Color.White);
            Player2 = new Player(player2name, Color.Black);

            //Create the Board
            currentChessBoard = new ChessBoard();

            //set white as starting Player
            CurrentPlayer = Player1;

            //set Move Counter to Zero
            TurnCounter = 0;

            //gameState
            GameState = gameState.Running;

            //
            movesHistory = new List<Move>();
        }

        //Implement here because attribute here
        //Check if Color Player == Color Piece
        public bool isPlayerAndPieceColorSame(Player player, Piece piece)
        {
            if (piece == null)
            {
                return false;
            }
            bool colorSame = player.Color == piece.PieceColor;
            return colorSame;
        }

        //Public because i need this function to determine the restricted fields



    }
}
