using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32;
using OOPChessProject.Pieces;


namespace OOPChessProject
{
    class Controller
    {


        public Field stringToField(string s)
        {
            // String to Field Methode
            int c = (int)(col)Enum.Parse(typeof(col), s[0].ToString());
            int r = (int)(row)Enum.Parse(typeof(row), "_" + s[1]);
            Field of = new Field(r, c);
            return of;
        }

        public bool isALegalMove(List<Move> lMoves, string s)
        {
            foreach (var m in lMoves)
            {
                if (m.ToField.ToString() == s)
                {
                    return true;
                }
            }

            return false;
        }

        public void MainGameLoop()
        {
            //Init Game with the Players
            //var cGame = new ChessGame("Eduard", "Benjamin");
            string st = "r2qk2r / pppppppp / 8 / 8 / 8 / 8 / PPPPPPPP / RN2K2R";

            
            //string st = "rnbqkbnr/pppppppp/8/8/8/5N2/2PPPPPP/6K1";
            ChessGame cGame = new ChessGame(st, "Eduard", "Felix");
            
            cGame.GameState = gameState.Running;
            bool isCheck;


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

                //check if there is a check //change color because we need to iter of opponents pieces
                isCheck = cGame.currentChessBoard.isChecked(Helper.ColorSwapper(cGame.CurrentPlayer.Color));
                Console.WriteLine("Checked:");
                if (isCheck)
                {
                    cGame.GameState = gameState.Check;
                    Console.WriteLine("CHECK!");
                }

                if (cGame.GameState == gameState.Check)
                {

                    if (!cGame.CheckMitigationPossible())
                    {
                        Console.WriteLine("Game Over!");
                    }
                }
                else //if there is no check we need to see if there is a stalemate which will result in a draw
                {
                    if (!cGame.movesAvailable())
                    {
                        Console.WriteLine("Stalemate!");
                    }
                }




                #region Asking for 1st User Input
                //User Input --> Another While Loop
                Console.WriteLine("Enter Origin Field: [A-H][1-8]");
                string s = Console.ReadLine();
                var of = stringToField(s);
                #endregion

                #region Check for right color and if field is not empty
                //if the colors are not the same we need to prompt again for input, because not the right piece was chosen.
                Piece pi;
                bool wassuccess = cGame.currentChessBoard.TryGetPieceFromField(of, out pi);
                if (!cGame.isPlayerAndPieceColorSame(cGame.CurrentPlayer, pi ))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("WRONG COLOR OR FIELD IS EMPTY!!!");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    continue;
                }
                #endregion

                
                //Lookup the possible Moves
                Piece p; 
                cGame.currentChessBoard.TryGetPieceFromField(of, out p);
                var listofMoves = p.getPossibleMoves(cGame.currentChessBoard);
                listofMoves = cGame.FilterMove(MovementType.defending, listofMoves);


                if (p.PrintRepresentation == "KI")
                {

                    //Check if castling is possible

                    listofMoves = cGame.FilterKingMoves(listofMoves, (King)p);
                    var canWeCastleShort = cGame.currentChessBoard.CastleShort(cGame.CurrentPlayer.Color);
                    var canWeCastleLong = cGame.currentChessBoard.CastleLong(cGame.CurrentPlayer.Color);
                    if (canWeCastleLong.Item1)
                    {
                        listofMoves.Add(canWeCastleLong.Item2);
                    }

                    if (canWeCastleShort.Item1)
                    {
                        listofMoves.Add(canWeCastleShort.Item2);
                    }

                }


                foreach (var f in listofMoves)
                {
                    Console.WriteLine(f);
                }


                //Check if there is a Piece and the correct color
                //Get List Of Possible Moves for that Piece
                //cGame.currentChessBoard.GetPieceFromField(Field f);

                Console.WriteLine("Enter Destination Field: [A-H][1-8]");
                s = Console.ReadLine();
                var df = stringToField(s);
                MovementType mtype = MovementType.moving;

                if (isALegalMove(listofMoves, s))
                {
                    
                    foreach (var move in listofMoves)
                    {
                        if (move.ToField.ToString() == df.ToString())
                        {
                            mtype = move.movementType;
                        }
                    }
                }
                else
                {
                    //Berührt geführt not implemented
                    Console.WriteLine("ILLEGAL MOVE!");
                    continue;
                }
                





                //Move gets Performed
                cGame.currentChessBoard.MovePiece(of, df, mtype, cGame.TurnCounter);

                //After the move check whether this piece now attacks the enemy King
                
                cGame.currentChessBoard.removeSomeGhosts(cGame.TurnCounter);
                //cGame.currentChessBoard
                //Change current Player
                cGame.CurrentPlayer= (cGame.CurrentPlayer == cGame.Player1) ? cGame.Player2: cGame.Player1;
                cGame.TurnCounter++;



            } while (true);


        }


    }
}
