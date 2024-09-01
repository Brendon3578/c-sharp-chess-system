using ChessConsoleSystem.Chess;
using ChessConsoleSystem.GameBoard.Exceptions;

namespace ChessConsoleSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var round = new ChessMatch();

            while (!round.IsEnded)
            {
                try
                {
                    Console.Clear();

                    Screen.PrintChessMatch(round);

                    Console.Write("   │ -> Origin position: ");
                    var origin = Screen.ReadChessPosition().ToPosition();
                    round.ValidateOriginPosition(origin);

                    Console.Clear();
                    bool[,] possibleMoveset = round.Board.GetPiece(origin).GetPossibleMoveset();
                    Screen.PrintChessBoard(round.Board, possibleMoveset);

                    Console.WriteLine($"\n   │ Round {round.Round}\t Choose your position");

                    Console.Write("   │ -> End position: ");
                    var end = Screen.ReadChessPosition().ToPosition();

                    round.ValidateEndPosition(origin, end);

                    round.PlayerMovePiece(origin, end);
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

        }
    }
}
