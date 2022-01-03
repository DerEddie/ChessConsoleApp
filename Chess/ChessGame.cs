using System;
using System.Collections.Generic;
using Chess.Pieces;


namespace Chess
{
    public enum GameState
    {
        Running,
        Check,
        Checkmate,
        Draw,
    }

    public class ChessGame
    {
        //carries basic information and information for the flow control
        public readonly ChessBoard CurrentChessBoard;
        public readonly List<Tuple<int,Move,ChessBoard>> MovesHistory =  new List<Tuple<int, Move, ChessBoard>>();
        public readonly Player Player1;
        public readonly Player Player2;
        public Player CurrentPlayer;
        public GameState GameState;
        public int TurnCounter;
        public bool IfFirstInputTrueElseFalse = true;

        //new Fields
        public bool IsCheck = false;
        public ChessGame(string fenNotationString, string player1Name, string player2Name)
        {
            Player1 = new Player(player1Name, Color.White, 600);
            Player2 = new Player(player2Name, Color.Black, 600);


            //Create the Board
            CurrentChessBoard = new ChessBoard("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR");

            //set white as starting Player
            CurrentPlayer = Player1;

            //set Move Counter to Zero
            TurnCounter = 0;

            //gameState
            GameState = GameState.Running;

            //init Board
            CurrentChessBoard = new ChessBoard(fenNotationString);
        }
        public ChessGame(string player1Name, string player2Name)
        {

            //Create Players
            Player1 = new Player(player1Name, Color.White, 600);
            Player2 = new Player(player2Name, Color.Black, 600);

            //Create the Board
            CurrentChessBoard = new ChessBoard("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR");

            //set white as starting Player
            CurrentPlayer = Player1;

            //set Move Counter to Zero
            TurnCounter = 0;

            //gameState
            GameState = GameState.Running;

            //
        }
        //Implement here because attribute here
        //Check if Color Player == Color Piece
        public static bool IsPlayerAndPieceColorSame(Player player, Piece piece)
        {
            if (piece == null)
            {
                return false;
            }
            Console.WriteLine(piece.PieceColor);
            bool colorSame = player.Color == piece.PieceColor;
            return colorSame;
        }
        public List<Move> FilterMoveWhichExposeCheck(List<Move> moves, Color currentPlayerColor)
        {
            var legalMoves = new List<Move>();
            //filtered moves list, return list, reverse logic add ok moves
            foreach (var m in moves)
            {
                var copyBoard = new ChessBoard(this.CurrentChessBoard);
                copyBoard.MovePiece(m.FromField, m.ToField, MovementType.Moving, 0);
                var enemyColor = HelperFunctions.OppositeColor(currentPlayerColor);
                if (!copyBoard.IsCheckedByColor(enemyColor))
                {
                    legalMoves.Add(m);
                }
            }
            return legalMoves;
        }
        //Check Whether a move results in a check
        public static List<Move> FilterMove(MovementType type, List<Move> moveList)
        {
            List<Move> filteredList = new List<Move>();
 
            foreach (var m in moveList)
            {
                if (m.MovementType != type)
                {
                    filteredList.Add(m);
                }
            }
            return filteredList;
        }
        public List<Move> FilterKingMoves(List<Move> moveList, King king)
        {
            Color c = king.PieceColor;
            Color oppColor = HelperFunctions.OppositeColor(c);
            var pieces = this.CurrentChessBoard.GetAllPiecesOfColor(oppColor);

            foreach (var p in pieces)
            {
                var enemyMoves = p.GetPossibleMoves(this.CurrentChessBoard);
                enemyMoves = FilterMove(MovementType.DoubleStep, enemyMoves);
                enemyMoves = FilterMove(MovementType.MovingPeaceful, enemyMoves);


                foreach (var mEnemy in enemyMoves)
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
            var pieces = this.CurrentChessBoard.GetAllPiecesOfColor(this.CurrentPlayer.Color);
            foreach (var pp in pieces)
            {
                var moves = pp.GetPossibleMoves(this.CurrentChessBoard);
                moves = FilterMove(MovementType.Defending, moves);
                foreach (var m in moves)
                {
                    //Iterate over all possible moves and check whether a move stops the check.
                    var copyBoard = new ChessBoard(this.CurrentChessBoard);
                    copyBoard.MovePiece(m.FromField, m.ToField, MovementType.Moving, 0);
                    if (copyBoard.IsCheckedByColor(HelperFunctions.OppositeColor(this.CurrentPlayer.Color))) continue;
                    Console.WriteLine(copyBoard);
                    return true;
                }
            }
            return false;
        }

        public bool MovesAvailable()
        {
            bool areMovesAvail = false;
            var pieces = this.CurrentChessBoard.GetAllPiecesOfColor(this.CurrentPlayer.Color);

            foreach (var pp in pieces)
            {
                var moves = pp.GetPossibleMoves(this.CurrentChessBoard);
                moves = FilterMove(MovementType.Defending, moves);
                if (moves.Count > 0)
                {
                    areMovesAvail = true;
                }
            }

            return areMovesAvail;
        }

        public void AddMoveToMoveList(Move m)
        {
            var tuple = new Tuple<int, Move, ChessBoard>(this.TurnCounter, m, this.CurrentChessBoard);
            this.MovesHistory.Add(tuple);
        }
    }
}
