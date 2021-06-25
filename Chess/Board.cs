using System.Collections.Generic;
using System.Text;

namespace ChessLogic
{
    public class Board
    {
        public string Data { get; private set; }

        readonly Figure[,] figures;
        public Color MoveColor { get; private set; }
        public int MoveNumber { get; private set; }

        public List<FigureOnSquare> GetAllFigures()
        {
            var res = new List<FigureOnSquare>();
            foreach(Square square in Square.GetSquares())
            {
                if (GetFigureAt(square).GetColor() == MoveColor)
                    res.Add(new FigureOnSquare(GetFigureAt(square), square));
            }
            return res;
        }

        public Board(string data)
        {
            Data = data;
            figures = new Figure[8, 8];
            InitilizationBoard();
        }

        void InitilizationBoard()
        {
            string[] parts = Data.Split();
            if (parts.Length != 6) return;
            InitilizationFigure(parts[0]);
            MoveColor = (parts[1] == "w") ? Color.white : Color.black; ;
            MoveNumber = int.Parse(parts[5]);
        }

        void InitilizationFigure(string data)
        {
            for(int j=8; j>=2; j--)
            {
                data = data.Replace(j.ToString(), (j - 1).ToString() + "1");
            }
            data = data.Replace("1", ".");
            string[] lines = data.Split('/');
            for (int y = 7; y >= 0; y--)
            {
                for (int x= 0;  x < 8; x++)
                {
                    figures[x, y] = lines[7-y][x] == '.' 
                        ? Figure.none
                        : (Figure)lines[7 - y][x];
                }
            }
        }

        public Figure GetFigureAt(Square square)
        {
            if(square.CheckOnBoard())
            {
                return figures[square.X, square.Y];
            }
            return Figure.none;
        }

        void SetFigureAt(Square square, Figure figure)
        {
            if(square.CheckOnBoard())
            {
                figures[square.X, square.Y] = figure;
            }
        }

        public Board Move(FigureMoving fm)
        {
            Board next = new Board(Data);
            next.SetFigureAt(fm.From, Figure.none);
            next.SetFigureAt(fm.To, fm.Promotion==Figure.none ? fm.Figure : fm.Promotion);
            if(MoveColor==Color.black)
            {
                next.MoveNumber++;
            }
            next.MoveColor = MoveColor.FlipColor();
            next.CreateData();
            return next;
        }

        void CreateData()
        {
            Data = DataFigure() + " " +
                   (MoveColor==Color.white ? "w" : "b") + 
                   " - - 0 " + MoveNumber.ToString();
        }

        string DataFigure()
        {
            var sb = new StringBuilder();
            for (int y=7; y>=0; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    sb.Append(figures[x, y]==Figure.none ? '1' : (char)figures[x,y]);          
                }
                if (y > 0)
                    sb.Append('/');
            }
            string eight = "11111111";
            for (int j = 8; j >= 2; j--)
            {
                sb.Replace(eight.Substring(0, j), j.ToString());
            }
            return sb.ToString();
        }

        public bool IsCheck()
        {
            Board after = new Board(Data)
            {
                MoveColor = MoveColor.FlipColor()
            };
            return after.IsEatKing();
        }

        private bool IsEatKing()
        {
            var badKing = IsEnemyKing();
            var moves = new Moves(this);
            foreach (var item in GetAllFigures())
            {
                var fm = new FigureMoving(item, badKing);
                if (moves.CanMove(fm))
                    return true;
            }
            return false;
        }

        private Square IsEnemyKing()
        {
            var badKnig = 
                MoveColor == Color.black
                ? Figure.whiteKing 
                : Figure.blackKing;
            foreach (var item in Square.GetSquares())
                if (GetFigureAt(item) == badKnig)
                    return item;
            return new Square(-1, -1);
        }

        public bool IsCheckAfterMove(FigureMoving fm)
        {
            Board after = Move(fm);
            return after.IsEatKing();
        }
    }
}
