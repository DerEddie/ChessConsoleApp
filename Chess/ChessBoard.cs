using System;
using System.Collections.Generic;
using Chess.Pieces;
namespace Chess
{
    public class ChessBoard
    {
        //field
        public Dictionary<string, Piece> KeyFieldValuePiece;
        public ChessBoard(ChessBoard otherBoard)
        {
            var copyDict = new Dictionary<string, Piece>();
            foreach (var kvpPair in otherBoard.KeyFieldValuePiece)
            {
                //clone -> returns object -> to Piece
                copyDict.Add(kvpPair.Key, (Piece)kvpPair.Value.Clone());
            }
            KeyFieldValuePiece = copyDict;
        }
        public ChessBoard(Dictionary<string, Piece> dict)
        {
            KeyFieldValuePiece = dict;
        }
        //A Constructor which sets up the Game Board
        public ChessBoard()
        {
            //create a dict from store the pieces as VALUE and field as KEY
            KeyFieldValuePiece = new Dictionary<string, Piece>();
            //iterate over all field combinations
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Field f = new Field(r, c);
                    //Piece p;
                    if(r == 0)
                    {
                        if(c == 0 | c == 7)
                        {
                            Rook p = new Rook(f, Color.White);
                            KeyFieldValuePiece.Add(f.ToString(), p);

                        }
                        else if(c == 1 | c == 6)
                        {
                            Knight p = new Knight(f, Color.White);
                            KeyFieldValuePiece.Add(f.ToString(), p);
                        }
                        else if(c == 2| c == 5)
                        {
                            Bishop p = new Bishop(f, Color.White);
                            KeyFieldValuePiece.Add(f.ToString(), p);
                        }
                        else if(c == 3)
                        {
                            //Queen's Row
                            Queen p = new Queen(f, Color.White);
                            KeyFieldValuePiece.Add(f.ToString(), p);
                        }
                        else if(c  == 4)
                        {
                            //King's Row
                            King p = new King(f, Color.White);
                            KeyFieldValuePiece.Add(f.ToString(), p);
                        }
                    }
                    if (r == 7)
                    {
                        if (c == 0 | c == 7)
                        {
                            Rook p = new Rook(f, Color.Black);
                            KeyFieldValuePiece.Add(f.ToString(), p);
                        }
                        else if (c == 1 | c == 6)
                        {
                            Knight p = new Knight(f, Color.Black);
                            KeyFieldValuePiece.Add(f.ToString(), p);
                        }
                        else if (c == 2 | c == 5)
                        {
                            Bishop p = new Bishop(f, Color.Black);
                            KeyFieldValuePiece.Add(f.ToString(), p);
                        }
                        else if (c == 3)
                        {
                            //Queen's Row
                            Queen p = new Queen(f, Color.Black);
                            KeyFieldValuePiece.Add(f.ToString(), p);
                        }
                        else if (c == 4)
                        {
                            //King's Row
                            King p = new King(f, Color.Black);
                            KeyFieldValuePiece.Add(f.ToString(), p);
                        }
                    }
                    if (r == 1)
                    {
                        Pawn p = new Pawn(f, Color.White);
                        KeyFieldValuePiece.Add(f.ToString(), p);
                    }
                    if(r == 6)
                    {
                        //Console.WriteLine(field);
                        Pawn p = new Pawn(f, Color.Black);
                        KeyFieldValuePiece.Add(f.ToString(), p);
                    }
                }
            }
        }
        public bool TryCastleShort(Color white)
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            string total = "   A  B  C  D  E  F  G  H \n";
            for (int r = 0; r < 8; r++)
            {
                string b = " +--+--+--+--+--+--+--+--+\n";
                total = total + b;
                total = total + (r + 1).ToString();
                for (int c = 0; c < 8; c++)
                {
                    Field f = new Field(r, c);
                    //checks whether field is Empty or there is a piece on it.
                    //returns also a boolean reporting on the success of the retrieval

                    bool hasVal = KeyFieldValuePiece.TryGetValue(f.ToString(), out var value);

                    if (hasVal)
                    {
                        var rep = value.PieceColor == Color.Black ? value.PrintRepresentation.ToLower() : value.PrintRepresentation;
                        total = total + $"|{rep}";
                    }
                    else
                    {
                        total = total + "|..";
                    }
                }
                total = total + "| \n";
            }
            total = total + " +--+--+--+--+--+--+--+--+\n";
            return total;
        }
        public bool IsRowAndColStillBoard(int rN, int cN)
        {
            return (0 <= cN && cN < 8 && 0 <= rN  && rN < 8);
        }
        public void CreateAGhostlyPawn(Pawn p, int iterationOfCreation, Field field, Color color)
        {
            //Inserts a new ghost Pawn on the board
            KeyFieldValuePiece[field.ToString()] = new GhostPawn(p,iterationOfCreation,field, color);
        }
        public Tuple<bool, Field> IsKingOnMoveList(List<Move> moveList, Color color)
        {
            foreach (var f in moveList)
            {
                var wasSuccess = this.TryGetPieceFromField(f.ToField, out var p);
                if (wasSuccess)
                {
                    if (p.GetType() == typeof(King))
                    {
                        if (p.PieceColor == color)
                        {
                            return new Tuple<bool, Field>(true, p.CurrentField);
                        }
                    }
                }
            }
            return new Tuple<bool, Field>(false, null);
        }
        public bool IsCheckedByColor(Color c)
        {
            var pieces = GetAllPiecesOfColor(c);
            foreach (var p in pieces)
            {
                var moves = p.GetPossibleMoves(this);
                List<Move> filteredMoves = new List<Move>();
                foreach (var m in moves)
                {
                    if (m.MovementType == MovementType.Capturing)
                    {
                        filteredMoves.Add(m);
                    }
                }
                var res = this.IsKingOnMoveList(filteredMoves, HelperFunctions.ColorSwapper(c));
                if (res.Item1)
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsFieldEmpty(Field f)
        {   
            bool isFieldOccupied = this.KeyFieldValuePiece.ContainsKey(f.ToString());
            return !isFieldOccupied;
        }
        public List<Piece> GetAllPiecesOfColor(Color c)
        {
            List<Piece> pList = new List<Piece>();
            foreach (var kvpair in KeyFieldValuePiece)
            {
                if (kvpair.Value.PieceColor == c)
                {
                    pList.Add(kvpair.Value);
                }
            }
            return pList;
        }

        public bool IsFieldOccupiedByColor(Field f, Color c)
        {
            //returns True if there is a black piece
            //returns False if there is  a white piece or field empty
            var pieceExists = TryGetPieceFromField(f,out var piece);
            if (!pieceExists)
            {
                //field is empty
                return false;
            }
            else if (piece.PieceColor == c)
            {
                //piece is correct color
                return true;
            }
            else
            {
                //piece is other color
                return false;
            }
        }

        public void RemoveSomeGhosts(int currentIteration)
        {
            var copyDict = new Dictionary<string, Piece>();
            foreach (var kvpPair in this.KeyFieldValuePiece)
            {
                copyDict.Add(kvpPair.Key, (Piece)kvpPair.Value.Clone());
            }
            foreach (var kvp in copyDict)
            {
                if (kvp.Value is GhostPawn gp)
                {
                    if (gp.IterationOfCreation < currentIteration)
                    {
                        this.KeyFieldValuePiece.Remove(kvp.Key);
                    }
                }
            }
        }
        //Move Piece in Dict and also change field in Piece Object
        //TODO implement move history
        public void MovePiece(Field from, Field to, MovementType type, int iterationOfMove)
        {
            var p = this.KeyFieldValuePiece[from.ToString()];
            var c = p.PieceColor;
            var toRow = to.FieldRow;
            var toCol = to.FieldCol;
            var fromRow = from.FieldRow;
            switch (type)
            {
                case MovementType.DoubleStep:
                    CreateAGhostlyPawn((Pawn) p, iterationOfMove,
                        toRow == 3 ? new Field(toRow - 1, toCol) : new Field(toRow + 1, toCol), c);
                    break;
                case MovementType.EnPassant:
                    var ghostPawnRetrieved = TryGetPieceFromField(to, out Piece g);
                    if (ghostPawnRetrieved)
                    {
                        var ghostPawn = (GhostPawn)g;
                        var realPawn = ghostPawn.TheRealPawn;
                        this.KeyFieldValuePiece.Remove(realPawn.CurrentField.ToString());
                    }
                    break;
                case MovementType.Moving:
                    break;
                case MovementType.MovingPeaceful:
                    break;
                case MovementType.Defending:
                    break;
                case MovementType.Capturing:
                    break;
                case MovementType.CastleShort:
                    break;
                case MovementType.CastleLong:
                    break;
                case MovementType.Promotion:
                    this.KeyFieldValuePiece[to.ToString()] = new Queen(to, p.PieceColor,true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            p.HasMovedOnce = true;
            p.CurrentField = to;
            if (IsFieldEmpty(to))
            {
                this.KeyFieldValuePiece.Add(to.ToString(),p);
            }
            else
            {
                this.KeyFieldValuePiece[to.ToString()] = p;
            }
            this.KeyFieldValuePiece.Remove(from.ToString());
            if (type == MovementType.CastleShort)
            {
                Field fRookOld = new Field(fromRow, 7);
                bool wasSuccess = TryGetPieceFromField(fRookOld, out var pc);
                if (wasSuccess)
                {
                    this.KeyFieldValuePiece.Remove(fRookOld.ToString());
                }
              
                Field fieldRookNew = new Field(fromRow, 5);
                pc.CurrentField = fieldRookNew;
                this.KeyFieldValuePiece.Add(fieldRookNew.ToString(), pc);
            }
            if (type == MovementType.CastleLong)
            {
                var fRookOld = new Field(fromRow, 0);
                var wasSuccess = TryGetPieceFromField(fRookOld, out var pc);
                if (wasSuccess)
                {
                    this.KeyFieldValuePiece.Remove(fRookOld.ToString());

                    Field fieldRookNew = new Field(fromRow, 3);
                    pc.CurrentField = fieldRookNew;
                    this.KeyFieldValuePiece.Add(fieldRookNew.ToString(), pc);
                }
            }
        }
        //because the King can't know whether other pieces have moved the higher class checkboard will perform this check.
        //Castling is performed on the kingside or queenside with the rook on the same rank.
        //Neither the king nor the chosen rook has previously moved.
        //There are no pieces between the king and the chosen rook.
        //The king is not currently in check.
        //The king does not pass through a square that is attacked by an enemy piece.
        //The king does not end up in check. (True of any legal move.)
        
        public bool TryGetPieceFromField(Field f, out Piece piece)
        {
            return KeyFieldValuePiece.TryGetValue(f.ToString(), out piece);
        }
        public bool TryCastle(out List<Move> moves, Piece piece1)
        {
            Move mo;
            var row = 0;
            bool gotRookForShort;
            moves = new List<Move>();
            //check there are no pieces in between
            if (piece1.PieceColor == Color.White)
            {
                row = 0;
                mo = new Move("KI", new Field("E1"), new Field("G1"), MovementType.CastleShort);
                gotRookForShort = this.TryGetPieceFromField(new Field(row, 7), out var rook);
            }
            else
            {
                row = 7;
                mo = new Move("KI", new Field("E8"), new Field("G8"), MovementType.CastleShort);
                gotRookForShort = this.TryGetPieceFromField(new Field(row, 7), out var rook);
            }
            bool spaceForShortCastleEmpty;
            bool isbishopFieldEmpty = !TryGetPieceFromField(new Field(row, 5), out var bishop);
            bool isknightFieldEmpty = !TryGetPieceFromField(new Field(row, 6), out var knight);
            spaceForShortCastleEmpty = isbishopFieldEmpty & isknightFieldEmpty;
            if (!piece1.HasMovedOnce & gotRookForShort & spaceForShortCastleEmpty)
            {
                moves.Add(mo);
            }
            bool spaceForLongCastleEmpty;
            bool isKnightFieldEmpty = !TryGetPieceFromField(new Field(row, 1), out var knight2);
            bool isBishopFieldEmpty = !TryGetPieceFromField(new Field(row, 2), out var bishop2);
            bool isQueenFieldEmpty = !TryGetPieceFromField(new Field(row, 3), out var queen);
            spaceForShortCastleEmpty = isknightFieldEmpty & isbishopFieldEmpty & isQueenFieldEmpty;
            if (piece1.PieceColor == Color.White)
            {
                row = 0;
                mo = new Move("KI", new Field("E1"), new Field("C1"), MovementType.CastleLong);
                gotRookForShort = this.TryGetPieceFromField(new Field(row, 7), out var rook);
            }
            else
            {
                row = 7;
                mo = new Move("KI", new Field("E8"), new Field("C8"), MovementType.CastleLong);
                gotRookForShort = this.TryGetPieceFromField(new Field(row, 7), out var rook);
            }
            if (!piece1.HasMovedOnce & gotRookForShort & spaceForShortCastleEmpty)
            {
                moves.Add(mo);
            }
            return moves.Count != 0;
        }
    }
}
