using System;
using System.Collections.Generic;

namespace Chess.Pieces 
{
    public enum Color
    {
        White,
        Black
    }
    public abstract class Piece : ICloneable
    {
        //A protected member is accessible within its class and by derived class instances.
        public Field CurrentField;
        public bool HasMovedOnce = false;
        //has a value in the children-classes
        public string PrintRepresentation;
        public readonly Color PieceColor;
        public abstract object Clone();
        //create a base constructor for instance creation of different pieces
        protected Piece(Field position, Color pieceColor)
        {
            //takes field instance for position
            CurrentField = position;
            PieceColor = pieceColor;
        }
        //Implement in Piece children
        public abstract List<Move> GetPossibleMoves(ChessBoard cb);
        public override string ToString()
        {
            return this.PrintRepresentation;
        }
        //adds possible moves to the List
        protected void TraverseInDirection(List<Move> mList, int traverseSteps, int row, int col, (int, int) direction, ChessBoard cb)
        {
            for (var i = 1; i <= traverseSteps; i++)
            {
                var (item1, item2) = direction;
                var rN = row + item1 * i;
                var cN = col + item2 * i;
                if (cb.IsRowAndColStillBoard(rN, cN))
                {
                    var fn = new Field(rN, cN);
                    cb.TryGetPieceFromField(fn, out var p);
                    if (cb.IsFieldOccupiedByColor(fn, HelperFunctions.OppositeColor(this.PieceColor)))
                    {
                        if (p.PrintRepresentation == "KI")
                        {
                            //continue because want the fields behind the kind register as well, so king can't just step back
                            mList.Add(new Move(PrintRepresentation, CurrentField, fn, MovementType.Capturing));
                            continue;
                        }
                        if (p.PrintRepresentation != "xx")
                        {
                            mList.Add(new Move(PrintRepresentation, CurrentField, fn, MovementType.Capturing));
                            break;
                        }
                        //if this piece is white and we reach a black one we can capture it but must stop iteration
                        mList.Add(new Move(this.PrintRepresentation, this.CurrentField, fn, MovementType.Moving));
                    }
                    else if (cb.IsFieldOccupiedByColor(fn, this.PieceColor))
                    {
                        mList.Add(new Move(this.PrintRepresentation, this.CurrentField, fn, MovementType.Defending));
                        break;
                    }
                    else
                    {
                        //its an empty field
                        mList.Add(new Move(this.PrintRepresentation, this.CurrentField, fn, MovementType.Moving));
                    }
                }
            }
        }
        protected List<Move> GetPossibleMovesTraversing(ChessBoard cb, List<(int, int)> directions, int traverseSteps = 7)
        {
            List<Move> mList = new List<Move>();
            //create fields and append to the List
            var r = CurrentField.FieldRow;
            var c = CurrentField.FieldCol;
            foreach (var os in directions)
            {
                TraverseInDirection(mList,traverseSteps,r,c,os,cb);
            }
            return mList;
        }
    }
}
