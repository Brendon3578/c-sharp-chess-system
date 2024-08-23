using ChessConsoleSystem.GameBoard;

namespace ChessConsoleSystem
{
    internal class Screen
    {
        public static void PrintChessBoard(ChessBoard board)
        {
            for (int r = 0; r < board.Rows; r++) // rows
            {
                for (int c = 0; c < board.Columns; c++) // columns
                {
                    if (board.GetPiece(r, c) == null)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("- ");

                    }
                    else
                    {
                        Console.ResetColor();
                        PrintSinglePiece(board.GetPiece(r, c));
                    }
                }
                Console.WriteLine();
            }
        }

        public static void PrintSinglePiece(Piece p)
        {
            if (p == null)
            {
                Console.Write("- ");
            }
            else
            {
                Console.ForegroundColor = ConvertPieceColorToConsoleColor(p.Color);
                Console.Write($"{p} ");
            }
        }

        public static ConsoleColor ConvertPieceColorToConsoleColor(Color color)
        {
            return color switch
            {
                Color.Black => ConsoleColor.Gray,
                Color.White => ConsoleColor.White,
                Color.Red => ConsoleColor.Red,
                Color.Brown => ConsoleColor.DarkYellow,
                _ => ConsoleColor.White
            };
        }
    }
}
