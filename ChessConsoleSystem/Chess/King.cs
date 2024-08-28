using ChessConsoleSystem.GameBoard;

namespace ChessConsoleSystem.Chess
{
    internal class King : Piece
    {

        public King(ChessBoard board, Color color) : base(board, color) { }

        public override string ToString()
        {
            return "K";
        }

        public override bool[,] GetPossibleMoveset()
        {
            bool[,] moveset = new bool[Board.Rows, Board.Columns];

            var move = new Position(0, 0);

            // northwest 
            move.SetValues(Position.Row - 1, Position.Column - 1);
            if (Board.IsValidPosition(move) && CanMove(move))
                moveset[move.Row, move.Column] = true;

            // above
            move.SetValues(Position.Row - 1, Position.Column);
            if (Board.IsValidPosition(move) && CanMove(move))
                moveset[move.Row, move.Column] = true;

            // northeast
            move.SetValues(Position.Row - 1, Position.Column + 1);
            if (Board.IsValidPosition(move) && CanMove(move))
                moveset[move.Row, move.Column] = true;

            // left
            move.SetValues(Position.Row, Position.Column - 1);
            if (Board.IsValidPosition(move) && CanMove(move))
                moveset[move.Row, move.Column] = true;

            // right
            move.SetValues(Position.Row, Position.Column + 1);
            if (Board.IsValidPosition(move) && CanMove(move))
                moveset[move.Row, move.Column] = true;

            // southwest 
            move.SetValues(Position.Row + 1, Position.Column - 1);
            if (Board.IsValidPosition(move) && CanMove(move))
                moveset[move.Row, move.Column] = true;

            // below
            move.SetValues(Position.Row + 1, Position.Column);
            if (Board.IsValidPosition(move) && CanMove(move))
                moveset[move.Row, move.Column] = true;

            // southeast
            move.SetValues(Position.Row + 1, Position.Column + 1);
            if (Board.IsValidPosition(move) && CanMove(move))
                moveset[move.Row, move.Column] = true;

            return moveset;
        }
    }
}
