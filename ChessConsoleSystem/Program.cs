using ChessConsoleSystem.GameBoard;

namespace ChessConsoleSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Position pawnPosition = new Position(2, 2);
            Console.WriteLine($"PawnPosition: {pawnPosition}");
        }
    }
}
