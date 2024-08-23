namespace ChessConsoleSystem.GameBoard.Exceptions
{
    internal class InvalidPositionException : GameBoardException
    {
        public InvalidPositionException(string message) : base(message) { }
    }
}
