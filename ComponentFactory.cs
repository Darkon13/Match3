using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3
{
    public static class ComponentFactory
    {
        public static Component GetComponent<T>(GameObject gameObject) where T : Component
        {
            if(typeof(T) == typeof(SpriteRenderer))
            {
                return new SpriteRenderer(gameObject, gameObject.DefaultTexture);
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
