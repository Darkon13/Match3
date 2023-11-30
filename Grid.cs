using Match3.Core;
using Match3.Core.Components;
using Match3.Core.Utils;
using Match3.GameObjects;
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
        private GemFactory _gemFactory;
        private ObjectPool<Gem> _objectPool;
        private Vector2 _pivot;
        private Dictionary<Point, Cell> _cells;
        private int _activeEventsCount;

        private int _tickPerSwap = 10;
        private float _swapDuration = 0.1f;
        private float _swapT;
        private Timer _swapTimer;
        private Cell _cellFrom;
        private Cell _cellTo;

        public int LineCount { get; private set; }
        public int ColumnCount { get; private set; }
        public float Width { get; private set; }
        public float Height { get; private set; }
        public bool Locked => _activeEventsCount > 0;
        public Vector2 Size => new Vector2(Width, Height);

        public Grid(GameController gameController) : base(gameController) { }

        public void Init(int lines, int columns, GemFactory gemFactory, ObjectPool<Gem> objectPool, int cellSize = 64, int distanceBetweenX = 0, int distanceBetweenY = 0)
        {
            if (columns <= 0 || lines <= 0)
            {
                return;
            }

            LineCount = lines;
            ColumnCount = columns;

            Transform.Position = new Vector2(Window.WindowBound.Width / 2, Window.WindowBound.Height / 2);
            _pivot = new Vector2(0.5f, 0.5f);
            _activeEventsCount = 0;

            _gemFactory = gemFactory;
            _objectPool = objectPool;

            _swapTimer = CreateTimer(_swapDuration/_tickPerSwap);
            _swapTimer.Ended += OnSwap;

            GenerateField(lines, columns, cellSize, distanceBetweenX, distanceBetweenY);
        }

        public void Swap(Point from, Point to)
        {
            if(Locked == false)
            {
                _cellFrom = _cells[from];
                _cellTo = _cells[to];

                _swapT = 0;

                _activeEventsCount++;

                _swapTimer.Start();
            }
        }

        private void OnSwap()
        {
            if(_swapT < 1f)
            {
                _swapT += 1f / _tickPerSwap;

                _cellFrom.Gem.Transform.Position = Vector2.Lerp(_cellFrom.Transform.Position, _cellTo.Transform.Position, _swapT);
                _cellTo.Gem.Transform.Position = Vector2.Lerp(_cellTo.Transform.Position, _cellFrom.Transform.Position, _swapT);

                _swapTimer.Start();
            }
            else
            {
                Gem gem = _cellFrom.Gem;

                _cellFrom.Gem = _cellTo.Gem;
                _cellTo.Gem = gem;

                DestroyGem(_cellTo.Point);
                _cellTo.Gem = CreateBonus<BombGem>(_cellTo.Transform.Position, _cellTo.Transform.Scale, Color.Red);

                _activeEventsCount--;
            }
        }

        public void DestroyGem(Point point)
        {
            _cells[point].Gem.Destroy(this, point);
        }

        private Gem CreateGem(Vector2 position, Vector2 scale)
        {
            Color color = Color.White;

            if(_objectPool.TryGetObject(out Gem gem))
            {
                gem.Init(_gemFactory.Create<BaseGem>(color));
                gem.Transform.Position = position;
                gem.Transform.SetScale(scale);
                gem.Enable();

                return gem;
            }

            return null;
        }

        private Gem CreateBonus<T>(Vector2 position, Vector2 scale, Color color) where T : GemType
        {
            if (_objectPool.TryGetObject(out Gem gem))
            {
                gem.Init(_gemFactory.Create<T>(color));
                gem.Transform.Position = position;
                gem.Transform.SetScale(scale);
                gem.Enable();

                return gem;
            }

            return null;
        }

        private void GenerateField(int lines, int columns, int cellSize = 64, int distanceBetweenX = 0, int distanceBetweenY = 0)
        {
            _cells = new Dictionary<Point, Cell>(lines * columns);

            Width = lines * cellSize + (lines - 1) * distanceBetweenX;
            Height = columns * cellSize + (columns - 1) * distanceBetweenY;

            for (int i = 0; i < lines; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Cell cell = GameController.CreateObject<Cell>();
                    cell.Init(new Point(i, j));

                    if (cell.TryGetComponent(out SpriteRenderer spriteRenderer))
                    {
                        float posX = Transform.Position.X + (i * cellSize) + (i * distanceBetweenX) - (Width * _pivot.X) + (spriteRenderer.Pivot.X * cellSize);
                        float posY = Transform.Position.Y + (j * cellSize) + (j * distanceBetweenY) - (Height * _pivot.Y) + (spriteRenderer.Pivot.Y * cellSize);

                        cell.Transform.Position = new Vector2(posX, posY);
                        cell.Transform.SetScale((float)cellSize / spriteRenderer.PixelPerUnit, (float)cellSize / spriteRenderer.PixelPerUnit);
                    }

                    cell.Gem = CreateGem(cell.Transform.Position, cell.Transform.Scale);
                    _cells.Add(new Point(i, j), cell);
                }
            }
        }
    }
}
