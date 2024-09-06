using ChessConsoleSystem.GameBoard;

namespace ChessConsoleSystem.Chess
{
    internal class Pawn : Piece
    {

        public Pawn(ChessBoard board, Color color) : base(board, color) { }

        public override string ToString()
        {
            return "P";
        }

        private bool ExistsEnemy(Position pos)
        {
            Piece? p = Board.GetPiece(pos);
            return p != null && p.Color != Color;
        }

        private bool IsFreePosition(Position pos)
        {
            return Board.GetPiece(pos) == null;
        }

        public override bool[,] GetPossibleMoveset()
        {
            bool[,] moveset = new bool[Board.Rows, Board.Columns];

            var move = new Position(0, 0);

            if (Color == Board.FirstPlayerColor)
            {
                move.SetValues(Position.Row - 1, Position.Column);
                if (Board.IsValidPosition(move) && IsFreePosition(move))
                    moveset[move.Row, move.Column] = true;

                move.SetValues(Position.Row - 2, Position.Column);
                if (Board.IsValidPosition(move) && IsFreePosition(move) && MovesAmount == 0)
                    moveset[move.Row, move.Column] = true;

                move.SetValues(Position.Row - 1, Position.Column - 1);
                if (Board.IsValidPosition(move) && ExistsEnemy(move))
                    moveset[move.Row, move.Column] = true;

                move.SetValues(Position.Row - 1, Position.Column + 1);
                if (Board.IsValidPosition(move) && ExistsEnemy(move))
                    moveset[move.Row, move.Column] = true;

            }
            else if (Color == Board.SecondPlayerColor)
            {
                move.SetValues(Position.Row + 1, Position.Column);
                if (Board.IsValidPosition(move) && IsFreePosition(move))
                    moveset[move.Row, move.Column] = true;

                move.SetValues(Position.Row + 2, Position.Column);
                if (Board.IsValidPosition(move) && IsFreePosition(move) && MovesAmount == 0)
                    moveset[move.Row, move.Column] = true;

                move.SetValues(Position.Row + 1, Position.Column + 1);
                if (Board.IsValidPosition(move) && ExistsEnemy(move))
                    moveset[move.Row, move.Column] = true;

                move.SetValues(Position.Row + 1, Position.Column + 1);
                if (Board.IsValidPosition(move) && ExistsEnemy(move))
                    moveset[move.Row, move.Column] = true;

            }



            return moveset;
        }
    }
}
