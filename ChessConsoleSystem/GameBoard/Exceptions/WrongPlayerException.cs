namespace ChessConsoleSystem.GameBoard.Exceptions
{
    internal class WrongPlayerException : GameBoardException
    {
        public WrongPlayerException(string message) : base(message) { }
    }
}
