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


        //Overloading the instructor with a fen notation option
        //FEN NOTATION: rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1
        //TODO b KQkq e3 0 1 not implemented yet...
        // lower case letter = black, upper case letter = white.
        //   "/" new row- numbers are empty fields
        //   KQ = King-&Queenside-Castling is possible
        //   kq = same for black
        //   en passant fields..
        public ChessGame(string fenNotationString)
        {
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



        //Check Whether a move results in a check


        //true if a mitigating move was found, False if not
        public bool ChessMitigationPossible()
        {
            bool mitigationFound = false;

            var pieces = this.currentChessBoard.getAllPiecesOfColor(this.CurrentPlayer.Color);
            foreach (var pp in pieces)
            {
                var moves = pp.getPossibleMoves(this.currentChessBoard);
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
                if (moves.Count > 0)
                {
                    areMovesAvail = true;
                }
            }

            return areMovesAvail;
        }



    }
}
