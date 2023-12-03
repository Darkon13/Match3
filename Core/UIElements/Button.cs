using Match3.Core.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Match3.Core.UIElements
{
    public class Button : UIElement
    {
        public string Text = "Button";
        private ImageElement _imageBound;
        private ImageElement _imageInside;
        private TextElement _text;

        public Button(Canvas canvas, Rectangle rectangle) : base(canvas, rectangle) {}

        public Button(UIElement uiElement, Rectangle rectangle) : base(uiElement, rectangle) {}

        protected override void Init()
        {
            IsRenderTarget = true;

            Rectangle imageBoundRect = new Rectangle(Rectangle.X / 2, Rectangle.Y / 2, Rectangle.Width, Rectangle.Height);
            _imageBound = AddImage(Rectangle, DefaultTexture);
            _imageBound.Image.Color = Color.Black;

            Rectangle imageInsideRect = new Rectangle(Rectangle.X / 2, Rectangle.Y / 2, Rectangle.Width, Rectangle.Height);
            _imageInside = AddImage(Rectangle, DefaultTexture);
            _imageInside.Image.SetScale(Vector2.One / 1.2f);

            _text = AddText(Rectangle, DefaultFont, Text);
        }

        protected override void OnMouseEnterAction()
        {
            _imageInside.Image.Color = Color.Gray;
        }

        protected override void OnMouseExitAction()
        {
            _imageInside.Image.Color = Color.White;
        }
    }
}
