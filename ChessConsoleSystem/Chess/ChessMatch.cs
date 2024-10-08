﻿using ChessConsoleSystem.GameBoard;
using ChessConsoleSystem.GameBoard.Exceptions;

namespace ChessConsoleSystem.Chess
{
    class ChessMatch
    {
        public ChessBoard Board { get; }
        public int Round { get; private set; }
        public Color CurrentPlayerColor { get; private set; }
        public bool IsEnded { get; private set; }

        private HashSet<Piece> _pieces;
        private HashSet<Piece> _capturedPieces;
        public bool IsCheckmate { get; private set; }
        public Piece? _vulnerablePieceForEnPassantMove { get; private set; }

        public ChessMatch()
        {
            Board = new ChessBoard(8, 8);
            Round = 1;

            Board.FirstPlayerColor = Color.White;
            Board.SecondPlayerColor = Color.Black;
            CurrentPlayerColor = Board.FirstPlayerColor;
            _pieces = new HashSet<Piece>();
            _capturedPieces = new HashSet<Piece>();
            IsEnded = false;
            IsCheckmate = false;
            _vulnerablePieceForEnPassantMove = null;
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

            if (!originPiece.IsPossibleMove(end))
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
                _capturedPieces.Add(CapturedPiece);
            }
            Board.PutPiece(p, end);

            bool isShortCastling = p is King && end.Column == origin.Column + 2;
            bool isLongCastling = p is King && end.Column == origin.Column - 2;
            if (isShortCastling)
            {
                // #special move - short castling or kingside (roque pequeno)
                Position rookOrigin = new(origin.Row, origin.Column + 3);
                Position rookEnd = new(origin.Row, origin.Column + 1);

                Piece Rook = Board.RemovePiece(rookOrigin) ?? throw new InvalidPositionException("Unable to do castling!");
                Rook.IncrementMovesAmount();
                Board.PutPiece(Rook, rookEnd);
            }
            else if (isLongCastling)
            {
                // #special move - long castling or queenside (roque grande)
                Position rookOrigin = new(origin.Row, origin.Column - 4);
                Position rookEnd = new(origin.Row, origin.Column - 1);

                Piece Rook = Board.RemovePiece(rookOrigin) ?? throw new InvalidPositionException("Unable to do castling!");
                Rook.IncrementMovesAmount();
                Board.PutPiece(Rook, rookEnd);
            }

            // #special move - En Passant
            if (p is Pawn && origin.Column != end.Column && CapturedPiece == null)
            {
                // Verifica se o movimento é válido para En Passant
                Position enPassantPawnPosition = GetEnPassantCapturePosition(p, end);
                if (_vulnerablePieceForEnPassantMove == Board.GetPiece(enPassantPawnPosition))
                {
                    CapturedPiece = Board.RemovePiece(enPassantPawnPosition);
                    _capturedPieces.Add(CapturedPiece ?? throw new InvalidPositionException("Piece doesn't exist for En Passant move!"));
                }
            }



            return CapturedPiece;
        }

        private Position GetEnPassantCapturePosition(Piece p, Position end)
        {
            return p.Color == Board.FirstPlayerColor
                   ? new Position(end.Row + 1, end.Column) // Peão branco captura para cima
                   : new Position(end.Row - 1, end.Column); // Peão preto captura para baixo
        }


        public void PlayerMovePiece(Position origin, Position end)
        {
            Piece? CapturedPiece = ExecutePieceMoveset(origin, end);

            Piece? movedPiece = Board.GetPiece(end);


            if (IsPlayerInCheck(CurrentPlayerColor))
            {
                UndoPieceMoveset(origin, end, CapturedPiece);
                throw new CheckmateException("You can't put yourself in check");
            }

            // #special move - promotion
            if (movedPiece is Pawn)
            {
                bool isFirstPlayerPromotion = movedPiece.Color == Board.FirstPlayerColor && end.Row == 0;
                bool isSecondPlayerPromotion = movedPiece.Color == Board.SecondPlayerColor && end.Row == 7;

                if (isFirstPlayerPromotion || isSecondPlayerPromotion)
                {
                    movedPiece = Board.RemovePiece(end);
                    _pieces.Remove(movedPiece);
                    Piece queen = new Queen(Board, movedPiece.Color);
                    Board.PutPiece(queen, end);
                    _pieces.Add(queen);

                }
            }

            if (IsPlayerInCheck(GetOpponentColor(CurrentPlayerColor)))
                IsCheckmate = true;
            else
                IsCheckmate = false;

            if (IsPlayerInCheckmate(GetOpponentColor(CurrentPlayerColor)))
            {
                IsEnded = true;
            }
            else
            {
                Round++;
                ChangePlayer();
            }


            // #special move - Pawn En Passsant
            bool isPawnFirstMove = end.Row == origin.Row - 2 || end.Row == origin.Row + 2;
            if (movedPiece is Pawn && isPawnFirstMove)
            {
                _vulnerablePieceForEnPassantMove = movedPiece;
            }

        }

