using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using OOPChessProject.Pieces;

namespace OOPChessProject
{
    public class ChessBoard
    {
        //field
        //remove public TODO
        public Dictionary<string, Piece> kFieldvPiece;
        public List<Piece> deadPieces = new List<Piece>();

        //A Constructor which sets up the Game Board
        public ChessBoard()
        {
            //create a dict to store the pieces as VALUE and field as KEY
            kFieldvPiece = new Dictionary<string, Piece>();
            //iterate over all field combinations
            foreach (row r in Enum.GetValues(typeof(row)))
            {
                foreach (col c in Enum.GetValues(typeof(col)))
                {
                    Field f = new Field(r, c);

                    // Rows = {1..8} , Cols = {1..8}
                    int rowNum = (int)r + 1;
                    int colNum = (int)c + 1;

                    //Piece p;
                    if(rowNum == 1)
                    {
                        
                        if(colNum == 1 | colNum == 8)
                        {
                            Rook p = new Rook(f, Color.White);
                            kFieldvPiece.Add(f.ToString(), p);

                        }
                        else if(colNum == 2 | colNum == 7)
                        {
                            Knight p = new Knight(f, Color.White);
                            kFieldvPiece.Add(f.ToString(), p);
                        }
                        else if(colNum == 3 | colNum == 6)
                        {
                            Bishop p = new Bishop(f, Color.White);
                            kFieldvPiece.Add(f.ToString(), p);
                        }
                        else if(colNum == 4)
                        {
                            //Queen's Row
                            Queen p = new Queen(f, Color.White);
                            kFieldvPiece.Add(f.ToString(), p);
                        }
                        else if(colNum  == 5)
                        {
                            //King's Row
                            King p = new King(f, Color.White);
                            kFieldvPiece.Add(f.ToString(), p);
                        }
                       
                    }

                    if (rowNum == 8)
                    {

                        if (colNum == 1 | colNum == 8)
                        {
                            Rook p = new Rook(f, Color.Black);
                            kFieldvPiece.Add(f.ToString(), p);

                        }
                        else if (colNum == 2 | colNum == 7)
                        {
                            Knight p = new Knight(f, Color.Black);
                            kFieldvPiece.Add(f.ToString(), p);
                        }
                        else if (colNum == 3 | colNum == 6)
                        {
                            Bishop p = new Bishop(f, Color.Black);
                            kFieldvPiece.Add(f.ToString(), p);
                        }
                        else if (colNum == 4)
                        {
                            //Queen's Row
                            Queen p = new Queen(f, Color.Black);
                            kFieldvPiece.Add(f.ToString(), p);
                        }
                        else if (colNum == 5)
                        {
                            //King's Row
                            King p = new King(f, Color.Black);
                            kFieldvPiece.Add(f.ToString(), p);
                        }
                    }

                    if (rowNum == 2)
                    {
                        Pawn p = new Pawn(f, Color.White);
                        kFieldvPiece.Add(f.ToString(), p);
                    }
                    if(rowNum == 7)
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
            foreach (row r in Enum.GetValues(typeof(row)))
            {
                string b = " +--+--+--+--+--+--+--+--+\n";
                total = total + b;
                total = total + ((int)r + 1).ToString();
                foreach (col c in Enum.GetValues(typeof(col)))
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


        public string debugBoardPrint()
        {
            string total = "   A  B  C  D  E  F  G  H \n";
            foreach (row r in Enum.GetValues(typeof(row)))
            {
                string b = " +--+--+--+--+--+--+--+--+\n";
                total = total + b;
                total = total + ((int)r + 1).ToString();
                foreach (col c in Enum.GetValues(typeof(col)))
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
        //if the destination field is occupied move it to the deadPieces

        //Objekt ändert sich selbst.
        //Oder eher anderes Objekt ändert dieses Objekt (WAM: Trennung Fields und Methoden)




        //TODO how to handle input field which is empty
        //Move Piece in Dict and also change field in Piece Object
        public void MovePiece(Field f1, Field f2)
        {
            
            //Piece which gonna move
            var p = this.kFieldvPiece[f1.ToString()];
            p.field = f2;
            if (IsFieldEmpty(f2))
            {
                
                this.kFieldvPiece.Add(f2.ToString(),p);
            }
            else
            {
                var deadP = this.kFieldvPiece[f2.ToString()];
                this.kFieldvPiece[f2.ToString()] = p;
                deadPieces.Add(deadP);
            }
            this.kFieldvPiece.Remove(f1.ToString());
        }

        public void castleShort()
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

        //TODO use this method?
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
