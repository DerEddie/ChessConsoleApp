using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPChessProject
{
    public enum MovementType
    {
        moving, //
        movingPeaceful,
        defending, //controlling can mean capturing or potentially move to that field once the ally piece left.
        doubleStep, //important for en passant implementation
        capturing,
        castleShort,
        castleLong,
        enPassant
    }

    //TODO remove instance of Piece from board and put it in the move object. Create new instance for board
    
    public class Move
    {
        private Piece p;
        Field fromField;
        Field toField;
        public MovementType movementType;
        private Piece capturedPiece;


        public Move(string name, Field from, Field to, MovementType mt)
        {
            //pieceName = name;
            fromField = from;
            toField = to;
            movementType = mt;
        }

        public override string ToString()
        {
            return (fromField, toField, movementType).ToString();
        }

        public Field ToField
        {
            get { return toField;}
            set { toField = value; }
        }

        public Field FromField
        {
            get { return fromField; }
            set { fromField = value; }
        }

    }
}
