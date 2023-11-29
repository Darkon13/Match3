using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3.Core.Components
{
    public class TransformKeyFrame
    {
        private Transform _transform;
        private float _delta;
        private float _currentT = 0;

        public Vector2 NextPosition { get; private set; }
        public Vector2 NextScale { get; private set; }
        public Vector2 CurrentPosition { get; private set; }
        public Vector2 CurrentScale { get; private set; }

        public TransformKeyFrame(Transform transform, int ticks, TransformKeyFrame transformKeyFrame = null)
        {
            if(transformKeyFrame == null)
            {
                CurrentPosition = transform.Position;
                CurrentScale = transform.Scale;
            }
            else
            {
                CurrentPosition = transform.Position;
                CurrentScale = transform.Scale;
            }

            NextPosition = CurrentPosition;
            NextScale = CurrentScale;

            _delta = 1f / Math.Min(1, ticks);
        }

        public void SetNextScale(Vector2 nextScale)
        {
            if (nextScale.X >= 0 && nextScale.Y >= 0)
                NextScale = nextScale;
        }

        public void SetNextScale(float x, float y) => SetNextScale(new Vector2(x, y));

        public void SetNextPosition(Vector2 nextPosition) => NextPosition = nextPosition;

        public void SetNextPosition(float x, float y) => SetNextPosition(new Vector2(x, y));

        public bool MoveNext()
        {
            _currentT += _delta;

            _transform.Position = Vector2.Lerp(CurrentPosition, NextPosition, _currentT);
            _transform.Scale = Vector2.Lerp(CurrentScale, NextScale, _currentT);

            if (_currentT >= 1)
                return false;

            return true;
        }
    }
}
