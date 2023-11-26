using Match3.Core;
using Match3.Core.Components;
using Match3.Core.Utils;
using Match3.Core.Utils.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3
{
    public class TestObject2 : GameObject
    {
        public TestObject2(GameController gameController) : base(gameController) { }

        protected override void Awake()
        {
            AddComponent<SpriteRenderer>();
            MouseListener.MousePositionChanged += OnMousePositionChanged;
        }

        protected override void OnEnable()
        {
            MouseListener.MousePositionChanged += OnMousePositionChanged;
        }

        protected override void OnDisable()
        {
            MouseListener.MousePositionChanged -= OnMousePositionChanged;
        }

        private void OnMousePositionChanged(Point point)
        {
            Transform.Position = new Vector2(point.X, point.Y);
        }
    }
}
