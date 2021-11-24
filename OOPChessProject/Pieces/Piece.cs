using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;


namespace OOPChessProject 
{
    public enum Color
    {
        White,
        Black
    }
    public abstract class Piece : ICloneable
    {

        //A protected member is accessible within its class and by derived class instances.
        public Field CurrField;
        bool IsAlive;
        public bool HasMovedOnce = false;


        //copyConstructor
        public Piece(Piece p)
        {
            CurrField = p.CurrField;
            IsAlive = p.IsAlive;
            HasMovedOnce = p.HasMovedOnce;
        }

        public abstract object Clone();
 

        //Movement Behaviour
        public List<(int, int)> rowOfsetcolOfset;

        //has a value in the children-classes
        public string PrintRepresentation;
        public Color PieceColor;

        //create a base constructor for instance creation of different pieces
        
        public Piece(Field position, Color pieceColor, bool aisAlive = true)
        {
            //takes field instance for position
            CurrField = position;
            IsAlive = aisAlive;
            PieceColor = pieceColor;
            
        }


        //Implement in Piece children
        public abstract List<Move> getPossibleMoves(ChessBoard cb);


        public override string ToString()
        {
            return this.PrintRepresentation;
        }


        public Field field   // property
        {
            get 
            {
                return field;  // get method
            }  

            set 
            { 
                CurrField = value;   // set method
            } 
        }

        //adds possible moves to the List
        protected void TraverseInDirection(List<Move> mList, int traverseSteps, int row, int col, (int, int) direction, ChessBoard cb)
        {
            int r_n = 0;
            int c_n = 0;

            for (int i = 1; i <= traverseSteps; i++)
            {
                r_n = row + direction.Item1 * i;
                c_n = col + direction.Item2 * i;

                Color EnemyCol;
                if (cb.isRowAndColStillBoard(r_n, c_n))
                {
                    Field fn = new Field(r_n, c_n);

                    if (this.PieceColor == Color.White)
                    {
                        EnemyCol = Color.Black;
                    }
                    else
                    {
                        EnemyCol = Color.White;
                    }




                    if (cb.IsFieldOccupiedByColor(fn, EnemyCol))
                    {
                        //if this piece is white and we reach a black one we can capture it but must stop iteration
                        mList.Add(new Move(this.PrintRepresentation, this.CurrField, fn, MovementType.capturing));

                        break;
                    }
                    else if (cb.IsFieldOccupiedByColor(fn, this.PieceColor))
                    {
                        //field occupied by own piece
                        break;
                    }
                    else
                    {
                        //its an empty field
                        mList.Add(new Move(this.PrintRepresentation, this.CurrField, fn, MovementType.moving));
                    }

                }
            }
        }


        

        protected List<Move> getPossibleMovesTraversing(ChessBoard cb, List<(int, int)> directions, int traverseSteps = 7)
        {
            List<Move> mList = new List<Move>();

            //create fields and append to the List
            int r = CurrField.FieldRow;
            int c = CurrField.FieldCol;

            int iterMax;


            foreach (var os in directions)
            {
                TraverseInDirection(mList,traverseSteps,r,c,os,cb);
            }

            return mList;
        }


        //Slightly Different implementation where the fields where pieces of own color stand are also a field which "potentially" the piece can move to
        //this is important for the King movement. Because a King can caputure pieces but only those which aren't protected by their allies.
        protected void TraverseInDirectionControlling(List<Move> mList, int traverseSteps, int row, int col, (int, int) direction, ChessBoard cb)
        {
            int r_n = 0;
            int c_n = 0;

            for (int i = 1; i <= traverseSteps; i++)
            {
                r_n = row + direction.Item1 * i;
                c_n = col + direction.Item2 * i;

                Color EnemyCol;
                if (cb.isRowAndColStillBoard(r_n, c_n))
                {
                    Field fn = new Field(r_n, c_n);

                    if (this.PieceColor == Color.White)
                    {
                        EnemyCol = Color.Black;
                    }
                    else
                    {
                        EnemyCol = Color.White;
                    }




                    if (cb.IsFieldOccupiedByColor(fn, EnemyCol))
                    {
                        //if this piece is white and we reach a black one we can capture it but must stop iteration
                        mList.Add(new Move(this.PrintRepresentation, this.CurrField, fn, MovementType.capturing));

                        break;
                    }
                    else if (cb.IsFieldOccupiedByColor(fn, this.PieceColor))
                    {
                        mList.Add(new Move(this.PrintRepresentation, this.CurrField, fn, MovementType.controlling));
                        break;
                    }
                    else
                    {
                        //its an empty field
                        mList.Add(new Move(this.PrintRepresentation, this.CurrField, fn, MovementType.moving));
                    }

                }
            }
        }


    }

    
}