        private void UndoPieceMoveset(Position origin, Position end, Piece? capturedPiece)
        {
            Piece p = Board.RemovePiece(end) ?? throw new InvalidPositionException("Piece don't exists in this end position!");
            p.DecrementMovesAmount();

            if (capturedPiece != null)
            {
                Board.PutPiece(capturedPiece, end);
                _capturedPieces.Remove(capturedPiece);
            }
            Board.PutPiece(p, origin);

            bool isShortCastling = p is King && end.Column == origin.Column + 2;
            bool isLongCastling = p is King && end.Column == origin.Column - 2;
            if (isShortCastling)
            {
                // #special move - short castling or kingside (roque pequeno)
                Position rookOrigin = new(origin.Row, origin.Column + 3);
                Position rookEnd = new(origin.Row, origin.Column + 1);

                Piece Rook = Board.RemovePiece(rookEnd) ?? throw new InvalidPositionException("Unable to do castling!");
                Rook.DecrementMovesAmount();
                Board.PutPiece(Rook, rookOrigin);
            }
            else if (isLongCastling)
            {
                // #special move - long castling or queenside (roque grande)
                Position rookOrigin = new(origin.Row, origin.Column - 4);
                Position rookEnd = new(origin.Row, origin.Column - 1);

                Piece Rook = Board.RemovePiece(rookEnd) ?? throw new InvalidPositionException("Unable to do castling!");
                Rook.IncrementMovesAmount();
                Board.PutPiece(Rook, rookOrigin);
            }


            // #special move - En Passsant
            bool isEnPassant = p is Pawn && origin.Row != end.Row && capturedPiece == _vulnerablePieceForEnPassantMove;
            if (isEnPassant)
            {
                var enemyPiece = Board.RemovePiece(end);
                Position? lastEnemyPosition = null;
                if (p.Color == Board.FirstPlayerColor)
                    lastEnemyPosition = new Position(3, end.Column);
                else if (p.Color == Board.SecondPlayerColor)
                    lastEnemyPosition = new Position(4, end.Column);

                if (enemyPiece == null || lastEnemyPosition == null)
                    throw new InvalidPositionException("Can't to undo En Passant move!");

                Board.PutPiece(enemyPiece, lastEnemyPosition);

            }
        }

        private void ChangePlayer()
        {
            CurrentPlayerColor = GetOpponentColor(CurrentPlayerColor);
        }

        private Color GetOpponentColor(Color playerColor)
        {
            return playerColor == Board.FirstPlayerColor ? Board.SecondPlayerColor : Board.FirstPlayerColor;
        }

        private Piece? GetKingPiece(Color playerColor)
        {
            return GetMatchPiecesByColor(playerColor).FirstOrDefault(p => p is King, null);
        }

        public bool IsPlayerInCheck(Color playerColor)
        {
            var king = GetKingPiece(playerColor) ?? throw new GameBoardException($"{playerColor} King piece does'nt exists!");
            return GetMatchPiecesByColor(GetOpponentColor(playerColor))
                .Any(p => p.GetPossibleMoveset()[king.Position.Row, king.Position.Column]);
        }

        public bool IsPlayerInCheckmate(Color playerColor)
        {
            if (!IsPlayerInCheck(playerColor))
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
                            bool isKingStillInCheck = IsPlayerInCheck(playerColor);
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
            _pieces.Add(p);
        }

        public HashSet<Piece> GetMatchPiecesByColor(Color color)
        {
            return _pieces.Where(p => p.Color == color && !_capturedPieces.Contains(p)).ToHashSet();
        }

        public HashSet<Piece> GetCapturedPiecesByColor(Color color)
        {
            return _capturedPieces.Where(p => p.Color == color).ToHashSet();
        }

        public void PlacePieces()
        {
            // Place pieces for white pieces (first player)
            PlaceNewPiece('a', 1, new Rook(Board, Board.FirstPlayerColor));
            PlaceNewPiece('b', 1, new Knight(Board, Board.FirstPlayerColor));
            PlaceNewPiece('c', 1, new Bishop(Board, Board.FirstPlayerColor));
            PlaceNewPiece('d', 1, new Queen(Board, Board.FirstPlayerColor));
            PlaceNewPiece('e', 1, new King(Board, Board.FirstPlayerColor, this));
            PlaceNewPiece('f', 1, new Bishop(Board, Board.FirstPlayerColor));
            PlaceNewPiece('g', 1, new Knight(Board, Board.FirstPlayerColor));
            PlaceNewPiece('h', 1, new Rook(Board, Board.FirstPlayerColor));

            // Place pawns for white pieces (first player)
            for (char file = 'a'; file <= 'h'; file++)
            {
                PlaceNewPiece(file, 2, new Pawn(Board, Board.FirstPlayerColor, this));
            }

            // Place pieces for black pieces (second player)
            PlaceNewPiece('a', 8, new Rook(Board, Board.SecondPlayerColor));
            PlaceNewPiece('b', 8, new Knight(Board, Board.SecondPlayerColor));
            PlaceNewPiece('c', 8, new Bishop(Board, Board.SecondPlayerColor));
            PlaceNewPiece('d', 8, new Queen(Board, Board.SecondPlayerColor));
            PlaceNewPiece('e', 8, new King(Board, Board.SecondPlayerColor, this));
            PlaceNewPiece('f', 8, new Bishop(Board, Board.SecondPlayerColor));
            PlaceNewPiece('g', 8, new Knight(Board, Board.SecondPlayerColor));
            PlaceNewPiece('h', 8, new Rook(Board, Board.SecondPlayerColor));

            // Place pawns for black pieces (second player)
            for (char file = 'a'; file <= 'h'; file++)
            {
                PlaceNewPiece(file, 7, new Pawn(Board, Board.SecondPlayerColor, this));
            }
        }
    }
}