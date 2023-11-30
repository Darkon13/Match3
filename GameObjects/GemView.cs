using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Match3.GameObjects
{
    public class GemView
    {
        public Color Color { get; private set; }
        public Texture2D Texture2D { get; private set; }

        public GemView(Color color, Texture2D texture2D)
        {
            Color = color;
            Texture2D = texture2D;
        }
    }
}
