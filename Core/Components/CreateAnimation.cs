using Microsoft.Xna.Framework;
using System;

namespace Match3.Core.Components
{
    public class CreateAnimation : Animation
    {
        private float _currentT = 0;
        private float _ticks = 5;
        private Vector2 _scale;

        public CreateAnimation(GameObject gameObject) : base(gameObject) { }

        protected override void StartAction()
        {
            _scale = GameObject.Transform.Scale;
        }

        protected override bool Animate()
        {
            if(_currentT < 1f)
            {
                GameObject.Transform.SetScale(Vector2.Lerp(Vector2.Zero, _scale, _currentT));

                _currentT = MathF.Min(1f, _currentT + 1f / _ticks);

                return false;
            }
            else
            {
                return true;
            }
        }

        protected override void StopAction()
        {
            GameObject.Transform.SetScale(_scale);
        }
    }
}
