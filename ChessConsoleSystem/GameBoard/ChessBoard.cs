﻿using ChessConsoleSystem.GameBoard.Exceptions;

namespace ChessConsoleSystem.GameBoard
{
    internal class ChessBoard
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        private Piece[,] Pieces;
        public ChessBoard(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Pieces = new Piece[Rows, Columns];
        }

        public Piece GetPiece(int row, int column)
        {
            return Pieces[row, column];
        }

        public Piece GetPiece(Position pos)
        {
            return Pieces[pos.Row, pos.Column];
        }

        public bool ExistsPiece(Position pos)
        {
            ValidPosition(pos);
            return GetPiece(pos) != null;
        }

        public void SetPiece(Piece piece, Position pos)
        {
            if (ExistsPiece(pos))
            {
                throw new PieceAlreadyExists($"Piece already exists in position [{pos.Row}, {pos.Column}]!");
            }
            Pieces[pos.Row, pos.Column] = piece;
            piece.Position = pos;
        }

        public bool IsValidPosition(Position pos)
        {
            if (pos.Row < 0 || pos.Row >= Rows || pos.Column < 0 || pos.Column >= Columns)
                return false;
            return true;
        }

        public void ValidPosition(Position pos)
        {
            if (!IsValidPosition(pos))
                throw new InvalidPositionException($"Invalid Position [{pos.Row}, {pos.Column}]!");
        }

    }
}
