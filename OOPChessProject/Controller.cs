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

        public static bool TryGetMoveFromListBasedOnDestinationField(List<Move> moveList, string toField, out Move move)
        {
            foreach (var m in moveList)
            {
                if (m.ToField.ToString() == toField)
                {
                    move = m;
                    return true;
                }
            }

            move = null;
            return false;
        }
        public static ChessGame InitChessGame()
        {
            //Init Game with the Players
            //var cGame = new ChessGame("Eduard", "Benjamin");
            //string st = "r2qk2r / pppppppp / 8 / 8 / 8 / 8 / PPPPPPPP / RN2K2R";
            //string st = "6k1/1pp1pppp/5n2/8/8/8/1PPPPPPP/R2QK2R";

            string st = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";
            ChessGame cGame = new ChessGame(st, "Eduard", "Felix");
            return cGame;
        }
        public static void UpdateGameState(ChessGame cGame)
        {
            cGame.IsCheck = cGame.CurrentChessBoard.IsChecked(HelperFunctions.ColorSwapper(cGame.CurrentPlayer.Color));
            if (cGame.IsCheck)
            {
                cGame.GameState = GameState.Check;
                Console.Write("Check");
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
                else
                {
                    cGame.GameState = GameState.Running;
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
        public static bool IsFieldValid(ChessGame cGame, Field of)
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
        public static List<Move> GetMovesForField(ChessGame cGame, Field of)
        {
            Piece p;
            cGame.CurrentChessBoard.TryGetPieceFromField(of, out p);
            var listofMoves = p.GetPossibleMoves(cGame.CurrentChessBoard);
            listofMoves = cGame.FilterMove(MovementType.Defending, listofMoves);

            if (p.PrintRepresentation == "KI")
            {
                //Check if castling is possible
                listofMoves = cGame.FilterKingMoves(listofMoves, (King)p);
                var canWeCastleShort = cGame.CurrentChessBoard.TryCastleShort(out var move1, p);
                var canWeCastleLong = cGame.CurrentChessBoard.TryCastleLong(cGame.CurrentPlayer.Color, out var move2);
                if(canWeCastleShort)
                {
                    listofMoves.Add(move1);
                }
                if(canWeCastleLong)
                {
                    listofMoves.Add(move2);
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

        public MovementType GetMoveTypeForDestinationField(List<Move> listOfMoves, Field destField)
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

        public static void SwitchPlayerTurn(ChessGame cGame)
        {
            cGame.CurrentPlayer = (cGame.CurrentPlayer == cGame.Player1) ? cGame.Player2 : cGame.Player1;
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
                    mtype = GetMoveTypeForDestinationField(listofMoves, df);
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
