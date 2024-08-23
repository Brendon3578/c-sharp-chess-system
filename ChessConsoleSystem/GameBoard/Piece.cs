namespace ChessConsoleSystem.GameBoard
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MovesAmount { get; protected set; }
        public ChessBoard ChessBoard { get; protected set; }

        public Piece(ChessBoard chessBoard, Color color)
        {
            ChessBoard = chessBoard;
            Color = color;
            MovesAmount = 0;
            Position = null;
        }
    }
}
