﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3.GameObjects
{
    public class BaseGem : GemType
    {
        public BaseGem(GemView gemView) : base(gemView) { }

        public override void CallAction(Grid grid, Point point)
        {
            CallEvent();
        }
    }
}
