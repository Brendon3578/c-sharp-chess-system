using ChessConsoleSystem.GameBoard;

namespace ChessConsoleSystem.Chess
{
    internal class Bishop : Piece
    {

        public Bishop(ChessBoard board, Color color) : base(board, color) { }

        public override string ToString()
        {
            return "B";
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

            // northeast
            move.SetValues(Position.Row - 1, Position.Column + 1);
            while (Board.IsValidPosition(move) && CanMove(move))
            {
                moveset[move.Row, move.Column] = true;
                if (Board.GetPiece(move) != null && Board.GetPiece(move)?.Color != Color)
                    break;
                move.SetValues(move.Row - 1, move.Column + 1);
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
