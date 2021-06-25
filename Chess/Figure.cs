namespace ChessLogic
{
    public enum Figure
    {
        none,

        whiteKing='K',
        whiteQueen = 'Q',
        whiteRook = 'R',
        whiteBishop = 'B',
        whiteKnight = 'N',
        whitePawn = 'P',

        blackKing = 'k',
        blackQueen = 'q',
        blackRook = 'r',
        blackBishop = 'b',
        blackKnight = 'n',
        blackPawn = 'p',
    }

    public static class FigureMethods
    {
        public static Color GetColor(this Figure figure)
        {
            if (figure == Figure.none)
                return Color.none;
            else if (figure == Figure.whiteBishop ||
                    figure == Figure.whiteKnight ||
                    figure == Figure.whiteKing ||
                    figure == Figure.whitePawn ||
                    figure == Figure.whiteQueen ||
                    figure == Figure.whiteRook)
                return Color.white;
            else return Color.black;
        }
    }
}
