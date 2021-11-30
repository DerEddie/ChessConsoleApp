using System;
using System.Collections.Generic;

using Chess;

using OOPChessProject.Pieces;


namespace OOPChessProject
{
    class Controller
    {
        public Field StringToField(string s)
        {
            // String to Field Methode
            int c = (int)(Col)Enum.Parse(typeof(Col), s[0].ToString());
            int r = (int)(Row)Enum.Parse(typeof(Row), "_" + s[1]);
            Field of = new Field(r, c);
            return of;
        }
        public bool IsALegalMove(List<Move> lMoves, string s)
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
            //string st = "r2qk2r / pppppppp / 8 / 8 / 8 / 8 / PPPPPPPP / RN2K2R";
            string st = "rnbqkbnr/pppppppp/8/8/Q7/8/PP2PPPP/RN2KBNR";

        //string st = "rnbqkbnr/pppppppp/8/8/8/5N2/2PPPPPP/6K1";
        ChessGame cGame = new ChessGame(st, "Eduard", "Felix");
            
            cGame.GameState = GameState.Running;
            bool isCheck;


            //Game Loop
            do
            {
                #region Printing the board
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("_________________________________");
                Console.WriteLine("Iteration Nr." + cGame.TurnCounter);
                Console.WriteLine(cGame.CurrentChessBoard);
                Console.WriteLine(cGame.CurrentPlayer + "'s turn!");
                Console.WriteLine(cGame.CurrentPlayer.Color);
                Console.WriteLine("_________________________________");
                #endregion region MyClass definition
                #region  Checking for Check, Mate, or StaleMate
                //check if there is a check //change color because we need to iter of opponents pieces
                isCheck = cGame.CurrentChessBoard.IsChecked(Helper.ColorSwapper(cGame.CurrentPlayer.Color));
                Console.WriteLine("Checked:");
                if (isCheck)
                {
                    cGame.GameState = GameState.Check;
                    Console.WriteLine("CHECK!");
                }

                if (cGame.GameState == GameState.Check)
                {

                    if (!cGame.CheckMitigationPossible())
                    {
                        Console.WriteLine("Game Over!");
                    }
                }
                else //if there is no check we need to see if there is a stalemate which will result in a draw
                {
                    if (!cGame.MovesAvailable())
                    {
                        Console.WriteLine("Stalemate!");
                    }
                }

                #endregion
                #region Asking for 1st User Input
                //User Input --> Another While Loop
                Console.WriteLine("Enter Origin Field: [A-H][1-8]");
                string s = Console.ReadLine();
                var of = StringToField(s);
                #endregion
                #region Check for right color and if field is not empty
                //if the colors are not the same we need to prompt again for input, because not the right piece was chosen.
                cGame.CurrentChessBoard.TryGetPieceFromField(of, out var pi);
                if (!cGame.IsPlayerAndPieceColorSame(cGame.CurrentPlayer, pi ))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("WRONG COLOR OR FIELD IS EMPTY!!!");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    continue;
                }
                #endregion
                //Lookup the possible Moves
                Piece p; 

                cGame.CurrentChessBoard.TryGetPieceFromField(of, out p);
                var listofMoves = p.getPossibleMoves(cGame.CurrentChessBoard);
                listofMoves = cGame.FilterMove(MovementType.Defending, listofMoves);
                


                if (p.PrintRepresentation == "KI")
                {
                    //Check if castling is possible
                    listofMoves = cGame.FilterKingMoves(listofMoves, (King)p);
                    var canWeCastleShort = cGame.CurrentChessBoard.CastleShort(cGame.CurrentPlayer.Color);
                    var canWeCastleLong = cGame.CurrentChessBoard.CastleLong(cGame.CurrentPlayer.Color);
                    if (canWeCastleLong.Item1)
                    {
                        listofMoves.Add(canWeCastleLong.Item2);
                    }
                    if (canWeCastleShort.Item1)
                    {
                        listofMoves.Add(canWeCastleShort.Item2);
                    }
                }
                else
                {
                    //its an other piece than the king, we need to check whether the move might expose our King
                    listofMoves = cGame.FilterMoveWhichExposeCheck(listofMoves, cGame.CurrentPlayer.Color);
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
                var df = StringToField(s);
                MovementType mtype = MovementType.Moving;

                if (IsALegalMove(listofMoves, s))
                {
                    
                    foreach (var move in listofMoves)
                    {
                        if (move.ToField.ToString() == df.ToString())
                        {
                            mtype = move.MovementType;
                        }
                    }
                }
                else
                {
                    //Berührt geführt not implemented
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ILLEGAL MOVE!");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    continue;
                }
                





                //Move gets Performed
                cGame.CurrentChessBoard.MovePiece(of, df, mtype, cGame.TurnCounter);

                //After the move check whether this piece now attacks the enemy King
                
                cGame.CurrentChessBoard.RemoveSomeGhosts(cGame.TurnCounter);
                //cGame.currentChessBoard
                //Change current Player
                cGame.CurrentPlayer= (cGame.CurrentPlayer == cGame.Player1) ? cGame.Player2: cGame.Player1;
                cGame.TurnCounter++;

            } while (cGame.TurnCounter < 1000);
        }
    }
}
