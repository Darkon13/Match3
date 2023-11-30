using Match3.Core;
using Match3.Core.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3.GameObjects
{
    public class Gem : GameObject
    {
        private GemType _gemType;
        private SpriteRenderer _spriteRenderer;
        private bool _inited = false;
        private float _layer = 0.1f;

        public event Action ActionEnded;

        public Gem(GameController gameController) : base(gameController) { }

        protected override void Awake()
        {
            _spriteRenderer = AddComponent<SpriteRenderer>();
            _spriteRenderer.Layer = _layer;
        }

        protected override void OnDisable()
        {
            if (_inited == true)
            {
                _gemType.Ended -= OnActionEnded;

                _inited = false;
            }
        }

        public void Init(GemType gemType)
        {
            if (_inited == false)
            {
                _gemType = gemType;

                _spriteRenderer.Texture2D = _gemType.View.Texture2D;
                _spriteRenderer.Color = _gemType.View.Color;

                _gemType.Ended += OnActionEnded;

                _inited = true;
            }
        }

        public void Destroy(Grid grid, Point point)
        {
            _gemType.CallAction(grid, point);
        }

        public void OnActionEnded()
        {
            ActionEnded?.Invoke();

            Disable();
        }
    }
}
