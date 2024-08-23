using ChessConsoleSystem.Chess;
using ChessConsoleSystem.GameBoard;

namespace ChessConsoleSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ChessBoard chessBoard = new ChessBoard(8, 8);

            chessBoard.SetPiece(new Rook(chessBoard, Color.Red), new Position(0, 0));
            chessBoard.SetPiece(new Rook(chessBoard, Color.Brown), new Position(1, 3));
            chessBoard.SetPiece(new King(chessBoard, Color.Black), new Position(2, 4));

            Screen.PrintChessBoard(chessBoard);
        }
    }
}
