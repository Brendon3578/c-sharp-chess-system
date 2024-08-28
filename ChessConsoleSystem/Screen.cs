using ChessConsoleSystem.Chess;
using ChessConsoleSystem.GameBoard;

namespace ChessConsoleSystem
{
    internal class Screen
    {
        public static void PrintChessBoard(ChessBoard board)
        {
            for (int r = 0; r < board.Rows; r++) // rows
            {
                Console.Write($"{8 - r} ");
                for (int c = 0; c < board.Columns; c++) // columns
                {
                    PrintSinglePiece(board.GetPiece(r, c));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void PrintChessBoard(ChessBoard board, bool[,] possibleMoveset)
        {
            for (int r = 0; r < board.Rows; r++) // rows
            {
                Console.Write($"{8 - r} ");
                for (int c = 0; c < board.Columns; c++) // columns
                {
                    bool pieceCanMoveInPosition = possibleMoveset[r, c];
                    PrintSinglePiece(board.GetPiece(r, c), pieceCanMoveInPosition);
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }


        public static void PrintSinglePiece(Piece p, bool moveablePosition = false)
        {
            if (moveablePosition)
                Console.BackgroundColor = ConsoleColor.DarkGray;
            if (p == null)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("- ");
            }
            else
            {
                Console.ForegroundColor = ConvertPieceColorToConsoleColor(p.Color);
                Console.Write($"{p} ");
            }
            Console.ResetColor();
        }

        public static ConsoleColor ConvertPieceColorToConsoleColor(Color color)
        {
            return color switch
            {
                Color.Black => ConsoleColor.DarkYellow,
                Color.White => ConsoleColor.White,
                Color.Red => ConsoleColor.Red,
                Color.Brown => ConsoleColor.DarkRed,
                _ => ConsoleColor.White
            };
        }

        public static ChessPosition ReadChessPosition()
        {
            string s = Console.ReadLine();
            char file = s[0];
            int rank = int.Parse(s[1] + "");
            return new ChessPosition(file, rank);
        }
    }
}
