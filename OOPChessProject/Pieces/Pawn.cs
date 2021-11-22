using System.Collections.Generic;

namespace OOPChessProject.Pieces
{
    class Pawn : Piece
    {

        public Pawn(Field aField, Color aPieceColor, bool aisAlive = true) : base(aField, aPieceColor, aisAlive)
        {
            //already Implemented
            base.PrintRepresentation= "PW";
        }


        //TODO consider en passant
        public override List<Move> getPossibleMoves(ChessBoard cb, bool isrecursive)
        {
            //Since Pawns move only forward, We need to know whether piece is black or white
            var rc_offset = new int[,] { { -1, 0 }};

            var rc_offset_capturing = new List<(int, int)>
            {
                (-1, -1),
                (-1, 1)
            };


                List<Move> fList = new List<Move>();

            row r = CurrField.fieldRow;
            col c = CurrField.fieldCol;

            //convert from enum to int
            int r_nr = (int)r;
            int c_nr = (int)c;

            //Pawn move in different direction depending on color
            int directionFactor;
            //use the chessboard
            if (this.PieceColor == Color.White)
            {
                directionFactor = -1;

            }
            else
            {
                directionFactor = 1;
            }

            //convert back to enum with modified
            row r1 = (row)r_nr + 1*(-directionFactor);
            col c1 = (col)c_nr;
            Field f = new Field(r1, c1);
            //check whether Field is Empty
            if (cb.IsFieldEmpty(f)) fList.Add(new Move(this.PrintRepresentation, this.CurrField, f, MovementType.moving)); ;

            
            foreach (var capt in rc_offset_capturing)
            {
                Field f1 = new Field((row) r_nr + capt.Item1*directionFactor, (col) c_nr + capt.Item2*directionFactor);

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
            
            

            //Check the double-step
            if (this.CurrField.FieldRow == row._2 | this.CurrField.FieldRow ==  row._7)
            {
                r1 = (row)r_nr + 2*(-directionFactor);
                c1 = (col)c_nr;
                f = new Field(r1, c1);
                if (cb.IsFieldEmpty(f)) fList.Add(new Move(this.PrintRepresentation, this.CurrField, f, MovementType.moving)); ;
            }

            //TODO Pawn can only attack opposite color..



            return fList;
        }
    }
}
