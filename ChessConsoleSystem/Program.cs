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

                    Console.Write("\nOrigem: ");
                    var origin = Screen.ReadChessPosition().ToPosition();

                    Console.Clear();
                    bool[,] possibleMoveset = round.Board.GetPiece(origin).GetPossibleMoveset();
                    Screen.PrintChessBoard(round.Board, possibleMoveset);


                    Console.Write("\nDestino: ");
                    var end = Screen.ReadChessPosition().ToPosition();

                    round.ExecuteMoveset(origin, end);
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
