using System.Collections.Generic;

namespace OOPChessProject.Pieces
{
    class Pawn : Piece
    {
        readonly int directionFactor;


        List<(int, int)> rowOfsetcolOfset = new List<(int, int)>
        {
            (-1, -1),
            (-1, 1)
        };

        public Pawn(Field position, Color pieceColor, bool aisAlive = true) : base(position, pieceColor, aisAlive)
        {
            //already Implemented
            base.PrintRepresentation= "PW";


            //set the 
            if (this.PieceColor == Color.White)
            {
                directionFactor = -1;

            }
            else
            {
                directionFactor = 1;
            }
        }

        private List<Move> GetDoubleStepMoves(int r_nr, int c_nr, ChessBoard cb)
        {
            List<Move> fList = new List<Move>();
            //Check the double-step
            if (this.CurrField.FieldRow == 1 | this.CurrField.FieldRow == 6)
            {
                int r1 = r_nr + 2 * (-directionFactor);
                int c1 = c_nr;
                Field f = new Field(r1, c1);
                if (cb.IsFieldEmpty(f))
                    fList.Add(new Move(this.PrintRepresentation, this.CurrField, f, MovementType.doubleStep));
            }

            return fList;
        }

        private List<Move> GetCapturingMoves(int r_nr, int c_nr, ChessBoard cb)
        {
            List<Move> fList = new List<Move>();

            var rc_offset_capturing = new List<(int, int)>
            {
                (-1, -1),
                (-1, 1)
            };

            foreach (var capt in rc_offset_capturing)
            {
                Field f1 = new Field(r_nr + capt.Item1 * directionFactor, c_nr + capt.Item2 * directionFactor);

                if (this.PieceColor == Color.White)
                {
                    if (cb.IsFieldOccupiedByColor(f1, Color.Black))
                    {
                        fList.Add(new Move(this.PrintRepresentation, this.CurrField, f1, MovementType.capturing));
                    }
                }
                else
                {
                    if (cb.IsFieldOccupiedByColor(f1, Color.White))
                    {
                        fList.Add(new Move(this.PrintRepresentation, this.CurrField, f1, MovementType.capturing));
                    }
                }
            }
            return fList;
        }

        private List<Move> GetMovingMoves(int r_nr, int c_nr, ChessBoard cb)
        {
            //Pawn move in different direction depending on color
            List<Move> fList = new List<Move>();

            //convert back to enum with modified values
            int r1 =  r_nr + 1 * (-directionFactor);
            int c1 =  c_nr;
            Field f = new Field(r1, c1);

            //check whether Field is Empty
            if (cb.IsFieldEmpty(f))
                fList.Add(new Move(this.PrintRepresentation, this.CurrField, f, MovementType.moving));
            ;



            return fList;
        }


        public override List<Move> getPossibleMoves(ChessBoard cb)
        {
            //Since Pawns move only forward, We need to know whether piece is black or white
            var rc_offset = new int[,] { { -1, 0 }};


            List<Move> fList = new List<Move>();

            int r_nr = CurrField.fieldToNum().Item1;
            int c_nr = CurrField.fieldToNum().Item2;

            var mList_moving = GetMovingMoves(r_nr, c_nr, cb);

            var mList_capturing = GetCapturingMoves(r_nr, c_nr, cb);

            var mList_moving2Step = GetDoubleStepMoves(r_nr, c_nr, cb);

            mList_capturing.AddRange(mList_moving);
            mList_capturing.AddRange(mList_moving2Step);
            return mList_capturing;
        }

    }
}
