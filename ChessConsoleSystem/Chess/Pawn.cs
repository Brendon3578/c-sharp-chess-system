using ChessConsoleSystem.GameBoard;

namespace ChessConsoleSystem.Chess
{
    internal class Pawn : Piece
    {

        public Pawn(ChessBoard board, Color color, ChessMatch match) : base(board, color)
        {
            _match = match;
        }
        private readonly ChessMatch _match;

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

                // #special move for white pieces - En Passsant
                if (Position.Row == 3)
                {
                    Position left = new Position(Position.Row, Position.Column - 1);

                    if (CanMoveEnPassant(left))
                        moveset[left.Row - 1, left.Column] = true;

                    Position right = new Position(Position.Row, Position.Column + 1);

                    if (CanMoveEnPassant(right))
                        moveset[right.Row - 1, right.Column] = true;
                }

            }
            else if (Color == Board.SecondPlayerColor)
            {
                move.SetValues(Position.Row + 1, Position.Column);
                if (Board.IsValidPosition(move) && IsFreePosition(move))
                    moveset[move.Row, move.Column] = true;

                move.SetValues(Position.Row + 2, Position.Column);
                if (Board.IsValidPosition(move) && IsFreePosition(move) && MovesAmount == 0)
                    moveset[move.Row, move.Column] = true;

                move.SetValues(Position.Row + 1, Position.Column - 1);
                if (Board.IsValidPosition(move) && ExistsEnemy(move))
                    moveset[move.Row, move.Column] = true;

                move.SetValues(Position.Row + 1, Position.Column + 1);
                if (Board.IsValidPosition(move) && ExistsEnemy(move))
                    moveset[move.Row, move.Column] = true;

                // #special move for black pieces - En Passsant
                if (Position.Row == 4)
                {
                    Position left = new Position(Position.Row, Position.Column - 1);

                    if (CanMoveEnPassant(left))
                        moveset[left.Row + 1, left.Column] = true;

                    Position right = new Position(Position.Row, Position.Column + 1);

                    if (CanMoveEnPassant(right))
                        moveset[right.Row + 1, right.Column] = true;
                }
            }



            return moveset;
        }
        private bool CanMoveEnPassant(Position enemy)
        {
            bool existsPieceInPlaceBoard = Board.IsValidPosition(enemy) && ExistsEnemy(enemy);
            bool isPieceVulnerable = Board.GetPiece(enemy) == _match._vulnerablePieceForEnPassantMove;
            return existsPieceInPlaceBoard && isPieceVulnerable;
        }
    }


}
