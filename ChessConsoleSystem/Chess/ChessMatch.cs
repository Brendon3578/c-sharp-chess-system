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
        public Color FirstPlayerColor { get; set; }
        public Color SecondPlayerColor { get; set; }
        private HashSet<Piece> Pieces;
        private HashSet<Piece> CapturedPieces;
        public bool IsCheckmate { get; private set; }

        public ChessMatch()
        {
            Board = new ChessBoard(8, 8);
            Round = 1;

            FirstPlayerColor = Color.White;
            SecondPlayerColor = Color.Black;
            CurrentPlayerColor = FirstPlayerColor;
            Pieces = new HashSet<Piece>();
            CapturedPieces = new HashSet<Piece>();
            IsEnded = false;
            IsCheckmate = false;
            PlacePieces();
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

        private Piece? ExecutePieceMoveset(Position origin, Position end)
        {

            Piece p = Board.RemovePiece(origin);
            p.IncrementMovesAmount();
            var CapturedPiece = Board.RemovePiece(end);
            if (CapturedPiece != null)
            {
                CapturedPieces.Add(CapturedPiece);
            }
            Board.PutPiece(p, end);
            return CapturedPiece;
        }


        public void PlayerMovePiece(Position origin, Position end)
        {
            Piece? CapturedPiece = ExecutePieceMoveset(origin, end);

            if (VerifyPlayerInCheck(CurrentPlayerColor))
            {
                UndoPieceMoveset(origin, end, CapturedPiece);
                throw new CheckmateException("You can't put yourself in check");
            }

            if (VerifyPlayerInCheck(GetOpponentColor(CurrentPlayerColor)))
                IsCheckmate = true;
            else
                IsCheckmate = false;

            if (VerifyPlayerInCheckmate(GetOpponentColor(CurrentPlayerColor)))
            {
                IsEnded = true;
            }
            else
            {
                Round++;
                ChangePlayer();

            }

        }

        private void UndoPieceMoveset(Position origin, Position end, Piece? capturedPiece)
        {
            Piece p = Board.RemovePiece(end) ?? throw new InvalidPositionException("Piece don't exists in this end position!");
            p.DecrementMovesAmount();

            if (capturedPiece != null)
            {
                Board.PutPiece(capturedPiece, end);
                CapturedPieces.Remove(capturedPiece);
            }
            Board.PutPiece(p, origin);
        }

        private void ChangePlayer()
        {
            bool isFirstPlayerPlaying = CurrentPlayerColor == FirstPlayerColor;
            CurrentPlayerColor = isFirstPlayerPlaying ? SecondPlayerColor : FirstPlayerColor;
        }

        private Color GetOpponentColor(Color playerColor)
        {
            if (playerColor == FirstPlayerColor) return SecondPlayerColor;
            return FirstPlayerColor;
        }

        private Piece? GetKingPiece(Color playerColor)
        {
            return GetMatchPiecesByColor(playerColor).FirstOrDefault(player => player is King, null);
        }

        public bool VerifyPlayerInCheck(Color playerColor)
        {
            var king = GetKingPiece(playerColor) ?? throw new GameBoardException($"{playerColor} King piece does'nt exists!");

            return GetMatchPiecesByColor(GetOpponentColor(playerColor))
                .Any(p => p.GetPossibleMoveset()[king.Position.Row, king.Position.Column]);
        }

        public bool VerifyPlayerInCheckmate(Color playerColor)
        {
            if (!VerifyPlayerInCheck(playerColor))
                return false;

            foreach (var piece in GetMatchPiecesByColor(playerColor))
            {
                bool[,] moveset = piece.GetPossibleMoveset();
                for (int i = 0; i < Board.Rows; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (moveset[i, j])
                        {
                            var origin = piece.Position;
                            var end = new Position(i, j);
                            Piece? capturedPiece = ExecutePieceMoveset(origin, end);
                            bool isKingStillInCheck = VerifyPlayerInCheck(playerColor);
                            UndoPieceMoveset(origin, end, capturedPiece);
                            if (!isKingStillInCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        private void PlaceNewPiece(char file, int rank, Piece p)
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
            PlaceNewPiece('c', 1, new Rook(Board, FirstPlayerColor));
            PlaceNewPiece('d', 1, new King(Board, FirstPlayerColor));
            PlaceNewPiece('h', 7, new Rook(Board, FirstPlayerColor));


            PlaceNewPiece('a', 8, new King(Board, SecondPlayerColor));
            PlaceNewPiece('b', 8, new Rook(Board, SecondPlayerColor));



            /*PlaceNewPiece('c', 1, new Rook(Board, Color.White));
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
            PlaceNewPiece('d', 8, new King(Board, Color.Black));*/
        }
    }
}