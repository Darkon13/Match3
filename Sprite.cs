using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3
{
    public class Sprite
    {
        private Texture2D _texture2D;

        public int Height => _texture2D.Height;
        public int Width => _texture2D.Width;

        public Sprite(Texture2D texture2D)
        {
            _texture2D = texture2D;
        }
    }
}
