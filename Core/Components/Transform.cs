using Microsoft.Xna.Framework;

namespace Match3.Core.Components
{
    public class Transform : Component
    {
        private Vector2 _scale;

        public Vector2 Position { get; set; }
        public Vector2 Scale => _scale;

        public Transform(GameObject gameObject) : base(gameObject) 
        {
            Position = new Vector2(0, 0);
            _scale = new Vector2(1f, 1f);
        }

        public void SetScale(Vector2 scale)
        {
            if(scale.X >= 0 && scale.Y >= 0)
                _scale = scale;
        }

        public void SetScale(float x, float y) => SetScale(new Vector2(x, y));
    }
}
