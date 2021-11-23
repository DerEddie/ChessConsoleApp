using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using OOPChessProject;
using OOPChessProject.Pieces;

namespace OOPChessProject
{
    public class ChessBoard
    {
        //field
        public Dictionary<string, Piece> kFieldvPiece;
        List<Piece> deadPieces = new List<Piece>();


        public ChessBoard(ChessBoard cb)
        {
            kFieldvPiece = cb.kFieldvPiece;
        }

        //A Constructor which sets up the Game Board
        public ChessBoard()
        {
            //create a dict from store the pieces as VALUE and field as KEY
            kFieldvPiece = new Dictionary<string, Piece>();
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
                            kFieldvPiece.Add(f.ToString(), p);

                        }
                        else if(c == 1 | c == 6)
                        {
                            Knight p = new Knight(f, Color.White);
                            kFieldvPiece.Add(f.ToString(), p);
                        }
                        else if(c == 2| c == 5)
                        {
                            Bishop p = new Bishop(f, Color.White);
                            kFieldvPiece.Add(f.ToString(), p);
                        }
                        else if(c == 3)
                        {
                            //Queen's Row
                            Queen p = new Queen(f, Color.White);
                            kFieldvPiece.Add(f.ToString(), p);
                        }
                        else if(c  == 4)
                        {
                            //King's Row
                            King p = new King(f, Color.White);
                            kFieldvPiece.Add(f.ToString(), p);
                        }
                       
                    }

                    if (r == 7)
                    {

                        if (c == 0 | c == 7)
                        {
                            Rook p = new Rook(f, Color.Black);
                            kFieldvPiece.Add(f.ToString(), p);

                        }
                        else if (c == 1 | c == 6)
                        {
                            Knight p = new Knight(f, Color.Black);
                            kFieldvPiece.Add(f.ToString(), p);
                        }
                        else if (c == 2 | c == 5)
                        {
                            Bishop p = new Bishop(f, Color.Black);
                            kFieldvPiece.Add(f.ToString(), p);
                        }
                        else if (c == 3)
                        {
                            //Queen's Row
                            Queen p = new Queen(f, Color.Black);
                            kFieldvPiece.Add(f.ToString(), p);
                        }
                        else if (c == 4)
                        {
                            //King's Row
                            King p = new King(f, Color.Black);
                            kFieldvPiece.Add(f.ToString(), p);
                        }
                    }

