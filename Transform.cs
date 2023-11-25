using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3
{
    public class Transform : Component
    {
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }

        public Transform(GameObject gameObject) : base(gameObject) 
        {
            Position = new Vector2(0, 0);
            Scale = new Vector2(1, 1);
        }
    }
}
