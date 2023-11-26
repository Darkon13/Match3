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
        private GameObject _gameObject;

        public Vector2 Pivot { get; set; } = new Vector2(0.5f, 0.5f);
        public Color Color { get; set; } = Color.White;
        public int PixelPerUnit { get; set; } = 100;

        public SpriteRenderer(GameObject gameObject, Texture2D texture2D = null) : base(gameObject)
        {
            _gameObject = gameObject;
            _texture2D = texture2D;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            float widthPerUnit = 1f / _texture2D.Width * PixelPerUnit;
            float heightPerUnit = 1f / _texture2D.Height * PixelPerUnit;
            Vector2 scale = _gameObject.Transform.Scale * new Vector2(widthPerUnit, heightPerUnit);

            float posX = _gameObject.Transform.Position.X - (_texture2D.Width * scale.X) * Pivot.X;
            float posY = _gameObject.Transform.Position.Y - (_texture2D.Height * scale.Y) * Pivot.Y;
            Vector2 pos = new Vector2(posX, posY);

            if (_texture2D != null && spriteBatch != null)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(_texture2D, pos, null, Color, 0f, new Vector2(), scale, SpriteEffects.None, 1f);
                spriteBatch.End();
            }
        }
    }
}
