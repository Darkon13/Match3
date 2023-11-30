using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3.Core.Utils
{
    public static class PointExtension
    {
        public static bool CheckDistance(this Point point, Point secondPoint, Func<Point, Point, bool> func)
        {
            return func.Invoke(point, secondPoint);
        }
    }
}
