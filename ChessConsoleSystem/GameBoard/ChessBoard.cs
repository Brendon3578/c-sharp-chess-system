namespace ChessConsoleSystem.GameBoard
{
    internal class ChessBoard
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        private Piece[,] Pieces;
        public ChessBoard(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Pieces = new Piece[Rows, Columns];
        }

        public Piece GetPiece(int row, int column)
        {
            return Pieces[row, column];
        }

        public void SetPiece(Piece piece, Position pos)
        {
            Pieces[pos.Row, pos.Column] = piece;
            piece.Position = pos;
        }


    }
}
