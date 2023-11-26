using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3.Core.Components
{
    public static class ComponentFactory
    {
        public static Component GetComponent<T>(GameObject gameObject, GameController gameController) where T : Component
        {
            if(typeof(T) == typeof(SpriteRenderer))
            {
                return new SpriteRenderer(gameObject, gameController.DefaultTexture);
            }
            else if(typeof(T) == typeof(Transform))
            {
                return new Transform(gameObject);
            }
            else
            {
                return null;
            }
        }
    }
}
