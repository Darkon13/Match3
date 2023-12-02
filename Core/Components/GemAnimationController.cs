using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3.Core.Components
{
    public class GemAnimationController : AnimationController
    {
        private const string CreateAnimationName = "Create";
        private const string DestroyAnimationName = "Destroy";

        public GemAnimationController(GameObject gameObject) : base(gameObject)
        {
            RegisterAnimation(CreateAnimationName, new CreateAnimation(gameObject));
            RegisterAnimation(DestroyAnimationName, new DestroyAnimation(gameObject));
        }
    }
}
