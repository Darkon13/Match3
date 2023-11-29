using Match3.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3
{
    public class Grid : GameObject
    {
        private Dictionary<Point, Rectangle> _points;
        private int _distanceBetweenCells;
        private Vector2 _pivot = new Vector2(0.5f, 0.5f);
        private bool _inited = false;

        public int LineCount { get; private set; }
        public int ColumnCount { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Vector2 Size => new Vector2(Width, Height);


        public Grid(GameController gameController) : base(gameController) { }

        public void Init(int lines, int columns, int cellWidth = 1, int cellHeight = 1, int distanceBetweenX = 0, int distanceBetweenY = 0)
        {
            if(columns <= 0 || lines <= 0)
            {
                return;
            }

            _points = new Dictionary<Point, Rectangle>(columns * lines);

            Width = lines * cellWidth + (lines - 1) * distanceBetweenX;
            Height = columns * cellHeight + (columns - 1) * distanceBetweenY;

            for (int i = 0; i < columns; i++)
            {
                for(int j = 0; j < lines; j++)
                {
                    int posX = (i * cellWidth) + (i * distanceBetweenX) - (int)(Width * _pivot.X);
                    int posY = (j * cellHeight) + (j * distanceBetweenY) - (int)(Height * _pivot.Y);

                    _points.Add(new Point(i, j), new Rectangle(posX, posY, cellWidth, cellHeight));
                }
            }
        }

        public Rectangle GetRectangleFromLocal(Point point)
        {
            if (_points.ContainsKey(point))
            {
                return _points[point];
            }

            return Rectangle.Empty;
        }

        public Rectangle GetRectangleFromLocal(int x, int y) => GetRectangleFromLocal(new Point(x, y));

        public Point GetPointFromLocal(Point point)
        {
            Rectangle rectangle = GetRectangleFromLocal(point);

            if(rectangle != Rectangle.Empty)
            {
                return rectangle.Location;
            }

            return Point.Zero;
        }

        public Point GetPointFromLocal(int x, int y) => GetPointFromLocal(new Point(x, y));

        public Rectangle GetRectangleFromGlobal(Point point)
        {
            foreach(Rectangle rectangle in _points.Values)
            {
                if (rectangle.Contains(point))
                {
                    return rectangle;
                }
            }

            return Rectangle.Empty;
        }

        public Rectangle GetRectangleFromGlobal(int x, int y) => GetRectangleFromGlobal(new Point(x, y));

        public Point GetPointFromGlobal(Point point)
        {
            Rectangle rectangle = GetRectangleFromGlobal(point);

            if (rectangle != Rectangle.Empty)
            {
                return rectangle.Location;
            }

            return Point.Zero;
        }

        public Point GetPointFromGlobal(int x, int y) => GetPointFromGlobal(new Point(x, y));
    }
}
