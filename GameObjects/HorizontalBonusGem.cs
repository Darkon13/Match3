﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3.GameObjects
{
    public class HorizontalBonusGem : GemType
    {
        public HorizontalBonusGem(GemView gemView) : base(gemView) { }

        public override void CallAction(Grid grid, Point point)
        {
            for (int i = 0; i < grid.ColumnCount; i++)
            {
                Point pointToDestroy = new Point(i, point.Y);

                if (pointToDestroy != point)
                    grid.DestroyGem(pointToDestroy);
            }

            CallEvent();
        }
    }
}
