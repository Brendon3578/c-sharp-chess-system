using ChessConsoleSystem.GameBoard;

namespace ChessConsoleSystem.Chess
{
    internal class King : Piece
    {

        private ChessMatch _match;

        public King(ChessBoard board, Color color, ChessMatch match) : base(board, color)
        {
            _match = match;
        }

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

            // #special move - short castling or kingside (roque pequeno)
            if (MovesAmount == 0 && !_match.IsCheckmate)
            {
                Position RookPosition = new(Position.Row, Position.Column + 3);

                Position[] nearPositions = [new(Position.Row, Position.Column + 1), new(Position.Row, Position.Column + 2)];

                if (CanDoCastling(RookPosition, nearPositions))
                {
                    moveset[Position.Row, Position.Column + 2] = true;
                }
            }

            // #special move - long castling or queenside (roque grande)
            if (MovesAmount == 0 && !_match.IsCheckmate)
            {
                Position RookPosition = new(Position.Row, Position.Column - 4);

                Position[] nearPositions = [new(Position.Row, Position.Column - 1), new(Position.Row, Position.Column - 2), new(Position.Row, Position.Column - 3)];

                if (CanDoCastling(RookPosition, nearPositions))
                {
                    moveset[Position.Row, Position.Column - 2] = true;
                }
            }

            return moveset;
        }

        private bool CanDoCastling(Position RookPosition, Position[] nearPositions)
        {
            Piece? p = Board.GetPiece(RookPosition);
            bool isRookValidToCastling = p != null && p is Rook && p.Color == Color && p.MovesAmount == 0;
            bool isNearPositionsEmpties = nearPositions.All(pos => Board.GetPiece(pos) == null);
            return isRookValidToCastling && isNearPositionsEmpties;
        }
    }
}
