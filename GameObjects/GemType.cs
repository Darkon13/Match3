using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3.GameObjects
{
    public abstract class GemType
    {
        public GemView View { get; private set; }
        
        public event Action Ended;
        
        public GemType(GemView gemView)
        {
            View = gemView;
        }

        public abstract void CallAction(Grid grid, Point point);

        protected void CallEvent()
        {
            Ended?.Invoke();
        }
    }
}
