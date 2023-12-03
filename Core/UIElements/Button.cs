using Match3.Core.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Match3.Core.UIElements
{
    public class Button : UIElement
    {
        public string Text = "Button";
        private ImageElement _imageInside;
        private TextElement _text;

        public Button(Canvas canvas, Rectangle rectangle) : base(canvas, rectangle) {}

        public Button(UIElement uiElement, Rectangle rectangle) : base(uiElement, rectangle) {}

        protected override void Init()
        {
            _imageInside = AddImage(Rectangle, DefaultTexture);

            _text = AddText(Rectangle, DefaultFont, Text);
            _text.Text.Color = Color.Black;
        }

        protected override void OnDraw(SpriteBatch spriteBatch, RenderBuffer<UIElement> renderBuffer)
        {
            renderBuffer.Register(_imageInside.Image.Rectangle, this);
        }

        protected override void OnMouseEnterAction()
        {
            _imageInside.Image.Color = Color.Gray;
        }

        protected override void OnMouseExitAction()
        {
            _imageInside.Image.Color = Color.White;
        }

        protected override void OnClickAction()
        {
            _imageInside.Image.Color = Color.DarkGray;
            _text.Text.Color = Color.Gray;
        }

        protected override void OnClickUpAction()
        {
            _imageInside.Image.Color = Color.White;
            _text.Text.Color = Color.Black;
        }
    }
}
