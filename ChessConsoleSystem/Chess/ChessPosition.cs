using ChessConsoleSystem.GameBoard;

namespace ChessConsoleSystem.Chess
{
    internal class ChessPosition
    {
        public char File { get; set; } // Column
        public int Rank { get; set; } // Row

        public ChessPosition(char file, int rank)
        {
            File = file;
            Rank = rank;
        }

        public Position ToPosition()
        {
            return new Position(8 - Rank, File - 'a');
        }

        public override string ToString()
        {
            return $"{File} {Rank}";
        }
    }

}
