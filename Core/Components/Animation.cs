using System;

namespace Match3.Core.Components
{
    public abstract class Animation
    {
        protected GameObject GameObject;

        public event Action Ended;

        public Animation(GameObject gameObject)
        {
            GameObject = gameObject;
        }

        public void Start()
        {
            StartAction();
        }

        protected virtual void StartAction() { }

        public void Update()
        {
            if(Animate() == true)
                Ended?.Invoke();
        }

        protected abstract bool Animate();

        public void StopAnimation()
        {
            StopAction();
        }

        protected abstract void StopAction();
    }
}
