using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
