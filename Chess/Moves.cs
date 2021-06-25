namespace ChessLogic
{
    class Moves
    {
        FigureMoving FigureMoving;
        readonly Board board;

        public Moves(Board board)
        {
            this.board = board;
        }

        public bool CanMove(FigureMoving figureMoving)
        {
            this.FigureMoving = figureMoving;
            return
                CanMoveFrom() &&
                CanMoveTo() &&
                CanFigureMove();
        }

        bool CanMoveFrom()
        {
            return FigureMoving.From.CheckOnBoard() &&
                FigureMoving.Figure.GetColor() == board.MoveColor;
        }

        bool CanMoveTo()
        {
            return FigureMoving.To.CheckOnBoard() && (FigureMoving.From.X!=FigureMoving.To.X || FigureMoving.From.Y!=FigureMoving.To.Y)
                && board.GetFigureAt(FigureMoving.To).GetColor() != board.MoveColor;
        }

        bool CanFigureMove()
        {
            switch (FigureMoving.Figure)
            {
                case Figure.whiteKing:
                case Figure.blackKing:
                    return CanKingMove();
                case Figure.whiteQueen:
                case Figure.blackQueen:
                    return CanStraightMove();
                case Figure.whiteRook:
                case Figure.blackRook:
                    return (FigureMoving.SignX == 0 || FigureMoving.SignY == 0) &&
                        CanStraightMove();
                case Figure.whiteBishop:
                case Figure.blackBishop:
                    return (FigureMoving.SignX != 0 && FigureMoving.SignY != 0) &&
                        CanStraightMove();
                case Figure.whiteKnight:
                case Figure.blackKnight:
                    return CanKnightMove();
                case Figure.whitePawn:
                case Figure.blackPawn:
                    return CanPawnMove();
                default: return false;
            }
        }

        private bool CanPawnMove()
        {
            if (FigureMoving.From.Y < 1 || FigureMoving.From.Y > 6)
                return false;
            int stepY = FigureMoving.Figure.GetColor() == Color.white ? 1 : -1;
            return 
                CanPawnGo(stepY) ||
                CanPawnJump(stepY) ||
                CanPawnEat(stepY);
        }

        private bool CanPawnEat(int stepY)
        {
            if (board.GetFigureAt(FigureMoving.To) != Figure.none)
                if (FigureMoving.AbsDeltaX == 1)
                    if (FigureMoving.DeltaY == stepY)
                        return true;
            return false;
        }

        private bool CanPawnJump(int stepY)
        {
            if (board.GetFigureAt(FigureMoving.To) == Figure.none)
                if (FigureMoving.DeltaX == 0)
                    if (FigureMoving.DeltaY == 2 * stepY)
                        if (FigureMoving.From.Y == 1 || FigureMoving.From.Y == 6)
                            if (board.GetFigureAt(new Square(FigureMoving.From.X, FigureMoving.From.Y + stepY)) == Figure.none)
                                return true;
            return false;
        }

        private bool CanPawnGo(int stepY)
        {
            if (board.GetFigureAt(FigureMoving.To) == Figure.none)
                if (FigureMoving.DeltaX == 0)
                    if (FigureMoving.DeltaY == stepY)
                        return true;
            return false;
        }

        private bool CanKingMove()
        {
            if (FigureMoving.AbsDeltaX <= 1 && FigureMoving.AbsDeltaY <= 1)
                return true;
            return false;
        }

        private bool CanKnightMove()
        {
            if (FigureMoving.AbsDeltaX == 1 && FigureMoving.AbsDeltaY == 2) return true;
            if (FigureMoving.AbsDeltaX == 2 && FigureMoving.AbsDeltaY == 1) return true;
            return false;
        }

        private bool CanStraightMove()
        {
            Square at = FigureMoving.From;
            do
            {
                at = new Square(at.X + FigureMoving.SignX, at.Y + FigureMoving.SignY);
                if (at.X == FigureMoving.To.X && at.Y == FigureMoving.To.Y)
                    return true;
            } while (at.CheckOnBoard() &&
                    board.GetFigureAt(at) == Figure.none);
            return false;
        }
    }
}
