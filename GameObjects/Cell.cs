using Match3.Core;
using Match3.Core.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Match3.GameObjects
{
    public class Cell : GameObject
    {
        private const string TextureName = "cell";

        private SpriteRenderer _spriteRenderer;
        private Vector2 _position;

        public Gem Gem { get; set; }
        public Point Point { get; private set; }
        public Color GemColor => Gem.Color;

        public event Action PositionChanged;
        
        public Cell(GameController gameController) : base(gameController) { }

        protected override void Awake()
        {
            _spriteRenderer = AddComponent<SpriteRenderer>();

            if(GameController.TryGetContent(TextureName, out Texture2D texture2D))
            {
                _spriteRenderer.Texture2D = texture2D;
                _spriteRenderer.IsRenderTarget = true;
            }

            _position = Transform.Position;

            PositionChanged += OnPositionChanged;
        }

        protected override void OnUpdate()
        {
            if(_position != Transform.Position)
            {
                PositionChanged?.Invoke();

                _position = Transform.Position;
            }
        }

        public void Init(Point point)
        {
            Point = point;
        }

        private void OnPositionChanged()
        {
            if(Gem != null)
            {
                Gem.Transform.Position = Transform.Position;
            }
        }
    }
}
