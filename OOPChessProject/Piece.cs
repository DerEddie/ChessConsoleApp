using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPChessProject
{
    public enum Color
    {
        White,
        Black
    }
    public abstract class Piece
    {

        //A protected member is accessible within its class and by derived class instances.
        public Field CurrField;
        bool IsAlive;
        int iconImageID;
        bool hasMovedOnce = false;

        //has a value in the children-classes
        public string PrintRepresentation;
        public Color PieceColor;

        //create a base constructor for instance creation of different pieces
        
        public Piece(Field aField, Color aPieceColor, bool aisAlive = true)
        {
            //takes field instance for position
            CurrField = aField;
            IsAlive = aisAlive;
            PieceColor = aPieceColor;
            
        }


        //Implement in Piece children
        public abstract List<Move> getPossibleMoves(ChessBoard cb, bool isrecursive);

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

        protected List<Move> getPossibleFieldsTraversingPieces(ChessBoard cb, List<(int, int)> directions, bool Istraverse = true)
        {
            List<Move> fList = new List<Move>();
            // get current field
            var rowNumColNum = CurrField.fieldToNum();

            //create fields and append to the List
            int r = rowNumColNum.Item1;
            int c = rowNumColNum.Item2;

            int iterMax;
            if (Istraverse) iterMax = 7; else iterMax = 1 ;


            foreach (var os in directions)
            {
                int r_n = 0;
                int c_n = 0;
                for (int i = 1; i <= iterMax; i++)
                {
                    r_n = r + os.Item1 * i;
                    c_n = c + os.Item2 * i;

                    Color EnemyCol;
                    if (cb.isRowAndColStillBoard(r_n, c_n))
                    {
                        Field fn = new Field((row)r_n, (col)c_n);

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
                            fList.Add(new Move(this.PrintRepresentation, this.CurrField, fn, MovementType.capturing));

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
                            fList.Add(new Move(this.PrintRepresentation, this.CurrField, fn, MovementType.moving));
                        }

                    }
                }
            }

            return fList;
        }
    }

    
}
