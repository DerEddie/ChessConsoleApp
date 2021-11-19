using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPChessProject
{
    class Controller
    {
        public Field stringToField(string s)
        {
            // String to Field Methode
            col c = (col)Enum.Parse(typeof(col), s[0].ToString());
            row r = (row)Enum.Parse(typeof(row), "_" + s[1]);
            Field of = new Field(r, c);
            return of;
        }



        public void Doit()
        {

            var cGame = new ChessGame("Eduard", "Benjamin");
            

            //var kFieldvPiece = new Dictionary<Field, Piece>();

            //Game Loop
            do
            {
                #region Printing the board
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("_________________________________");
                Console.WriteLine("Iteration Nr." + cGame.TurnCounter);
                Console.WriteLine(cGame.currentChessBoard);
                Console.WriteLine(cGame.CurrentPlayer + "'s turn!");
                Console.WriteLine(cGame.CurrentPlayer.Color);
                Console.WriteLine("_________________________________");
                #endregion region MyClass definition  

                //User Input --> Another While Loop
                Console.WriteLine("Enter Origin Field: [A-H][1-8]");
                string s = Console.ReadLine();
                var of = stringToField(s);

                //if the colors are not the same we need to prompt again for input, because not the right piece was chosen.
                if (!cGame.isPlayerAndPieceColorSame(cGame.CurrentPlayer, cGame.currentChessBoard.GetPieceFromField(of)))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("WRONG COLOR OR FIELD IS EMPTY!!!");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    continue;
                }

                //Lookup the possible Moves
                var p = cGame.currentChessBoard.GetPieceFromField(of);
                Console.WriteLine("FieldState" + p.CurrField);
                var l = p.getPossibleMoves(cGame.currentChessBoard, false);

                foreach (var f in l)
                {
                    Console.WriteLine(f);
                }


                //Check if there is a Piece and the correct color
                //Get List Of Possible Moves for that Piece
                //cGame.currentChessBoard.GetPieceFromField(Field f);


                Console.WriteLine("Enter Destination Field: [A-H][1-8]");
                s = Console.ReadLine();
                var df = stringToField(s);

                //Move gets Performed
                cGame.currentChessBoard.MovePiece(of, df);

                //After the move check whether this piece now attacks the enemy King
                var listAfterMove = p.getPossibleMoves(cGame.currentChessBoard, false);

                Console.WriteLine("Did the King get checked?");

                
                var r = cGame.IsKingOnMoveList(listAfterMove);
                if (r.Item1)
                {
                    Console.WriteLine(r.Item2);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Check!!!");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }

                

                //Change current Player
                cGame.CurrentPlayer= (cGame.CurrentPlayer == cGame.Player1) ? cGame.Player2: cGame.Player1;

                cGame.TurnCounter++;

            } while (true);


        }


    }
}
