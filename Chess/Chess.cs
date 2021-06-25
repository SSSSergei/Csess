using System.Collections.Generic;

namespace ChessLogic
{
    public class Chess
    {
        public string Data { get; private set; }
        public Board Board { get; private set; }
        readonly Moves moves;
        public List<FigureMoving> Moves
        {
            get
            {
                return allMoves;
            }
        }

        private List<FigureMoving> allMoves;

        public Chess(string data = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")
        {
            Data = data;
            Board = new Board(data);
            moves = new Moves(Board);
            FindAllMoves();
        }

        Chess(Board board)
        {
            Board = board;
            Data= board.Data;
            moves = new Moves(board);
        }


        public Chess Move(FigureMoving move)
        {
            if (!moves.CanMove(move))
                return this;
            if (Board.IsCheckAfterMove(move))
                return this;
            Board nextBoard = Board.Move(move);
            Chess nextChess = new Chess(nextBoard);
            return nextChess;
        }

        public char GetFigureAt(int x, int y)
        {
            Square square = new Square(x,y);
            Figure f = Board.GetFigureAt(square);
            return f == Figure.none ? '.' : (char)f;
        }

        void FindAllMoves()
        {
            allMoves = new List<FigureMoving>();
            foreach (var fs in Board.GetAllFigures())
            {
                foreach (var to in Square.GetSquares())
                {
                    var fm = new FigureMoving(fs, to);
                    if (moves.CanMove(fm))
                        if(!Board.IsCheckAfterMove(fm))
                            allMoves.Add(fm);
                }
            }
        }

        public List<string> GetAllMoves()
        {
            FindAllMoves();
            var list = new List<string>();
            foreach (var item in allMoves)
            {
                list.Add(item.ToString());
            }
            return list;
        }

        public bool IsCheck()
        {
            return Board.IsCheck();
        }
    }
}
