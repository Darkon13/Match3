using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Match3.Core.UIElements
{
    public class Text : RenderElement
    {
        public string Value;
        public SpriteFont Font;

        public Text(SpriteFont spriteFont, string text = "")
        {
            Font = spriteFont;
            Value = text;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(spriteBatch != null && Font != null)
            {
                Vector2 stringSize = Font.MeasureString(Value);

                float posX = Position.X - stringSize.X * Pivot.X;
                float posY = Position.Y - stringSize.Y * Pivot.Y;
                Vector2 position = new Vector2(posX, posY);

                spriteBatch.DrawString(Font, Value, position, Color, 0, Vector2.Zero, Scale, SpriteEffects.None, Layer);
            }
        }
    }
}
