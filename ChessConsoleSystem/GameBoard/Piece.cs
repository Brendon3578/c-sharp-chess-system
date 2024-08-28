namespace ChessConsoleSystem.GameBoard
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MovesAmount { get; protected set; }
        public ChessBoard Board { get; protected set; }

        public Piece(ChessBoard board, Color color)
        {
            Board = board;
            Color = color;
            MovesAmount = 0;
            Position = null;
        }

        public void IncrementMovesAmount() => MovesAmount++;

        abstract public bool[,] GetPossibleMoveset();

        protected bool CanMove(Position pos)
        {
            Piece p = Board.GetPiece(pos);
            bool isFreeSpace = p == null;
            bool hasEnemyPiece = p?.Color != Color;
            return isFreeSpace || hasEnemyPiece;
        }
    }
}
