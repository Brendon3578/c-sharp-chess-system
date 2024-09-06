using ChessConsoleSystem.GameBoard;

namespace ChessConsoleSystem.Chess
{
    internal class Queen : Piece
    {

        public Queen(ChessBoard board, Color color) : base(board, color) { }

        public override string ToString()
        {
            return "Q";
        }

        public override bool[,] GetPossibleMoveset()
        {
            bool[,] moveset = new bool[Board.Rows, Board.Columns];

            var move = new Position(0, 0);

            // northwest 
            move.SetValues(Position.Row - 1, Position.Column - 1);
            while (Board.IsValidPosition(move) && CanMove(move))
            {
                moveset[move.Row, move.Column] = true;
                if (Board.GetPiece(move) != null && Board.GetPiece(move)?.Color != Color)
                    break;
                move.SetValues(move.Row - 1, move.Column - 1);
            }

            // above
            move.SetValues(Position.Row - 1, Position.Column);
            while (Board.IsValidPosition(move) && CanMove(move))
            {
                moveset[move.Row, move.Column] = true;
                var p = Board.GetPiece(move);
                bool isFreeSpace = p != null;
                bool hasEnemyPiece = p?.Color != Color;

                if (isFreeSpace && hasEnemyPiece)
                    break;
                move.Row = move.Row - 1;
            }

            // northeast
            move.SetValues(Position.Row - 1, Position.Column + 1);
            while (Board.IsValidPosition(move) && CanMove(move))
            {
                moveset[move.Row, move.Column] = true;
                if (Board.GetPiece(move) != null && Board.GetPiece(move)?.Color != Color)
                    break;
                move.SetValues(move.Row - 1, move.Column + 1);
            }

            // left
            move.SetValues(Position.Row, Position.Column - 1);
            while (Board.IsValidPosition(move) && CanMove(move))
            {
                moveset[move.Row, move.Column] = true;
                var p = Board.GetPiece(move);
                bool isFreeSpace = p != null;
                bool hasEnemyPiece = p?.Color != Color;

                if (isFreeSpace && hasEnemyPiece)
                    break;
                move.Column = move.Column - 1;
            }

            // right
            move.SetValues(Position.Row, Position.Column + 1);
            while (Board.IsValidPosition(move) && CanMove(move))
            {
                moveset[move.Row, move.Column] = true;
                var p = Board.GetPiece(move);
                bool isFreeSpace = p != null;
                bool hasEnemyPiece = p?.Color != Color;

                if (isFreeSpace && hasEnemyPiece)
                    break;
                move.Column = move.Column + 1;
            }

            // southwest
            move.SetValues(Position.Row + 1, Position.Column + 1);
            while (Board.IsValidPosition(move) && CanMove(move))
            {
                moveset[move.Row, move.Column] = true;
                if (Board.GetPiece(move) != null && Board.GetPiece(move)?.Color != Color)
                    break;
                move.SetValues(move.Row + 1, move.Column + 1);
            }

            // below
            move.SetValues(Position.Row + 1, Position.Column);
            while (Board.IsValidPosition(move) && CanMove(move))
            {
                moveset[move.Row, move.Column] = true;
                var p = Board.GetPiece(move);
                bool isFreeSpace = p != null;
                bool hasEnemyPiece = p?.Color != Color;

                if (isFreeSpace && hasEnemyPiece)
                    break;
                move.Row = move.Row + 1;
            }


            // southeast
            move.SetValues(Position.Row + 1, Position.Column - 1);
            while (Board.IsValidPosition(move) && CanMove(move))
            {
                moveset[move.Row, move.Column] = true;
                if (Board.GetPiece(move) != null && Board.GetPiece(move)?.Color != Color)
                    break;
                move.SetValues(move.Row + 1, move.Column - 1);
            }

            return moveset;
        }
    }
}
