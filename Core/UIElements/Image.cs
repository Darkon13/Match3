using Match3.Core.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Match3.Core.UIElements
{
    public class Image : RenderElement
    {
        public Texture2D Texture2D;
        private int _pixelPerUnit = 100;
        private Rectangle _rectangle;
        private int _width;
        private int _height;

        public Rectangle Rectangle => _rectangle;
        public int PixelPerUnit => _pixelPerUnit;
        public int Width => _width;
        public int Height => _height;

        public void SetPixelPerUnit(int pixelPerUnit)
        {
            if(pixelPerUnit >= 1)
            {
                _width = _width / _pixelPerUnit * pixelPerUnit;
                _height = _height / _pixelPerUnit * pixelPerUnit;

                _pixelPerUnit = pixelPerUnit;
            }
        }

        public void SetSize(Vector2 size) => SetSize((int)size.X, (int)size.Y);

        public void SetSize(int width, int height)
        {
            SetWidth(width);
            SetHeight(height);
        }

        public void SetWidth(int width)
        {
            if(width >= 0)
            {
                _width = width;
            }
        }

        public void SetHeight(int height)
        {
            if (height >= 0)
            {
                _height = height;
            }
        }

        public Image(Texture2D texture2D)
        {
            Texture2D = texture2D;
            _width = _pixelPerUnit;
            _height = _pixelPerUnit;
            _rectangle = Rectangle.Empty;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Texture2D != null && spriteBatch != null)
            {
                float widthPerUnit = (float)(Width / PixelPerUnit) / Texture2D.Width * PixelPerUnit;
                float heightPerUnit = (float)(Height / PixelPerUnit) / Texture2D.Height * PixelPerUnit;
                Vector2 scale = Scale * new Vector2(widthPerUnit, heightPerUnit);

                float posX = Position.X - Width * Pivot.X;
                float posY = Position.Y - Height * Pivot.Y;
                Vector2 pos = new Vector2(posX, posY);

                _rectangle = _rectangle.RectangleFromFloat(posX, posY, Width * Scale.X, Height * Scale.Y);
                spriteBatch.Draw(Texture2D, pos, null, Color, 0f, new Vector2(), scale, SpriteEffects.None, Layer);
            }
        }
    }
}
