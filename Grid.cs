using Match3.Core;
using Match3.Core.Components;
using Match3.Core.Utils;
using Match3.GameObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Match3
{
    public class Grid : GameObject
    {
        private GemFactory _gemFactory;
        private ObjectPool<Gem> _objectPool;
        private Vector2 _pivot;
        private List<Point> _destroyedGems;
        private List<Point> _pointsToCheck;
        private Dictionary<Gem, List<Point>> _moveList;
        private Dictionary<Point, Cell> _cells;

        private Point _pointFrom;
        private Point _pointTo;

        private int _tickPerMove = 10;
        private float _moveDuration = 0.1f;
        private float _moveT = 0;
        private Timer _moveTimer;

        public int LineCount { get; private set; }
        public int ColumnCount { get; private set; }
        public float Width { get; private set; }
        public float Height { get; private set; }
        public bool Locked { get; private set; }
        public Vector2 Size => new Vector2(Width, Height);

        public event Action Moved;

        public Grid(GameController gameController) : base(gameController) { }

        public void Init(int lines, int columns, GemFactory gemFactory, ObjectPool<Gem> objectPool, int cellSize = 64, int distanceBetweenX = 0, int distanceBetweenY = 0)
        {
            if (columns <= 0 || lines <= 0)
            {
                return;
            }

            _moveList = new Dictionary<Gem, List<Point>>(lines * columns);
            _destroyedGems = new List<Point>(lines * columns);
            _pointsToCheck = new List<Point>(lines * columns);
            _cells = new Dictionary<Point, Cell>(lines * columns);

            Width = lines * cellSize + (lines - 1) * distanceBetweenX;
            Height = columns * cellSize + (columns - 1) * distanceBetweenY;

            LineCount = lines;
            ColumnCount = columns;

            Transform.Position = new Vector2(Window.WindowBound.Width / 2, Window.WindowBound.Height / 2);

            _pivot = new Vector2(0.5f, 0.5f);

            _gemFactory = gemFactory;
            _objectPool = objectPool;

            _moveTimer = CreateTimer(_moveDuration/_tickPerMove);
            _moveTimer.Ended += MoveGems;
            Moved += SearchCombination;

            GenerateField(lines, columns, cellSize, distanceBetweenX, distanceBetweenY);
        }

        public void Swap(Point from, Point to)
        {
            if(Locked == false)
            {
                if(_cells[from].Gem != null && _cells[to].Gem != null)
                {
                    if(_cells[from].Gem.IsActive && _cells[to].Gem.IsActive)
                    {
                        int lenghtX = Math.Abs(from.X - to.X);
                        int lenghtY = Math.Abs(from.Y - to.Y);

                        if ((lenghtX <= 1 && lenghtY <= 1) && lenghtX != lenghtY)
                        {
                            _pointFrom = from;
                            _pointTo = to;

                            RegisterToMove(_pointFrom, _pointTo);
                            RegisterToMove(_pointTo, _pointFrom);

                            StartMove();

                            Moved += OnSwap;

                            Locked = true;
                        }
                    }
                }
            }
        }

        private void OnSwap()
        {
            bool resultFrom = DestroyCombination(_pointFrom);
            bool resultTo = DestroyCombination(_pointTo);

            if (resultFrom == false && resultTo == false)
            {
                RegisterToMove(_pointFrom, _pointTo);
                RegisterToMove(_pointTo, _pointFrom);

                StartMove();
            }
            else
            {
                RegisterToMoveDown();

                StartMove();
            }

            Moved -= OnSwap;
        }

        public void DestroyGem(Point point)
        {
            if(_cells[point].Gem != null && _cells[point].Gem.IsActive == true)
            {
                _cells[point].Gem.Destroy(this, point);

                _destroyedGems.Add(point);

                //_cells[point].Gem.AnimationEnded += OnDestroy;

                //_cells[point].Gem.StartAnimation("Destroy", point);
            }
        }

        //private void OnDestroy(Point point)
        //{
        //    _cells[point].Gem.Destroy(this, point);

        //    _destroyedGems.Add(point);

        //    _cells[point].Gem.AnimationEnded -= OnDestroy;
        //}

        private void SearchCombination()
        {
            int count = _destroyedGems.Count;

            foreach(Point point in _pointsToCheck)
            {
                DestroyCombination(point);
            }
            
            _pointsToCheck.Clear();

            if (count != _destroyedGems.Count)
            {
                RegisterToMoveDown();

                StartMove();
            }
            else
            {
                if(_destroyedGems.Count != 0)
                {
                    RestoreGems();
                }
            }
        }

        private void RestoreGems()
        {
            foreach(Point point in _destroyedGems)
            {
                _cells[point].Gem = CreateGem<BaseGem>(_cells[point].Transform.Position, _cells[point].Transform.Scale, Color.White);
            }

            _pointsToCheck.AddRange(_destroyedGems);
            _destroyedGems.Clear();

            SearchCombination();
        }

        private void RegisterToMove(Point from, Point to)
        {
            Gem gem = _cells[from].Gem;

            if(gem != null)
            {
                if (_moveList.ContainsKey(gem))
                {
                    _moveList[gem] = new List<Point>{ from, to };
                }
                else
                {
                    _moveList.Add(gem, new List<Point> { from, to });
                }
            }
        }

        private void RegisterToMoveDown()
        {
            Dictionary<int, List<int>> pointsToMove = new Dictionary<int, List<int>>();

            foreach (Point point in _destroyedGems)
            {
                if (pointsToMove.ContainsKey(point.X))
                {
                    if (pointsToMove[point.X][0] < point.Y)
                    {
                        pointsToMove[point.X][0] = point.Y;
                    }

                    pointsToMove[point.X][1]++;
                }
                else
                {
                    pointsToMove.Add(point.X, new List<int> { point.Y, 1 });
                }
            }

            _destroyedGems.Clear();

            foreach (int column in pointsToMove.Keys)
            {
                int y = pointsToMove[column][0];

                Point point = new Point(column, y);

                for (int j = y - 1; j >= 0; j--)
                {
                    Point swapPoint = new Point(point.X, j);

                    if (_cells[swapPoint].Gem != null && _cells[swapPoint].Gem.IsActive == true && _moveList.ContainsKey(_cells[swapPoint].Gem) == false)
                    {
                        RegisterToMove(swapPoint, point);
                        _cells[swapPoint].Gem = null;
                        
                        _pointsToCheck.Add(point);

                        point.Y = point.Y - 1;
                    }
                }

                _cells[point].Gem = null;

                _pointsToCheck.Add(point);

                for (int j = 0; j < pointsToMove[column][1]; j++)
                {
                    _destroyedGems.Add(new Point(point.X, j));
                }
            }
        }

        private void StartMove()
        {
            _moveTimer.Start();
            _moveT = 0;

            Locked = true;
        }

        private void MoveGems()
        {
            if(_moveT < 1f && _moveList.Count > 0)
            {
                _moveT = MathF.Min(1f, _moveT + (1f / _tickPerMove));

                foreach(Gem gem in _moveList.Keys)
                {
                    Vector2 from = _cells[_moveList[gem][0]].Transform.Position;
                    Vector2 to = _cells[_moveList[gem][1]].Transform.Position;

                    gem.Transform.Position = Vector2.Lerp(from, to, _moveT);
                }

                _moveTimer.Start();
            }
            else
            {
                foreach (Gem gem in _moveList.Keys)
                {
                    _cells[_moveList[gem][1]].Gem = gem;
                }

                _moveList.Clear();

                Locked = false;

                Moved?.Invoke();
            }
        }

        private void ChangeBaseGem<T>(Point point) where T : GemType
        {
            Color color = _cells[point].GemColor;

            _cells[point].Gem.Destroy(this, point);
            _cells[point].Gem = CreateGem<T>(_cells[point].Transform.Position, _cells[point].Transform.Scale, color);
        }

        private Gem CreateGem<T>(Vector2 position, Vector2 scale, Color color) where T : GemType
        {
            if(_objectPool.TryGetObject(out Gem gem))
            {
                gem.Init(_gemFactory.Create<T>(color));
                gem.Transform.Position = position;
                gem.Transform.SetScale(scale);
                gem.Enable();

                return gem;
            }

            return null;
        }

        private Gem CreateDifferentGem(Vector2 position, Vector2 scale, Point point)
        {
            Gem gem = null;

            bool isDifferentGem = false;
            bool isColorInColumnDifferent = true;
            bool isColorInLineDifferent = true;

            Color colorInColumn = Color.White;
            Color colorInLine = Color.White;

            if (Math.Max(0, point.X - 1) != 0)
            {
                Point point1 = new Point(point.X - 2, point.Y);
                Point point2 = new Point(point.X - 1, point.Y);

                if (_cells[point1].Gem != null && _cells[point2].Gem != null)
                {
                    if (_cells[point1].GemColor == _cells[point2].GemColor)
                    {
                        colorInColumn = _cells[point1].GemColor;
                        isColorInColumnDifferent = false;
                    }
                    else
                    {
                        isColorInColumnDifferent = true;
                    }
                }
                else
                {
                    isColorInColumnDifferent = true;
                }
            }

            if (Math.Max(0, point.Y - 1) != 0)
            {
                Point point1 = new Point(point.X, point.Y - 2);
                Point point2 = new Point(point.X, point.Y - 1);

                if (_cells[point1].Gem != null && _cells[point2].Gem != null)
                {
                    if (_cells[point1].GemColor == _cells[point2].GemColor)
                    {
                        colorInLine = _cells[point1].GemColor;
                        isColorInLineDifferent = false;
                    }
                    else
                    {
                        isColorInLineDifferent = true;
                    }
                }
                else
                {
                    isColorInLineDifferent = true;
                }
            }

            while (isDifferentGem != true)
            {
                gem = CreateGem<BaseGem>(position, scale, Color.White);

                isDifferentGem = true;

                if (isColorInLineDifferent == true && isColorInColumnDifferent == true)
                {
                    break;
                }
                else
                {
                    if (gem.Color == colorInLine)
                    {
                        isDifferentGem = false;

                        gem.Disable();
                    }

                    if (isDifferentGem == true)
                    {
                        if (gem.Color == colorInColumn)
                        {
                            isDifferentGem = false;

                            gem.Disable();
                        }
                    }
                }
            }

            return gem;
        }

        private bool DestroyCombination(Point searchPoint)
        {
            if (FindCombinationInPoint(searchPoint, out List<Point> line, out List<Point> column))
            {
                List<Point> pointsToDesctruct = new List<Point>();

                if (line.Count >= 2)
                {
                    pointsToDesctruct.AddRange(line);
                }

                if (column.Count >= 2)
                {
                    pointsToDesctruct.AddRange(column);
                }

                if (pointsToDesctruct.Count > 0)
                {
                    if (line.Count >= 2 && column.Count >= 2)
                    {
                        ChangeBaseGem<BombGem>(searchPoint);
                    }
                    else if (line.Count >= 3)
                    {
                        ChangeBaseGem<HorizontalBonusGem>(searchPoint);
                    }
                    else if (column.Count >= 3)
                    {
                        ChangeBaseGem<VerticalBonusGem>(searchPoint);
                    }
                    else
                    {
                        pointsToDesctruct.Add(searchPoint);
                    }
                }

                foreach (Point point in pointsToDesctruct)
                {
                    DestroyGem(point);
                }

                return pointsToDesctruct.Count > 0;
            }

            return false;
        }

        private bool FindCombinationInPoint(Point point, out List<Point> lines, out List<Point> column) 
        {
            lines = new List<Point>();
            column = new List<Point>();

            lines.AddRange(FindCombinationInDirection(point, true, false, false));
            lines.AddRange(FindCombinationInDirection(point, true, false, true));
            column.AddRange(FindCombinationInDirection(point, false, true, false));
            column.AddRange(FindCombinationInDirection(point, false, true, true));

            return lines.Count > 0 || column.Count > 0;
        }

        private List<Point> FindCombinationInDirection(Point point, bool isX, bool isY, bool isNegative)
        {
            List<Point> result = new List<Point>();

            if (isX == true && isY == true)
            {
                return result;
            }

            if(_cells[point].Gem != null && _cells.ContainsKey(point) && _cells[point].Gem.IsActive == true)
            {
                Color color = _cells[point].GemColor;
                Point currentPoint = point;
                bool isFinish = false;

                while(isFinish == false)
                {
                    if(isX == true)
                    {
                        if(isNegative == true)
                        {
                            currentPoint.X = currentPoint.X - 1;

                            if(currentPoint.X < 0)
                            {
                                isFinish = true;

                                break;
                            }
                        }
                        else
                        {
                            currentPoint.X = currentPoint.X + 1;

                            if (currentPoint.X >= LineCount)
                            {
                                isFinish = true;

                                break;
                            }
                        }
                    }
                    else
                    {
                        if (isNegative == true)
                        {
                            currentPoint.Y = currentPoint.Y - 1;

                            if (currentPoint.Y < 0)
                            {
                                isFinish = true;

                                break;
                            }
                        }
                        else
                        {
                            currentPoint.Y = currentPoint.Y + 1;

                            if (currentPoint.Y >= ColumnCount)
                            {
                                isFinish = true;

                                break;
                            }
                        }
                    }

                    if(_cells[currentPoint].Gem != null && _cells[currentPoint].Gem.IsActive == true && _cells[currentPoint].GemColor == color)
                    {
                        result.Add(currentPoint);
                    }
                    else
                    {
                        isFinish = true;
                    }
                }
            }

            return result;
        }

        private void GenerateField(int lines, int columns, int cellSize = 64, int distanceBetweenX = 0, int distanceBetweenY = 0)
        {
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < lines; j++)
                {
                    Cell cell = GameController.CreateObject<Cell>();
                    cell.Init(new Point(i, j));

                    if (cell.TryGetComponent(out SpriteRenderer spriteRenderer))
                    {
                        float posX = Transform.Position.X + (i * cellSize) + (i * distanceBetweenX) - (Width * _pivot.X) + (spriteRenderer.Pivot.X * spriteRenderer.Image.Width);
                        float posY = Transform.Position.Y + (j * cellSize) + (j * distanceBetweenY) - (Height * _pivot.Y) + (spriteRenderer.Pivot.Y * spriteRenderer.Image.Height);

                        cell.Transform.Position = new Vector2(posX, posY);
                        cell.Transform.SetScale((float)cellSize / spriteRenderer.PixelPerUnit, (float)cellSize / spriteRenderer.PixelPerUnit);
                    }

                    cell.Gem = CreateDifferentGem(cell.Transform.Position, cell.Transform.Scale, new Point(i, j));
                    _cells.Add(new Point(i, j), cell);
                }
            }
        }
    }
}
