using Match3.Core.Utils;
using System;

namespace Match3.Core.Components
{
    public class Animator : Component
    {
        private AnimationController _animationController;
        private Timer _timer;

        public event Action AnimationEnded;

        public Animator(GameObject gameObject, Timer timer) : base(gameObject) 
        { 
            _timer = timer;
            _timer.Ended += UpdateAnimation;

            _timer.Start();
        }

        public void SetDuration(double seconds)
        {
            if(seconds > 0)
            {
                _timer.SetDuration(seconds);
            }
        }

        public void StartAnimation(string animation)
        {
            if(_animationController != null)
            {
                _animationController.StartAnimation(animation);
            }
        }

        public void StopAnimation()
        {
            if(_animationController != null)
            {
                _animationController.StopAnimation();
            }
        }

        public void SetAnimationController(AnimationController animationController)
        {
            if(animationController != null)
            {
                if(_animationController != null)
                {
                    _animationController.AnimationEnded -= OnAnimationEnded;
                }

                _animationController = animationController;
                _animationController.AnimationEnded += OnAnimationEnded;
            }
        }

        private void OnAnimationEnded()
        {
            AnimationEnded?.Invoke();
        }

        private void UpdateAnimation()
        {
            if(_animationController != null && GameObject.IsActive == true)
            {
                _animationController.Update();

            }
            
            _timer.Start();
        }
    }
}
