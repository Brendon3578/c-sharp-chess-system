namespace ChessConsoleSystem.GameBoard.Exceptions
{
    internal class CheckmateException : GameBoardException
    {
        public CheckmateException(string message) : base(message) { }
    }
}
