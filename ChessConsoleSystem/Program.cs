using ChessConsoleSystem.Chess;
using ChessConsoleSystem.GameBoard.Exceptions;

namespace ChessConsoleSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var round = new ChessMatch();

                while (!round.IsEnded)
                {
                    Console.Clear();
                    Screen.PrintChessBoard(round.Board);
                    Console.WriteLine($"\n   │ Round {round.Round}\t Await for {round.ActualPlayer.ToString().ToUpper()} pieces play");

                    Console.Write("   | -> Origin position: ");

                    var origin = Screen.ReadChessPosition().ToPosition();

                    Console.Clear();
                    bool[,] possibleMoveset = round.Board.GetPiece(origin).GetPossibleMoveset();
                    Screen.PrintChessBoard(round.Board, possibleMoveset);

                    Console.WriteLine($"\n   │ Round {round.Round}\t Choose your position");

                    Console.Write("   | -> End position: ");
                    var end = Screen.ReadChessPosition().ToPosition();

                    round.StartMatchTurn(origin, end);
                }
            }
            catch (GameBoardException ex)
            {
                Console.WriteLine($"{ex.GetType().Name} Error! - {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.GetType().Name} Error! - {ex.Message}");
            }

        }
    }
}
