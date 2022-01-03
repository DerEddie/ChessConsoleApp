using System.Collections.Generic;

namespace Chess.Pieces
{
    public class Pawn : Piece
    {
        private readonly int _directionFactor;

        public Pawn(Field position, Color pieceColor) : base(position, pieceColor)
        {
            //already Implemented
            PrintRepresentation= "PW";
            //set the 
            if (PieceColor == Color.White)
            {
                _directionFactor = -1;

            }
            else
            {
                _directionFactor = 1;
            }
        }

        //The object keyword represents the System.Object type,
        //which is the root type in the C# class hierarchy. This
        //keyword is often used when there's no way to identify
        //the object type at compile time, which often happens
        //in various interoperability scenarios.

        public override object Clone()
        {
            var pawn = new Pawn(CurrentField, PieceColor);
            return pawn;
        }

        private List<Move> GetDoubleStepMoves(int rNr, int cNr, ChessBoard cb)
        {
            var fList = new List<Move>();
            //Check the double-step
            if ( (CurrentField.FieldRow == 1 & PieceColor == Color.White ) | (CurrentField.FieldRow == 6 & PieceColor == Color.Black))
            {
                if(GetMovingMoves(rNr,cNr,cb).Count != 0)
                {
                    int r1 = rNr + 2 * (-_directionFactor);
                    int c1 = cNr;
                    Field f = new Field(r1, c1);
                    if (cb.IsFieldEmpty(f))
                        fList.Add(new Move(PrintRepresentation, CurrentField, f, MovementType.DoubleStep));
                }
            }
            return fList;
        }

        private List<Move> GetCapturingMoves(int rNr, int cNr, ChessBoard cb)
        {
            List<Move> fList = new List<Move>();
            var rcOffsetCapturing = new List<(int, int)>
            {
                (-1, -1),
                (-1, 1)
            };
            foreach (var (item1, item2) in rcOffsetCapturing)
            {
                //Check so Pawn doesn't try to capture outside the board,
                if (cb.IsRowAndColStillBoard(rNr + item1 * _directionFactor, cNr + item2 * _directionFactor))
                {
                    Field f1 = new Field(rNr + item1 * _directionFactor, cNr + item2 * _directionFactor);
                    cb.TryGetPieceFromField(f1, out var p);


                    if (cb.IsFieldOccupiedByColor(f1, HelperFunctions.OppositeColor(PieceColor)))
                    {
                        fList.Add(p.PrintRepresentation == "xx"
                            ? new Move(PrintRepresentation, CurrentField, f1, MovementType.EnPassant)
                            : new Move(PrintRepresentation, CurrentField, f1, MovementType.Capturing));
                    }
                    else
                    {
                        fList.Add(new Move(PrintRepresentation, CurrentField, f1, MovementType.Defending));
                    }
                }
            }
            return fList;
        }

        private List<Move> GetMovingMoves(int rNr, int cNr, ChessBoard cb)
        {
            //Pawn move in different direction depending on color
            List<Move> fList = new List<Move>();
            //convert back to enum with modified values
            var r1 =  rNr + 1 * (-_directionFactor);
            var c1 =  cNr;
            var f = new Field(r1, c1);
            //check whether Field is Empty
            if (cb.IsFieldEmpty(f))
            {
                if (r1 == 7 | r1 == 0)
                {
                    fList.Add(new Move(PrintRepresentation, CurrentField, f, MovementType.Promotion));
                }
                else
                {
                    fList.Add(new Move(PrintRepresentation, CurrentField, f, MovementType.MovingPeaceful));
                }
            }
            return fList;
        }


        public override List<Move> GetPossibleMoves(ChessBoard cb)
        {
            //Since Pawns move only forward, We need to know whether piece is black or white
            var rNr = CurrentField.FieldRow;
            int cNr = CurrentField.FieldCol;
            var mListMoving = GetMovingMoves(rNr, cNr, cb);
            var mListCapturing = GetCapturingMoves(rNr, cNr, cb);
            var mListMoving2Step = GetDoubleStepMoves(rNr, cNr, cb);
            mListCapturing.AddRange(mListMoving);
            mListCapturing.AddRange(mListMoving2Step);
            return mListCapturing;
        }

    }
}
