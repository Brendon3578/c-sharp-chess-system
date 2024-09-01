using ChessConsoleSystem.GameBoard;
using ChessConsoleSystem.GameBoard.Exceptions;

namespace ChessConsoleSystem.Chess
{
    class ChessMatch
    {
        public ChessBoard Board { get; }
        public int Round { get; private set; }
        public Color CurrentPlayerColor { get; private set; }
        public bool IsEnded { get; private set; }
        private HashSet<Piece> Pieces;
        private HashSet<Piece> CapturedPieces;

        public ChessMatch()
        {
            Board = new ChessBoard(8, 8);
            Round = 1;
            CurrentPlayerColor = Color.White;
            Pieces = new HashSet<Piece>();
            CapturedPieces = new HashSet<Piece>();
            IsEnded = false;
            PlacePieces();
        }

        public void StartMatchTurn(Position origin, Position end)
        {
            ExecuteMoveset(origin, end);
            Round++;
            ChangePlayer();
        }

        public void ValidateOriginPosition(Position origin)
        {
            var p = Board.GetPiece(origin) ?? throw new InvalidPositionException("Piece don't exists in this origin position!");
            if (CurrentPlayerColor != p.Color)
            {
                throw new WrongPlayerException("The origin chosen piece is not yours!");
            }
            if (!p.ExistsPossibleMovesets())
            {
                throw new StuckPieceException("There are no possible moves to move the origin piece!");
            }
        }

        public void ValidateEndPosition(Position origin, Position end)
        {
            var originPiece = Board.GetPiece(origin) ?? throw new InvalidPositionException("Piece don't exists in this origin position!");

            if (!originPiece.IsPossibleToMoveTo(end))
            {
                throw new InvalidPositionException("You cant move for this end position!");
            }
        }

        private void ExecuteMoveset(Position origin, Position end)
        {

            Piece p = Board.RemovePiece(origin);
            p.IncrementMovesAmount();
            var CapturedPiece = Board.RemovePiece(end);
            if (CapturedPiece != null)
            {
                CapturedPieces.Add(CapturedPiece);
            }
            Board.PutPiece(p, end);
        }

        private void ChangePlayer()
        {
            if (CurrentPlayerColor == Color.White)
                CurrentPlayerColor = Color.Black;
            else
                CurrentPlayerColor = Color.White;
        }

        public void PlaceNewPiece(char file, int rank, Piece p)
        {
            Board.PutPiece(p, new ChessPosition(file, rank).ToPosition());
            Pieces.Add(p);
        }

        public HashSet<Piece> GetMatchPiecesByColor(Color color)
        {
            return Pieces.Where(p => p.Color == color && !CapturedPieces.Contains(p)).ToHashSet();
        }

        public HashSet<Piece> GetCapturedPiecesByColor(Color color)
        {
            return CapturedPieces.Where(p => p.Color == color).ToHashSet();
        }

        public void PlacePieces()
        {
            PlaceNewPiece('c', 1, new Rook(Board, Color.White));
            PlaceNewPiece('c', 2, new Rook(Board, Color.White));
            PlaceNewPiece('d', 2, new Rook(Board, Color.White));
            PlaceNewPiece('e', 2, new Rook(Board, Color.White));
            PlaceNewPiece('e', 1, new Rook(Board, Color.White));
            PlaceNewPiece('d', 1, new King(Board, Color.White));

            PlaceNewPiece('c', 7, new Rook(Board, Color.Black));
            PlaceNewPiece('c', 8, new Rook(Board, Color.Black));
            PlaceNewPiece('d', 7, new Rook(Board, Color.Black));
            PlaceNewPiece('e', 7, new Rook(Board, Color.Black));
            PlaceNewPiece('e', 8, new Rook(Board, Color.Black));
            PlaceNewPiece('d', 8, new King(Board, Color.Black));
        }
    }
}