using System;
using System.Collections.Generic;

using Chess;
using Chess.Pieces;


namespace OOPChessProject
{
    public class Controller
    {

        public static bool IsALegalMove(List<Move> lMoves, string s)
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

        public static ChessGame InitChessGame()
        {
            //Init Game with the Players
            //var cGame = new ChessGame("Eduard", "Benjamin");
            //string st = "r2qk2r / pppppppp / 8 / 8 / 8 / 8 / PPPPPPPP / RN2K2R";
            string st = "6k1/1pp1pppp/5n2/8/8/8/1PPPPPPP/R2QK2R";

            //string st = "rnbqkbnr/pppppppp/8/8/8/5N2/2PPPPPP/6K1";
            ChessGame cGame = new ChessGame(st, "Eduard", "Felix");
            return cGame;
        }
        public static void UpdateGameState(ChessGame cGame)
        {
            cGame.isCheck = cGame.CurrentChessBoard.IsChecked(Helperfunctions.ColorSwapper(cGame.CurrentPlayer.Color));
            
            if (cGame.isCheck)
            {
                cGame.GameState = GameState.Check;
                Console.Write("Check");
            }

            if (cGame.GameState == GameState.Check)
            {

                if (!cGame.CheckMitigationPossible())
                {
                    cGame.GameState = GameState.Checkmate;
                    Console.Write("Check+Mate");
                }
            }
            else //if there is no check we need to see if there is a stalemate which will result in a draw
            {
                if (!cGame.MovesAvailable())
                {
                    cGame.GameState = GameState.Draw;
                    Console.WriteLine("StaleMate");
                }
            }
        }
        public static void GameStateBoardToConsole(ChessGame cGame)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("_________________________________");
            Console.WriteLine("Iteration Nr." + cGame.TurnCounter);
            Console.WriteLine(cGame.CurrentChessBoard);
            Console.WriteLine(cGame.CurrentPlayer + "'s turn!");
            Console.WriteLine(cGame.CurrentPlayer.Color);
            Console.WriteLine("_________________________________");
        }
        public static bool isFieldValid(ChessGame cGame, Field of)
        {
            //if the colors are not the same we need to prompt again for input, because not the right piece was chosen.
            cGame.CurrentChessBoard.TryGetPieceFromField(of, out var pi);
            if (!cGame.IsPlayerAndPieceColorSame(cGame.CurrentPlayer, pi))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("WRONG COLOR OR FIELD IS EMPTY!!!");
                Console.ForegroundColor = ConsoleColor.Yellow;
                return false;
            }
            return true;
        }

        public static List<Move> getMovesForField(ChessGame cGame, Field of)
        {
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

            return listofMoves;
        }

        public MovementType getMoveTypeForDestinationField(List<Move> listOfMoves, Field destField)
        {
            var mtype = MovementType.Moving;
            foreach (var move in listOfMoves)
            {
                if (move.ToField.ToString() == destField.ToString())
                {
                    mtype = move.MovementType;
                }

            }

            return mtype;
        }

        public void MainGameLoop()
        {
            var cGame = InitChessGame();

            
            //Game Loop
            do
            {
                GameStateBoardToConsole(cGame);

                UpdateGameState(cGame); // Checks, Mate, Stalemate 
                Console.WriteLine("Enter Origin Field: [A-H][1-8]");
                string s = Console.ReadLine();
                
                var of = Helperfunctions.StringToField(s);
                if(!isFieldValid(cGame,of))
                {
                    continue;
                }
                var listofMoves = getMovesForField(cGame, of);
                Console.WriteLine("Enter Destination Field: [A-H][1-8]");
                s = Console.ReadLine();
                var df = Helperfunctions.StringToField(s);

                MovementType mtype;
                if (IsALegalMove(listofMoves, s))
                {
                    mtype = getMoveTypeForDestinationField(listofMoves, df);
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
                cGame.CurrentPlayer= (cGame.CurrentPlayer == cGame.Player1) ? cGame.Player2: cGame.Player1;
                cGame.TurnCounter++;
            } while (cGame.TurnCounter < 1000);
        }
    }
}
