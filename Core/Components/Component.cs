using Microsoft.Xna.Framework.Graphics;
using Match3.Core.Utils;

namespace Match3.Core.Components
{
    public abstract class Component
    {
        public GameObject GameObject { get; private set; }

        public Component(GameObject gameObject) => GameObject = gameObject;

        public bool TryGetComponent<T>(out T component) where T : Component
        {
            component = null;

            if (GameObject.TryGetComponent(out T gameObjectComponent))
                component = gameObjectComponent;

            return component != null;
        }

        public virtual void Draw(SpriteBatch spriteBatch, RenderBuffer<GameObject> renderBuffer) { }
    }
}
