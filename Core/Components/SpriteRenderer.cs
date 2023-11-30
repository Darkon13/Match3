using Match3.Core.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Match3.Core.Components
{
    public class SpriteRenderer : Component
    {
        private Vector2 _pivot = new Vector2(0.5f, 0.5f);

        public int PixelPerUnit => 100;
        public Texture2D Texture2D { get; set; }
        public float Layer { get; set; } = 0;
        public bool IsRenderTarget { get; set; } = false;

        public Color Color { get; set; } = Color.White;

        public Vector2 Pivot => _pivot;

        public SpriteRenderer(GameObject gameObject, Texture2D texture2D = null) : base(gameObject)
        {
            Texture2D = texture2D;
        }

        public void SetPivot(float x, float y) => _pivot = new Vector2(Math.Clamp(x, 0, 1f), Math.Clamp(y, 0, 1f));

        public void SetPivot(Vector2 pivot) => SetPivot(pivot.X, pivot.Y);

        public override void Draw(SpriteBatch spriteBatch, RenderBuffer<GameObject> renderBuffer)
        {
            float widthPerUnit = 1f / Texture2D.Width * PixelPerUnit;
            float heightPerUnit = 1f / Texture2D.Height * PixelPerUnit;
            Vector2 scale = GameObject.Transform.Scale * new Vector2(widthPerUnit, heightPerUnit);

            float posX = GameObject.Transform.Position.X - (Texture2D.Width * scale.X) * _pivot.X;
            float posY = GameObject.Transform.Position.Y - (Texture2D.Height * scale.Y) * _pivot.Y;
            Vector2 pos = new Vector2(posX, posY);

            if (Texture2D != null && spriteBatch != null)
            {
                if(IsRenderTarget == true)
                {
                    Rectangle rectangle = new Rectangle();
                    rectangle = rectangle.RectangleFromFloat(posX, posY, scale.X * Texture2D.Width, scale.Y * Texture2D.Height);

                    renderBuffer.Register(rectangle, GameObject);
                }

                spriteBatch.Draw(Texture2D, pos, null, Color, 0f, new Vector2(), scale, SpriteEffects.None, Layer);
            }
        }
    }
}