                    if (r == 1)
                    {
                        Pawn p = new Pawn(f, Color.White);
                        kFieldvPiece.Add(f.ToString(), p);
                    }
                    if(r == 6)
                    {
                        //Console.WriteLine(f);
                        Pawn p = new Pawn(f, Color.Black);
                        kFieldvPiece.Add(f.ToString(), p);
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

                    bool hasVal = kFieldvPiece.TryGetValue(f.ToString(), out value);

                    if (hasVal)
                    {
                        total = total + String.Format("|{0}", value);
                    }
                    else
                    {
                        total = total + String.Format("|..", value);
                    }
                }
                total = total + "| \n";
            }
            total = total + " +--+--+--+--+--+--+--+--+\n";
            return total;
        }


        public bool isRowAndColStillBoard(int r_n, int c_n)
        {
            return (0 <= c_n && c_n < 8 && 0 <= r_n  && r_n < 8);
        }

        


        public Tuple<bool, Field> IsKingOnMoveList(List<Move> flist)
        {
            var cb = this;
            var posList = cb.kFieldvPiece;

            foreach (var f in flist)
            {
                Piece p;
                var wasSuccess = this.TryGetPieceFromField(f.ToField, out p);
                if (wasSuccess)
                {
                    if (p.GetType() == typeof(King))
                    {
                        return new Tuple<bool, Field>(true, p.CurrField);
                    }
                }
            }

            return new Tuple<bool, Field>(false, null);
        }


        public bool isChecked(Color c)
        {
            
            var pieces = getAllPiecesOfColor(c);
            foreach (var p in pieces)
            {
                var moves = p.getPossibleMoves(this);
                List<Move> filtered_moves = new List<Move>();
                foreach (var m in moves)
                {
                    if (m.movementType == MovementType.capturing)
                    {
                        filtered_moves.Add(m);
                    }
                }
                

                var res = this.IsKingOnMoveList(filtered_moves);
                if (res.Item1)
                {
                    return true;
                }
            }

            return false;
        }

        public HashSet<Field> getControlledFieldsByColor(Color c, ChessBoard cb)
        {
            //use a hashset so every Field will be unique.
            HashSet<Field> forbiddenFields = new HashSet<Field>();

            var pList = getAllPiecesOfColor(c);
            foreach (var p in pList)
            {
                var controlledFieldList = getControlledFields(p, cb, p.rowOfsetcolOfset);
            }


            return forbiddenFields;
        }

        protected List<Field> getControlledFields(Piece p,  ChessBoard cb, List<(int, int)> directions, bool Istraverse = true)
        {
            List<Field> fList = new List<Field>();

            //create fields and append to the List
            int r = p.CurrField.FieldRow;
            int c = p.CurrField.FieldCol;

            int iterMax;
            if (Istraverse) iterMax = 7; else iterMax = 1;


            Color EnemyCol = Helper.ColorSwapper(p.PieceColor);

            foreach (var os in directions)
            {
                int r_n = 0;
                int c_n = 0;
                for (int i = 1; i <= iterMax; i++)
                {
                    r_n = r + os.Item1 * i;
                    c_n = c + os.Item2 * i;

                    if (cb.isRowAndColStillBoard(r_n, c_n))
                    {
                        Field fn = new Field(r_n, c_n);

                        if (cb.IsFieldOccupiedByColor(fn, EnemyCol))
                        {
                            //if this piece is white and we reach a black one we can capture it but must stop iteration
                            fList.Add(fn);

                            break;
                        }
                        else if (cb.IsFieldOccupiedByColor(fn, p.PieceColor))
                        {
                            //field occupied by own piece
                            fList.Add(fn);
                            break;
                        }
                        else
                        {
                            //its an empty field
                            fList.Add(fn);
                        }

                    }
                }
            }

            return fList;
        }



        public string debugBoardPrint()
        {
            string total = "   A  B  C  D  E  F  G  H \n";
            for (int r = 0; r < 8; r++)
            {
                
                string b = " +--+--+--+--+--+--+--+--+\n";
                total = total + b;
                total = total + ((int)r + 1).ToString();
                for (int c = 0; r < 8; c++)
                {
                    Field f = new Field(r, c);
                    Piece value;

                    //checks whether field is Empty or there is a piece on it.
                    //returns also a boolean reporting on the success of the retrieval

                    bool hasVal = kFieldvPiece.TryGetValue(f.ToString(), out value);

                    if (hasVal)
                    {
                        total = total + String.Format("|{0}{1}", value, value.PieceColor);
                    }
                    else
                    {
                        total = total + String.Format("|..", value);
                    }
                }
                total = total + "| \n";

            }
            total = total + " +--+--+--+--+--+--+--+--+\n";
            return total;
        }


        //Check wheter a field is empty
        public bool IsFieldEmpty(Field f)
        {   
            bool IsFieldOccupied = this.kFieldvPiece.ContainsKey(f.ToString());
            return !IsFieldOccupied;
        }

        public List<Piece> getAllPiecesOfColor(Color c)
        {
            List<Piece> pList = new List<Piece>();
            foreach (var kvpair in kFieldvPiece)
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

        


        public bool IsFieldWhiteOccupied(Field f)
        {
            var p = GetPieceFromField(f);
            return Color.White == p.PieceColor;
        }


        //return something if it is accessible or return null???
        /*
        public Piece locateKingOnBoard(Color color)
        {
            foreach (var kvp in kFieldvPiece)
            {
                if (kvp.Value.PrintRepresentation == "KN" &  )
                {
                    return kvp.Value;
                }
            }
        }
        */

        //Removing the field from the dict and creating new entry/overwriting entry
        //if the destination field is occupied move it from the deadPieces

        //Objekt ändert sich selbst.
        //Oder eher anderes Objekt ändert dieses Objekt (WAM: Trennung Fields und Methoden)



        //Move Piece in Dict and also change field in Piece Object
        //TODO implement move history
        public void MovePiece(Field from, Field to)
        {
            
            //Piece which gonna move
            var p = this.kFieldvPiece[from.ToString()];
            p.field = to;
            if (IsFieldEmpty(to))
            {
                
                this.kFieldvPiece.Add(to.ToString(),p);
            }
            else
            {
                var deadP = this.kFieldvPiece[to.ToString()];
                this.kFieldvPiece[to.ToString()] = p;
                deadPieces.Add(deadP);
            }
            this.kFieldvPiece.Remove(from.ToString());

            //cg.movesHistory.Add(new Move(p.PrintRepresentation,from,to, MovementType.moving));
        }

        public void castleShort(Color c)
        {
            //check whether King haven't moved yet.
            //check whether Rook didn't move yet.
            


        }

        public void castleLong()
        {

        }


        public Piece GetPieceFromField(Field f)
        {
            Piece value;
            bool hasVal = kFieldvPiece.TryGetValue(f.ToString(), out value);
            if (hasVal)
            {
                return value;
            }
            else
            {
                return null;
            }
        }

        public bool TryGetPieceFromField(Field f, out Piece piece)
        {
            return kFieldvPiece.TryGetValue(f.ToString(), out piece);
        }
        public Field getKingPostion()
        {
            return null;
        }

    }
}
