using Match3.Core.UIElements;
using Match3.Core.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Match3.Core.Components
{
    public class SpriteRenderer : Component
    {
        public bool IsRenderTarget = false;

        public Image Image { get; private set; }
        public Color Color { get; set; } = Color.White;
        public int PixelPerUnit => Image.PixelPerUnit;
        public float Layer => Image.Layer;
        public Vector2 Pivot => Image.Pivot;

        public SpriteRenderer(GameObject gameObject, Texture2D texture2D = null) : base(gameObject)
        {
            Image = new Image(texture2D);
        }

        public override void Draw(SpriteBatch spriteBatch, RenderBuffer<GameObject> renderBuffer)
        {
            Image.Position = GameObject.Transform.Position;
            Image.SetScale(GameObject.Transform.Scale);
            Image.Draw(spriteBatch);
            Image.Color = Color;
            Image.Layer = Layer;

            if(IsRenderTarget == true)
            {
                renderBuffer.Register(Image.Rectangle, GameObject);
            }
        }
    }
}
