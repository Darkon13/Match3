using Match3.Core.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Match3.Core.UIElements
{
    public class TextElement : UIElement
    {
        public Text Text { get; private set; }

        public TextElement(Canvas canvas, Rectangle rectangle, SpriteFont font) : base(canvas, rectangle)
        {
            Text = new Text(font);
        }

        public TextElement(UIElement uiElement, Rectangle rectangle, SpriteFont font) : base(uiElement, rectangle)
        {
            Text = new Text(font);
        }

        protected override void OnDraw(SpriteBatch spriteBatch, RenderBuffer<UIElement> renderBuffer)
        {
            Text.Position = new Vector2(Rectangle.X, Rectangle.Y);
            Text.Layer = Layer;
            Text.Draw(spriteBatch);
        }
    }
}
