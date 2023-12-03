using Match3.Core;
using Match3.Core.Components;
using Microsoft.Xna.Framework;

namespace Match3.GameObjects
{
    public class Gem : GameObject
    {
        private GemType _gemType;
        private SpriteRenderer _spriteRenderer;
        //private Animator _animator;
        private bool _inited = false;
        private float _layer = 0.1f;

        public Color Color { get; private set; }

        //public event Action ActionEnded;
        //public event Action<Point> AnimationEnded;

        public Gem(GameController gameController) : base(gameController) { }

        protected override void Awake()
        {
            _spriteRenderer = AddComponent<SpriteRenderer>();
            _spriteRenderer.Image.Layer = _layer;
            //_animator = AddComponent<Animator>();
            //_animator.SetDuration(0.1f);
            //_animator.SetAnimationController(new GemAnimationController(this));
        }

        protected override void OnDisable()
        {
            if (_inited == true)
            {
                _inited = false;

                //_gemType.Ended -= OnActionEnded;
                //_animator.AnimationEnded -= OnAnimationEnded;
            }
        }

        public void Init(GemType gemType)
        {
            if (_inited == false)
            {
                _gemType = gemType;

                Color = _gemType.View.Color;

                _spriteRenderer.Image.Texture2D = _gemType.View.Texture2D;
                _spriteRenderer.Color = _gemType.View.Color;

                //_gemType.Ended += OnActionEnded;
                //_animator.AnimationEnded += OnAnimationEnded;

                _inited = true;
            }
        }

        //public void StartAnimation(string animation, Point point)
        //{
        //    _animator.StartAnimation(animation);

        //    _point = point;
        //}

        public void Destroy(Grid grid, Point point)
        {
            Disable();
            _gemType.CallAction(grid, point);
        }

        //private void OnAnimationEnded()
        //{
        //    AnimationEnded?.Invoke(_point);
        //}

        //private void OnActionEnded()
        //{
        //    ActionEnded?.Invoke();
        //}
    }
}
