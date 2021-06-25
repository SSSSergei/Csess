using System.Collections.Generic;

namespace ChessLogic
{
    public struct Square
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public static List<Square> GetSquares()
        {
            var res = new List<Square>();
            for (int y = 0; y < 8; y++)
                for (int x = 0; x < 8; x++)
                    res.Add(new Square(x, y));
            return res;
        }

        public Square(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool CheckOnBoard()
        {
            return X >= 0 && X < 8
                && Y >= 0 && Y < 8;
        }
    }
}
