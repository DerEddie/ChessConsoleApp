namespace Chess
{
    public enum MovementType
    {
        Moving, //
        MovingPeaceful,
        Defending, //controlling can mean capturing or potentially move to that field once the ally piece left.
        DoubleStep, //important for en passant implementation
        Capturing,
        CastleShort,
        CastleLong,
        EnPassant,
        Promotion
    }

    
    public class Move
    {
        private readonly string _name;
        public Field FromField;
        public Field ToField;
        public MovementType MovementType;
        //private Piece m_capturedPiece;


        public Move(string name, Field from, Field to, MovementType mt)
        {
            this._name = name;
            FromField = from;
            ToField = to;
            MovementType = mt;
            //this.m_capturedPiece = mCapturedPiece;
        }


        public override string ToString()
        {
            return (Piece: _name,fromField: FromField, toField: ToField, movementType: MovementType).ToString();
        }


    }
}
