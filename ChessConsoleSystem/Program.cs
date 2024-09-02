using ChessConsoleSystem.Chess;
using ChessConsoleSystem.GameBoard.Exceptions;

namespace ChessConsoleSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var match = new ChessMatch();

            while (!match.IsEnded)
            {
                try
                {
                    Console.Clear();

                    Screen.PrintChessMatch(match);

                    Console.Write("   │ -> Origin position: ");
                    var origin = Screen.ReadChessPosition().ToPosition();
                    match.ValidateOriginPosition(origin);

                    Console.Clear();
                    bool[,] possibleMoveset = match.Board.GetPiece(origin).GetPossibleMoveset();
                    Screen.PrintChessBoard(match.Board, possibleMoveset);

                    Console.WriteLine($"\n   │ match {match.Round}\t Choose your position");

                    Console.Write("   │ -> End position: ");
                    var end = Screen.ReadChessPosition().ToPosition();

                    match.ValidateEndPosition(origin, end);

                    match.PlayerMovePiece(origin, end);
                }
                catch (GameBoardException ex)
                {
                    Console.WriteLine($"   > ! Ooops... < {ex.Message} >");
                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"   > ! Error < {ex.Message} >");
                    Console.ReadLine();
                }
            }
            Console.Clear();
            Screen.PrintChessMatch(match);
        }
    }
}
