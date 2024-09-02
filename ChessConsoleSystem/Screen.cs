using ChessConsoleSystem.Chess;
using ChessConsoleSystem.GameBoard;

namespace ChessConsoleSystem
{
    internal class Screen
    {

        public static void PrintChessMatch(ChessMatch match)
        {
            PrintChessBoard(match.Board);
            PrintCapturedPieces(match);

            Console.WriteLine($"\n   ┌ Round {match.Round}");

            if (match.IsEnded)
            {
                Console.WriteLine("   │ Checkmate!");
                Console.WriteLine($"   └ # The winner is {match.CurrentPlayerColor}");
                return;
            }
            Console.Write("   │ Await for ");
            Console.ForegroundColor = ConvertPieceColorToConsoleColor(match.CurrentPlayerColor);
            Console.Write(match.CurrentPlayerColor.ToString().ToUpperInvariant());
            Console.ResetColor();
            Console.WriteLine(" pieces to play");

            if (match.IsCheckmate)
            {
                Console.WriteLine("   │ # In check!");
            }
        }

        public static void PrintChessBoard(ChessBoard board, bool[,]? possibleMoveset = null)
        {
            PrintBoardHeader();
            for (int r = 0; r < board.Rows; r++) // rows
            {
                Console.Write($" {8 - r} ");
                WriteBoardLedge();
                Console.Write(" ");
                for (int c = 0; c < board.Columns; c++) // columns
                {
                    if (possibleMoveset != null)
                    {
                        bool pieceCanMoveInPosition = possibleMoveset[r, c];
                        PrintSinglePiece(board.GetPiece(r, c), pieceCanMoveInPosition);
                    }
                    else
                    {
                        PrintSinglePiece(board.GetPiece(r, c));
                    }
                }
                WriteBoardLedge();
                Console.WriteLine();
            }
            PrintBoardFooter();
            Console.WriteLine("     a b c d e f g h");
        }

        public static void PrintCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("   ┌ Captured Pieces");
            Console.Write($"   │ {match.FirstPlayerColor}:");
            PrintPiecesSet(match.GetCapturedPiecesByColor(match.FirstPlayerColor));
            Console.Write($"   │ {match.SecondPlayerColor}:");
            PrintPiecesSet(match.GetCapturedPiecesByColor(match.SecondPlayerColor));
        }

        public static void PrintPiecesSet(HashSet<Piece> piecesSet)
        {
            Console.Write("[ ");
            foreach (var piece in piecesSet)
            {
                WritePiece(piece);
            }
            Console.WriteLine("]");
        }

        public static void WritePiece(Piece p)
        {
            Console.ForegroundColor = ConvertPieceColorToConsoleColor(p.Color);
            Console.Write($"{p} ");
            Console.ResetColor();
        }


        public static void PrintSinglePiece(Piece p, bool moveablePosition = false)
        {
            if (moveablePosition)
                Console.BackgroundColor = ConsoleColor.DarkGray;
            if (p == null)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("- ");
                Console.ResetColor();
            }
            else
            {
                WritePiece(p);
            }
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

        public static void PrintBoardHeader()
        {
            Console.WriteLine("   ┌─────────────────┐");
        }
        public static void PrintBoardFooter()
        {
            Console.WriteLine("   └─────────────────┘");
        }

        public static void WriteBoardLedge()
        {
            Console.Write("│");
        }
    }
}
