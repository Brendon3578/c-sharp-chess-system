using ChessConsoleSystem.GameBoard;

namespace ChessConsoleSystem.Chess
{
    internal class Rook : Piece
    {

        public Rook(ChessBoard board, Color color) : base(board, color) { }

        public override string ToString()
        {
            return "R";
        }



        public override bool[,] GetPossibleMoveset()
        {
            bool[,] moveset = new bool[Board.Rows, Board.Columns];

            var move = new Position(0, 0);

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


            return moveset;
        }
    }
}
