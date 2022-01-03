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
        private readonly string m_name;
        public readonly Field FromField;
        public readonly Field ToField;
        public readonly MovementType MovementType;
        //private Piece m_capturedPiece;


        public Move(string mName, Field from, Field to, MovementType mt)
        {
            this.m_name = mName;
            FromField = from;
            ToField = to;
            MovementType = mt;
            //this.m_capturedPiece = mCapturedPiece;
        }


        public override string ToString()
        {
            return (Piece: m_name,fromField: FromField, toField: ToField, movementType: MovementType).ToString();
        }


    }
}
