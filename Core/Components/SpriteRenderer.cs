using Match3.Core.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3.Core.Components
{
    public class SpriteRenderer : Component
    {
        private Texture2D _texture2D;
        private int _pixelPerUnit = 100;

        public Color Color { get; set; } = Color.White;

        public Vector2 Pivot { get; private set; } = new Vector2(0.5f, 0.5f);

        public int PixelPerUnit
        {
            get => _pixelPerUnit;
            set => Math.Max(0, value);
        }

        public SpriteRenderer(GameObject gameObject, Texture2D texture2D = null) : base(gameObject)
        {
            _texture2D = texture2D;
        }

        public void SetPivot(float x, float y) => Pivot = new Vector2(Math.Clamp(x, 0, 1f), Math.Clamp(y, 0, 1f));

        public void SetPivot(Vector2 pivot) => SetPivot(pivot.X, pivot.Y);

        public override void Draw(SpriteBatch spriteBatch, RenderBuffer<GameObject> renderBuffer)
        {
            float widthPerUnit = 1f / _texture2D.Width * PixelPerUnit;
            float heightPerUnit = 1f / _texture2D.Height * PixelPerUnit;
            Vector2 scale = GameObject.Transform.Scale * new Vector2(widthPerUnit, heightPerUnit);

            float posX = GameObject.Transform.Position.X - (_texture2D.Width * scale.X) * Pivot.X;
            float posY = GameObject.Transform.Position.Y - (_texture2D.Height * scale.Y) * Pivot.Y;
            Vector2 pos = new Vector2(posX, posY);

            if (_texture2D != null && spriteBatch != null)
            {
                Rectangle rectangle = new Rectangle();
                rectangle = rectangle.RectangleFromFloat(posX, posY, scale.X * _texture2D.Width, scale.Y * _texture2D.Height);

                renderBuffer.Register(rectangle, GameObject);
                spriteBatch.Begin();
                spriteBatch.Draw(_texture2D, pos, null, Color, 0f, new Vector2(), scale, SpriteEffects.None, 1f);
                spriteBatch.End();
            }
        }
    }
}
