using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Configuration;
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

        
        public ChessGame(string fenNotationString, string player1name, string player2name)
        {
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

            var dict = new Dictionary<string, Piece>();


            int row = 7;
            int col = 0;
            Color color;
            Piece p; //= new Pawn(new Field(0,0), Color.White);
            foreach (var c in fenNotationString)
            {
                if (Char.IsLower(c))
                {
                     color = Color.Black;
                }

                else
                {
                     color = Color.White;
                }

                var lowerOfChar = Char.ToLower(c);
                Field f = new Field(row, col);

                
                if (Char.IsDigit(lowerOfChar))
                {

                    col = col + int.Parse(c.ToString());
                    continue;
                }
                if (Char.IsLetter(lowerOfChar))
                {

                    //switch
                    switch (lowerOfChar)
                    {
                        case 'r':
                            p = new Rook(f, color);
                            break;
                        case 'n':
                            p = new Knight(f, color);
                            break;
                        case 'b':
                            p = new Bishop(f, color);
                            break;
                        case 'q':
                            p = new Queen(f, color);
                            break;
                        case 'k':
                            p = new King(f, color);
                            break;
                        case 'p':
                            p = new Pawn(f, color);
                            break;
                        default:
                            p = new Pawn(f, color);
                            break;
                    }
                    dict.Add(f.ToString(), p);
                    col++;
                }
                else if (lowerOfChar == char.Parse("/"))
                {
                    row--;
                    col = 0;
                }


                

             }
            currentChessBoard = new ChessBoard(dict);
        }



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

        public List<Move> FilterMoveWhichExposeCheck(List<Move> moves, Color currentplayerColor)
        {
            List<Move> legalMoves = new List<Move>();
            //filtered moves list, return list, reverse logic add ok moves
            foreach (var m in moves)
            {
                ChessBoard copyBoard = new ChessBoard(this.currentChessBoard);
                copyBoard.MovePiece(m.FromField, m.ToField, MovementType.moving, 0);
                //TODO is checked doesnt work properly it doenst care for the kings color
                Color enemyColor = Helper.ColorSwapper(currentplayerColor);
                if (!copyBoard.isChecked(enemyColor))
                {
                    legalMoves.Add(m);
                }
            }
            return legalMoves;
        }


        //Check Whether a move results in a check
        public List<Move> FilterMove(MovementType type, List<Move> MoveList)
        {
            List<Move> filteredList = new List<Move>();
 
            foreach (var m in MoveList)
            {
                if (m.movementType != type)
                {
                    filteredList.Add(m);
                }
            }
            return filteredList;
        }


        public List<Move> FilterKingMoves(List<Move> moveList, King king)
        {
            Color c = king.PieceColor;
            Color oppColor = Helper.ColorSwapper(c);
            var pieces = this.currentChessBoard.getAllPiecesOfColor(oppColor);

            foreach (var p in pieces)
            {
                var enemyPmoves = p.getPossibleMoves(this.currentChessBoard);
                enemyPmoves = FilterMove(MovementType.doubleStep, enemyPmoves);
                enemyPmoves = FilterMove(MovementType.movingPeaceful, enemyPmoves);


                foreach (var mEnemy in enemyPmoves)
                {
                    for (int i = moveList.Count-1; i >= 0; i--)
                    {
                        if (moveList[i].ToField.ToString() == mEnemy.ToField.ToString())
                        {
                            moveList.RemoveAt(i);
                        }
                    }
                }
            }

            return moveList;
        }


        public bool CheckMitigationPossible()
        {
            bool mitigationFound = false;

            var pieces = this.currentChessBoard.getAllPiecesOfColor(this.CurrentPlayer.Color);
            foreach (var pp in pieces)
            {
                var moves = pp.getPossibleMoves(this.currentChessBoard);
                moves = FilterMove(MovementType.defending, moves);

                foreach (var m in moves)
                {
                    ChessBoard copyBoard = new ChessBoard(this.currentChessBoard);
                    copyBoard.MovePiece(m.FromField, m.ToField, MovementType.moving, 0);
                    if (!copyBoard.isChecked(Helper.ColorSwapper(this.CurrentPlayer.Color)))
                    {
                        mitigationFound = true;
                        break;
                    }

                }

            }

            return mitigationFound;
        }

        //TODO make sure available moves are actually available filtering of moves needed...if check for example
        public bool movesAvailable()
        {
            bool areMovesAvail = false;
            var pieces = this.currentChessBoard.getAllPiecesOfColor(this.CurrentPlayer.Color);

            foreach (var pp in pieces)
            {
                var moves = pp.getPossibleMoves(this.currentChessBoard);
                moves = FilterMove(MovementType.defending, moves);
                if (moves.Count > 0)
                {
                    areMovesAvail = true;
                }
            }

            return areMovesAvail;
        }



    }
}
