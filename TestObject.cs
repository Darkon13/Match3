using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3
{
    public class TestObject : GameObject
    {
        private float _radius = 100;
        private float _angle = 0;
        private Vector2 _position;

        public TestObject(Game1 game) : base(game) { }

        protected override void Awake()
        {
            Transform.Position = new Vector2(Game.Window.ClientBounds.Width/2, Game.Window.ClientBounds.Height/2);
            _position = Transform.Position;
            AddComponent<SpriteRenderer>();
        }

        protected override void Update()
        {
            _angle += 1;
            float posX = _position.X + MathF.Cos(_angle * (MathF.PI / 180)) * _radius;
            float posY = _position.Y + MathF.Sin(_angle * (MathF.PI / 180)) * _radius;

            Transform.Position = new Vector2(posX, posY);
        }
    }
}
