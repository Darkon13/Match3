using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3.Core.Utils
{
    public static class RectangleExtension
    {
        public static Rectangle RectangleFromFloat(this Rectangle rectangle, float x, float y, float width, float height)
        {
            return new Rectangle((int) x, (int) y, (int) width, (int) height);
        }
    }
}
