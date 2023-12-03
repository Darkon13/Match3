using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Match3.Core.UIElements
{
    public abstract class RenderElement
    {
        private float _layer = 0;
        private Vector2 _pivot = new Vector2(0.5f, 0.5f);
        private Vector2 _scale = Vector2.One;

        public Vector2 Position { get; set; } = Vector2.Zero;
        public Color Color { get; set; } = Color.White;
        public float Layer
        {
            get { return _layer; }
            set { _layer = Math.Clamp(value, 0, 1f); }
        }
        public Vector2 Pivot => _pivot;
        public Vector2 Scale => _scale;

        public void SetPivot(float x, float y)
        {
            _pivot = new Vector2(Math.Clamp(x, 0, 1f), Math.Clamp(y, 0, 1f));
        }

        public void SetPivot(Vector2 pivot) => SetPivot(pivot.X, pivot.Y);

        public void SetScale(float x, float y)
        {
            if(x >= 0 && y >= 0)
            {
                _scale = new Vector2(x, y);
            }
        }

        public void SetScale(Vector2 scale) => SetScale(scale.X, scale.Y);

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
