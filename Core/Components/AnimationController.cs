using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3.Core.Components
{
    public abstract class AnimationController
    {
        protected GameObject GameObject;
        private Dictionary<string, Animation> _animations;
        private Animation _currentAnimation;

        public event Action AnimationEnded;

        public AnimationController(GameObject gameObject)
        {
            GameObject = gameObject;
            _animations = new Dictionary<string, Animation>();
        }

        public void Update()
        {
            if(_currentAnimation != null)
                _currentAnimation.Update();
        }

        public bool StartAnimation(string animationName)
        {
            if (_animations.ContainsKey(animationName))
            {
                _currentAnimation = _animations[animationName];
                _currentAnimation.Ended += OnAnimationEnd;
                _currentAnimation.Start();
                _currentAnimation.Update();

                return true;
            }

            return false;
        }

        public bool StopAnimation()
        {
            if(_currentAnimation != null)
            {
                _currentAnimation.StopAnimation();
                _currentAnimation.Ended -= OnAnimationEnd;
                _currentAnimation = null;

                return true;
            }

            return false;
        }

        protected bool RegisterAnimation(string animationName, Animation animation)
        {
            if(animationName != "" && animation != null)
            {
                if (_animations.ContainsKey(animationName))
                {
                    _animations[animationName] = animation;
                }
                else
                {
                    _animations.Add(animationName, animation);
                }

                return true;
            }

            return false;
        }

        private void OnAnimationEnd()
        {
            if(_currentAnimation != null)
            {
                _currentAnimation.Ended -= AnimationEnded;
                _currentAnimation = null;
            }

            AnimationEnded?.Invoke();
        }
    }
}
