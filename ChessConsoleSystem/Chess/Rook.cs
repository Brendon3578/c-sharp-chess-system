using ChessConsoleSystem.GameBoard;

namespace ChessConsoleSystem.Chess
{
    internal class Rook : Piece
    {

        public Rook(ChessBoard board, Color color) : base(board, color) { }

        public override string ToString()
        {
            return "R";
        }
    }
}
