using ChessConsoleSystem.GameBoard;

namespace ChessConsoleSystem.Chess
{
    class ChessMatch
    {
        public ChessBoard Board { get; set; }
        private int Round;
        private Color ActualPlayer;
        public bool IsEnded { get; private set; }

        public ChessMatch()
        {
            Board = new ChessBoard(8, 8);
            Round = 1;
            ActualPlayer = Color.White;
            PlacePieces();
            IsEnded = false;
        }

        public void ExecuteMoveset(Position origin, Position end)
        {

            Piece p = Board.RemovePiece(origin);
            p.IncrementMovesAmount();
            var CapturedPiece = Board.RemovePiece(end);
            Board.PutPiece(p, end);
        }

        public void PlacePieces()
        {
            Board.PutPiece(new Rook(Board, Color.White), new ChessPosition('c', 1).ToPosition());
            Board.PutPiece(new Rook(Board, Color.White), new ChessPosition('c', 2).ToPosition());
            Board.PutPiece(new Rook(Board, Color.White), new ChessPosition('d', 2).ToPosition());
            Board.PutPiece(new Rook(Board, Color.White), new ChessPosition('e', 2).ToPosition());
            Board.PutPiece(new Rook(Board, Color.White), new ChessPosition('e', 1).ToPosition());
            Board.PutPiece(new King(Board, Color.White), new ChessPosition('d', 1).ToPosition());

            Board.PutPiece(new Rook(Board, Color.Black), new ChessPosition('c', 7).ToPosition());
            Board.PutPiece(new Rook(Board, Color.Black), new ChessPosition('c', 8).ToPosition());
            Board.PutPiece(new Rook(Board, Color.Black), new ChessPosition('d', 7).ToPosition());
            Board.PutPiece(new Rook(Board, Color.Black), new ChessPosition('e', 7).ToPosition());
            Board.PutPiece(new Rook(Board, Color.Black), new ChessPosition('e', 8).ToPosition());
            Board.PutPiece(new King(Board, Color.Black), new ChessPosition('d', 8).ToPosition());



        }


    }
}

public static class DebugExtensions
{
    public static string Test2D(this Array source, int pad = 10)
    {
        var result = "";
        for (int i = source.GetLowerBound(0); i <= source.GetUpperBound(0); i++)
        {
            for (int j = source.GetLowerBound(1); j <= source.GetUpperBound(1); j++)
                result += source.GetValue(i, j).ToString().PadLeft(pad);
            result += "\n";
        }
        return result;
    }
}