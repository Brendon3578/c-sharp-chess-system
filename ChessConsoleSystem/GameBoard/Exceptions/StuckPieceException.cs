namespace ChessConsoleSystem.GameBoard.Exceptions
{
    internal class StuckPieceException : GameBoardException
    {
        public StuckPieceException(string message) : base(message) { }
    }
}
