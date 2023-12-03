using Match3.Core.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Match3.Core.UIElements
{
    public class ImageElement : UIElement
    {
        public Image Image { get; private set; }

        public ImageElement(Canvas canvas, Rectangle rectangle, Texture2D texture2D) : base(canvas, rectangle) 
        {
            Image = new Image(texture2D);
        }

        public ImageElement(UIElement uIElement, Rectangle rectangle, Texture2D texture2D) : base(uIElement, rectangle) 
        {
            Image = new Image(texture2D);
        }

        protected override void OnDraw(SpriteBatch spriteBatch, RenderBuffer<UIElement> renderBuffer)
        {
            Image.Position = new Vector2(Rectangle.X, Rectangle.Y);
            Image.SetSize(Rectangle.Width, Rectangle.Height);
            Image.Draw(spriteBatch);
            Image.Layer = Layer;

            if(IsRenderTarget == true)
            {
                renderBuffer.Register(Image.Rectangle, this);
            }
        }
    }
}
