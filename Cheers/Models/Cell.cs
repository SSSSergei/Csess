using ChessLogic;
using Prism.Mvvm;
using System;

namespace Chees.Models
{
    public class Cell : BindableBase
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Figure Figure { get; set; }
        public Square Square { get; set; }
        public string PathToFigureImage { get; private set; }
        private static string PathToFolder { get; set; }

        public Cell(Square square, Figure figure)
        {
            Square = square;
            Figure = figure;
            PathToFolder = AppDomain.CurrentDomain.BaseDirectory+ @"..\..\Pictures\";
            PathToFigureImage = GetPathToPicByFigure(this.Figure);
            Y = Square.Y * 100;
            X = Square.X * 100;
        }

        private static string GetPathToPicByFigure(Figure figure)
        {
            switch (figure)
            {
                case Figure.blackBishop:
                    return PathToFolder + "black_bishop.png";
                case Figure.blackKing:
                    return PathToFolder + "black_king.png";
                case Figure.blackKnight:
                    return PathToFolder + "black_knight.png";
                case Figure.blackPawn:
                    return PathToFolder + "black_pawn.png";
                case Figure.blackQueen:
                    return PathToFolder + "black_queen.png";
                case Figure.blackRook:
                    return PathToFolder + "black_rook.png";
                case Figure.whiteBishop:
                    return PathToFolder + "white_bishop.png";
                case Figure.whiteKing:
                    return PathToFolder + "white_king.png";
                case Figure.whiteKnight:
                    return PathToFolder + "white_knight.png";
                case Figure.whitePawn:
                    return PathToFolder + "white_pawn.png";
                case Figure.whiteQueen:
                    return PathToFolder + "white_queen.png";
                case Figure.whiteRook:
                    return PathToFolder + "white_rook.png";
                case Figure.none:
                    return "";
                default:
                    throw new Exception("Not Found!");
            }
        }
    }
}
