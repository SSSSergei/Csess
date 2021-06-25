using Chees.Models;
using ChessLogic;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace Chees.ViewModels
{
    public class MainVM : BindableBase
    {
        private Chess _chess;

        public Cell ChangedCell { get; private set; }

        private readonly Cell _nullCell;

        public string Info { get; private set; }

        public DelegateCommand<Cell> SelectCell { get; private set; }

        public DelegateCommand StartNewGame { get; private set; }

        public DelegateCommand LoadGame { get; private set; }

        public DelegateCommand Options { get; private set; }

        public DelegateCommand Refresh { get; private set; }

        public CancelEventHandler WriteChess { get; private set; }

        public ObservableCollection<Cell> Board { get; private set; }

        private readonly string _path;

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public MainVM()
        {
            _path = System.AppDomain.CurrentDomain.BaseDirectory
                + @"..\..\SavedChess\chess.dat";
            _chess = new Chess();
            _nullCell = new Cell(new Square(-100, -100), Figure.none);
            ChangedCell = _nullCell;
            Board = new ObservableCollection<Cell>();
            SelectCell = new DelegateCommand<Cell>(ChangeCell);
            StartNewGame = new DelegateCommand(Restart);
            LoadGame = new DelegateCommand(ReadFile);
            Refresh = new DelegateCommand(SaveChessToMenu);
            WriteChess = SaveChess;
            UpdateBoard();
            UpdateInfo();
            Application.Current.MainWindow.Closing += WriteChess;
        }

        private void ReadFile()
        {
            _chess = ChessFileReaderWriter.ReadChessFromFile(_path);
            UpdateBoard();
        }

        private void SaveChessToMenu()
        {
            ChessFileReaderWriter.WriteChessToFile(_path, _chess);
        }

        private void SaveChess(object o1, object o2)
        {
            ChessFileReaderWriter.WriteChessToFile(_path, _chess);
        }

        private void Restart()
        {
            _chess = new Chess();
            UpdateBoard();
            UpdateInfo();
        }

        private void ChangeCell(Cell cell)
        {
            if (ChangedCell == _nullCell)
            {
                if (cell.Figure == Figure.none)
                {
                    return;
                }
                ChangedCell = cell;
            }
            else
            {
                if (ChangedCell == cell)
                {
                    ChangedCell = _nullCell;
                }
                else
                {
                    if (FigureMethods.GetColor(ChangedCell.Figure) == FigureMethods.GetColor(cell.Figure))
                    {
                        ChangedCell = _nullCell;
                        return;
                    }
                    List<FigureMoving> moves = _chess.Moves;
                    foreach (FigureMoving move in moves)
                    {
                        if (move.From.X == ChangedCell.Square.X
                            && move.From.Y == ChangedCell.Square.Y
                            && move.To.X == cell.Square.X
                            && move.To.Y == cell.Square.Y)
                        {
                            _chess = _chess.Move(move);
                            _chess.GetAllMoves();
                            UpdateBoard();
                            if (_chess.Moves.Count != 0)
                            {
                                UpdateInfo();
                            }
                            break;
                        }
                    }
                    ChangedCell = _nullCell;
                }
            }
            RaisePropertyChanged("ChangedCell");
        }

        private void UpdateBoard()
        {
            Board board = _chess.Board;
            Board.Clear();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Square square = new Square(i, j);
                    Figure figure = board.GetFigureAt(square);

                    Board.Add(new Cell(square, figure));
                }
            }
            UpdateInfo();
        }

        private void UpdateInfo()
        {
            Info = _chess.Board.MoveColor == Color.white ?
                "White" : "Black";
            RaisePropertyChanged("Info");
        }
    }
}
