using System;
using Chess;
using static Chess.Controller;

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
            void MainGameLoop()
            {
                var cGame = InitChessGame();
                //Game Loop
                do
                {
                    GameStateBoardToConsole(cGame);
                    UpdateGameState(cGame); // Checks, Mate, Stalemate 
                    Console.WriteLine("Enter Origin Field: [A-H][1-8]");
                    string s = Console.ReadLine();
                    var of = HelperFunctions.StringToField(s);
                    if(!IsFieldValid(cGame,of))
                    {
                        continue;
                    }
                    var listofMoves = GetMovesForField(cGame, of);
                    Console.WriteLine("Enter Destination Field: [A-H][1-8]");
                    s = Console.ReadLine();
                    var df = HelperFunctions.StringToField(s);
                    MovementType mtype;
                    if (IsALegalMove(listofMoves, s))
                    {
                        mtype = Controller.GetMoveTypeForDestinationField(listofMoves, df);
                    }
                    else
                    {
                        //Berührt geführt not implemented
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ILLEGAL MOVE!");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        continue;
                    }
                    cGame.CurrentChessBoard.MovePiece(of, df, mtype, cGame.TurnCounter);
                    cGame.CurrentChessBoard.RemoveSomeGhosts(cGame.TurnCounter);
                    //cGame.currentChessBoard
                    //Change current Player
                    SwitchPlayerTurn(cGame);
                    cGame.TurnCounter++;
                } while (cGame.TurnCounter < 1000);
            }
        }
    }
}
