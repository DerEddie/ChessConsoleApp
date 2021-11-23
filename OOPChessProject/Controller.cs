using System;


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

        /*
        public bool isKinginCheck(List<Piece> enemyPieces,ChessBoard currentChessBoard)
        {
            foreach (var ep in enemyPieces)
            {
                var movesList = ep.getPossibleMoves(currentChessBoard);
                var evalRes = IsKingOnMoveList(movesList);
                if (evalRes.Item1)
                {
                    GameState = gameState.Check;



                    Console.WriteLine(evalRes.Item2);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Check!!!");
                    Console.ForegroundColor = ConsoleColor.Yellow;

                }
            }
        }
        */

        public void MainGameLoop()
        {
            //Init Game with the Players
            var cGame = new ChessGame("Eduard", "Benjamin");
 

            

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

                //check if there is a check
                //see if king is on controlledFields.
                //GetControlledFields

                //Check if there is a check.
                Console.WriteLine("Did the King get checked?");

                var enemyPieces = cGame.currentChessBoard.getAllPiecesOfColor(Helper.ColorSwapper(cGame.CurrentPlayer.Color));


                //Check if the check can be mitigated
                var OwnPieces = cGame.currentChessBoard.getAllPiecesOfColor(cGame.CurrentPlayer.Color);

                //Create Copy of the Board
                //Perform the the move and then check if the check is still there. Once Move was found we break out
                //var boardCopy 



                #region Asking for 1st User Input
                //User Input --> Another While Loop
                Console.WriteLine("Enter Origin Field: [A-H][1-8]");
                string s = Console.ReadLine();
                var of = stringToField(s);
                #endregion

                #region Check for right color and if field is not empty
                //if the colors are not the same we need to prompt again for input, because not the right piece was chosen.
                if (!cGame.isPlayerAndPieceColorSame(cGame.CurrentPlayer, cGame.currentChessBoard.GetPieceFromField(of)))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("WRONG COLOR OR FIELD IS EMPTY!!!");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    continue;
                }
                #endregion

                

                

                
                //Lookup the possible Moves
                var p = cGame.currentChessBoard.GetPieceFromField(of);
                var l = p.getPossibleMoves(cGame.currentChessBoard);

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
                cGame.currentChessBoard.MovePiece(of, df,cGame);

                //After the move check whether this piece now attacks the enemy King
                

                

                
                


                

                //Change current Player
                cGame.CurrentPlayer= (cGame.CurrentPlayer == cGame.Player1) ? cGame.Player2: cGame.Player1;

                cGame.TurnCounter++;

            } while (true);


        }


    }
}
