using ChessConsoleSystem.GameBoard;

namespace ChessConsoleSystem.Chess
{
    internal class Knight : Piece
    {

        public Knight(ChessBoard board, Color color) : base(board, color) { }

        public override string ToString()
        {
            return "N";
        }

        public override bool[,] GetPossibleMoveset()
        {
            bool[,] moveset = new bool[Board.Rows, Board.Columns];

            var move = new Position(0, 0);

            move.SetValues(Position.Row - 1, Position.Column - 2);
            if (Board.IsValidPosition(move) && CanMove(move))
                moveset[move.Row, move.Column] = true;

            move.SetValues(Position.Row - 2, Position.Column - 1);
            if (Board.IsValidPosition(move) && CanMove(move))
                moveset[move.Row, move.Column] = true;

            move.SetValues(Position.Row - 2, Position.Column + 1);
            if (Board.IsValidPosition(move) && CanMove(move))
                moveset[move.Row, move.Column] = true;

            move.SetValues(Position.Row - 1, Position.Column + 2);
            if (Board.IsValidPosition(move) && CanMove(move))
                moveset[move.Row, move.Column] = true;

            move.SetValues(Position.Row + 1, Position.Column + 2);
            if (Board.IsValidPosition(move) && CanMove(move))
                moveset[move.Row, move.Column] = true;

            move.SetValues(Position.Row + 2, Position.Column + 1);
            if (Board.IsValidPosition(move) && CanMove(move))
                moveset[move.Row, move.Column] = true;

            move.SetValues(Position.Row + 2, Position.Column - 1);
            if (Board.IsValidPosition(move) && CanMove(move))
                moveset[move.Row, move.Column] = true;

            move.SetValues(Position.Row + 1, Position.Column - 2);
            if (Board.IsValidPosition(move) && CanMove(move))
                moveset[move.Row, move.Column] = true;


            return moveset;
        }
    }
}
