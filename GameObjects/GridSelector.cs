using Match3.Core;
using Match3.Core.Components;
using Match3.Core.Utils.Input;
using Microsoft.Xna.Framework;

namespace Match3.GameObjects
{
    public class GridSelector : GameObject
    {
        private Grid _grid;
        private Cell _selectedCell;
        private SpriteRenderer _spriteRenderer;
        private Color _selectionColor;
        private KeyBinder _keyBinder;
        private bool _inited;

        public GridSelector(GameController gameController) : base(gameController) { }

        protected override void OnEnable()
        {
            if(_inited == true)
            {
                _keyBinder.Bind(InputState.KeyDownOnce, MouseKeys.LeftKey, OnLeftClick);
            }
        }

        protected override void OnDisable()
        {
            if(_inited == true)
            {
                _keyBinder.UnbindAllKeys();
            }
        }

        public void Init(Grid grid)
        {
            if(_inited == false)
            {
                _selectionColor = Color.Blue;
                _grid = grid;

                _keyBinder = new KeyBinder();
                _keyBinder.Bind(InputState.KeyDownOnce, MouseKeys.LeftKey, OnLeftClick);

                _inited = true;
            }
        }

        private void OnLeftClick()
        {
            if(_grid.Locked == true)
                return;

            Point point = MouseListener.MousePosition;

            if(GameController.Raycast(point, out GameObject gameObject))
            {
                if(gameObject is Cell)
                {
                    Cell cell = gameObject as Cell;

                    if(_selectedCell == null)
                    {
                        _selectedCell = cell;

                        if(_selectedCell.TryGetComponent(out SpriteRenderer renderer))
                        {
                            _spriteRenderer = renderer;
                        }

                        _spriteRenderer.Color = _selectionColor;
                    }
                    else
                    {
                        _grid.Swap(_selectedCell.Point, cell.Point);

                        _spriteRenderer.Color = Color.White;
                        _selectedCell = null;
                    }
                }
            }
            else
            {
                if(_spriteRenderer != null)
                {
                    _spriteRenderer.Color = Color.White;
                }

                _spriteRenderer = null;
                _selectedCell = null;
            }
        }
    }
}
