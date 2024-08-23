namespace ChessConsoleSystem.GameBoard.Exceptions
{
    internal class PieceAlreadyExists : GameBoardException
    {
        public PieceAlreadyExists(string message) : base(message) { }
    }
}
