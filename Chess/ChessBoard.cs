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
                    Piece value;

                    //checks whether field is Empty or there is a piece on it.
                    //returns also a boolean reporting on the success of the retrieval

                    bool hasVal = KeyFieldValuePiece.TryGetValue(f.ToString(), out value);

                    if (hasVal)
                    {
                        total = total + $"|{value}";
                    }
                    else
                    {
                        total = total + $"|..";
                    }
                }
                total = total + "| \n";
            }
            total = total + " +--+--+--+--+--+--+--+--+\n";
            return total;
        }
        public bool IsRowAndColStillBoard(int r_n, int c_n)
        {
            return (0 <= c_n && c_n < 8 && 0 <= r_n  && r_n < 8);
        }
        public void CreateAGhostlyPawn(Pawn p, int iterofcreation, Field field, Color color)
        {
            //Inserts a new ghost Pawn on the board
            KeyFieldValuePiece[field.ToString()] = new GhostPawn(p,iterofcreation,field, color);
        }
        public Tuple<bool, Field> IsKingOnMoveList(List<Move> flist, Color color)
        {
            var cb = this;
            var posList = cb.KeyFieldValuePiece;

            foreach (var f in flist)
            {
                Piece p;
                var wasSuccess = this.TryGetPieceFromField(f.ToField, out p);
                if (wasSuccess)
                {
                    if (p.GetType() == typeof(King))
                    {
                        if (p.PieceColor == color)
                        {
                            return new Tuple<bool, Field>(true, p.CurrField);
                        }
                    }
                }
            }

            return new Tuple<bool, Field>(false, null);
        }
        public bool IsChecked(Color c)
        {
            
            var pieces = getAllPiecesOfColor(c);
            foreach (var p in pieces)
            {
                var moves = p.getPossibleMoves(this);
                List<Move> filteredMoves = new List<Move>();
                foreach (var m in moves)
                {
                    if (m.MovementType == MovementType.Capturing)
                    {
                        filteredMoves.Add(m);
                    }
                }
                

                var res = this.IsKingOnMoveList(filteredMoves, Helperfunctions.ColorSwapper(c));
                if (res.Item1)
                {
                    return true;
                }
            }

            return false;
        }

        //Check wheter a field is empty
        public bool IsFieldEmpty(Field f)
        {   
            bool isFieldOccupied = this.KeyFieldValuePiece.ContainsKey(f.ToString());
            return !isFieldOccupied;
        }

        public List<Piece> getAllPiecesOfColor(Color c)
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
            Piece piece;
            var pieceExists = TryGetPieceFromField(f,out piece);
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

        public void RemoveSomeGhosts(int currentIter)
        {
            //iter over positionsDict
            //remove the ghost which are too old.
            //Result: en passant is only possible

            //Changing an iterable while iteration over it -> Error. 
            //Fix: Copy the dictionary iter over it. but change the original one.
            var copyDict = new Dictionary<string, Piece>();
            foreach (var kvpPair in this.KeyFieldValuePiece)
            {
                //clone -> returns object -> to Piece
                copyDict.Add(kvpPair.Key, (Piece)kvpPair.Value.Clone());
            }



            foreach (var kvp in copyDict)
            {
                
                GhostPawn gp;
                if (kvp.Value is GhostPawn)
                {
                    gp = ((GhostPawn) kvp.Value);
                    if (gp.IterationOfCreation < currentIter)
                    {
                        this.KeyFieldValuePiece.Remove(kvp.Key);
                    }
                }
            }
        }

        //Move Piece in Dict and also change field in Piece Object
        //TODO implement move history


        public void MovePiece(Field from, Field to, MovementType type, int iterOfMove)
        {

            var p = this.KeyFieldValuePiece[from.ToString()];
            var c = p.PieceColor;
            var toRow = to.FieldRow;
            var toCol = to.FieldCol;
            var fromRow = from.FieldRow;
            var fromCol = from.FieldCol;


            switch (type)
            {
                case MovementType.DoubleStep:
                    if (toRow == 3)
                    {
                        CreateAGhostlyPawn((Pawn)p, iterOfMove, new Field(toRow - 1, toCol), c);
                    }
                    else
                    {
                        CreateAGhostlyPawn((Pawn)p, iterOfMove, new Field(toRow + 1, toCol), c);
                    }
                    break;

                case MovementType.EnPassant:
                    Piece g;
                    TryGetPieceFromField(to, out g);
                    
                   
                    var ghostPawn = (GhostPawn) g;
                    var realPawn = ghostPawn.TheRealPawn;

                    this.KeyFieldValuePiece.Remove(realPawn.CurrField.ToString());

                    break;
            }

            //Piece which gonna move
            
            p.HasMovedOnce = true;
            p.field = to;
            if (IsFieldEmpty(to))
            {
                this.KeyFieldValuePiece.Add(to.ToString(),p);
            }
            else
            {
                var deadP = this.KeyFieldValuePiece[to.ToString()];
                this.KeyFieldValuePiece[to.ToString()] = p;
            }
            this.KeyFieldValuePiece.Remove(from.ToString());

            if (type == MovementType.CastleShort)
            {

                Field fRookOld = new Field(fromRow, 7);
                Piece pc;
                bool wassuccess = TryGetPieceFromField(fRookOld, out pc);
                this.KeyFieldValuePiece.Remove(fRookOld.ToString());

                Field f_rooknew = new Field(fromRow, 5);
                this.KeyFieldValuePiece.Add(f_rooknew.ToString(), pc);
            }

        }
        //because the King can't know whether other pieces have moved the higher class checkboard will perform this check.
        //Castling is performed on the kingside or queenside with the rook on the same rank.
        //Neither the king nor the chosen rook has previously moved.
        //There are no pieces between the king and the chosen rook.
        //The king is not currently in check.
        //The king does not pass through a square that is attacked by an enemy piece.
        //The king does not end up in check. (True of any legal move.)
        public Tuple<bool, Move> CastleLong(Color c)
        {
            //check whether King haven't moved yet.
            //check whether Rook didn't move yet.
            int row = 0;
            Move mo_;

            Tuple<bool, Move> moveTuple;

            //check there are no pieces in bewetween
            if (c == Color.White)
            {
                row = 0;
                mo_ = new Move("KI", new Field("E1"), new Field("G1"), MovementType.CastleLong);
            }
            else
            {
                row = 7;
                mo_ = new Move("KI", new Field("E1"), new Field("G1"), MovementType.CastleLong);
            }

            List<Field> shouldntHaveMoved = new List<Field>();

            Field fofRook = new Field(row, 0);
            Field fofKing = new Field(row, 4);
            shouldntHaveMoved.Add(fofKing);
            shouldntHaveMoved.Add(fofRook);

            List<Field> shouldBeEmpty = new List<Field>();

            Field fofKingsNeighbor = new Field(row, 1);
            Field fofRooksNeighbor = new Field(row, 2);
            Field fofbishop = new Field(row, 3);
            shouldBeEmpty.Add(fofRooksNeighbor);
            shouldBeEmpty.Add(fofKingsNeighbor);
            shouldBeEmpty.Add(fofbishop);

            foreach (var f in shouldBeEmpty)
            {
                if (!this.IsFieldEmpty(f))
                {
                    moveTuple = new Tuple<bool, Move>(false, mo_);
                    return moveTuple;
                }
            }

            foreach (var f in shouldntHaveMoved)
            {
                Piece piece;
                var isRetrieved = this.TryGetPieceFromField(f, out piece);
                if (isRetrieved)
                {
                    if (piece.HasMovedOnce)
                    {
                        moveTuple = new Tuple<bool, Move>(false, mo_);
                        return moveTuple;
                    }
                }
            }

            moveTuple = new Tuple<bool, Move>(true, mo_);
            return moveTuple;
        }

        public Tuple<bool,Move> CastleShort(Color c)
        {
            Tuple<bool, Move> moveTuple;
            Move mo_;
            int row = 0;
            //check there are no pieces in bewetween
            if (c == Color.White)
            {
                row = 0;

                mo_ = new Move("KI", new Field("E1"), new Field("G1"), MovementType.CastleShort);
            }
            else
            {
                row = 7;
                mo_ = new Move("KI", new Field("E8"), new Field("G8"), MovementType.CastleShort);
            }
            List<Field> shouldntHaveMoved = new List<Field>();
            
            Field fofRook = new Field(row, 0);
            Field fofKing = new Field(row, 4);
            shouldntHaveMoved.Add(fofKing);
            shouldntHaveMoved.Add(fofRook);

            List<Field> shouldBeEmpty = new List<Field>();

            Field fofKingsNeighbor = new Field(row, 5);
            Field fofRooksNeighbor = new Field(row, 6);
            shouldBeEmpty.Add(fofRooksNeighbor);
            shouldBeEmpty.Add(fofKingsNeighbor);

            foreach (var f in shouldBeEmpty)
            {
                if (!this.IsFieldEmpty(f))
                {
                    moveTuple = new Tuple<bool, Move>(false, mo_);
                    return moveTuple;
                }
            }

            foreach (var f in shouldntHaveMoved)
            {
                var isRetrieved = this.TryGetPieceFromField(f, out var piece);
                if (isRetrieved)
                {
                    if (piece.HasMovedOnce)
                    {
                        moveTuple = new Tuple<bool, Move>(false, mo_);
                        return moveTuple; 
                    }
                }
            }

            moveTuple = new Tuple<bool, Move>(true, mo_);
            return moveTuple; ;
        }



        
        public bool TryGetPieceFromField(Field f, out Piece piece)
        {
            return KeyFieldValuePiece.TryGetValue(f.ToString(), out piece);
        }


    }
}
