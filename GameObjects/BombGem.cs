using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3.GameObjects
{
    public class BombGem : GemType
    {
        private int _radius = 3;

        public BombGem(GemView gemView) : base(gemView) { }

        public override void CallAction(Grid grid, Point point)
        {
            int maxX = Math.Min(grid.LineCount - 1, point.X + _radius);
            int maxY = Math.Min(grid.ColumnCount - 1, point.Y + _radius);
            int minX = Math.Max(0, point.X - _radius);
            int minY = Math.Max(0, point.Y - _radius);

            for(int i = minX; i <= maxX; i++)
            {
                for(int j = minY; j <= maxY; j++)
                {
                    Point destroyPoint = new Point(i, j);

                    if(destroyPoint != point && Vector2.Distance(destroyPoint.ToVector2(), point.ToVector2()) <= _radius)
                        grid.DestroyGem(new Point(i, j));
                }
            }

            CallEvent();
        }
    }
}
