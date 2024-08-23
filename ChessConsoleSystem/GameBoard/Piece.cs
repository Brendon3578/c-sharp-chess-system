namespace ChessConsoleSystem.GameBoard
{
    internal class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MovesAmount { get; protected set; }
        public ChessBoard ChessBoard { get; protected set; }

        public Piece(Position position, Color color, ChessBoard chessBoard)
        {
            Position = position;
            Color = color;
            ChessBoard = chessBoard;
            MovesAmount = 0;
        }
    }
}
