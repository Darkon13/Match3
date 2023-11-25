using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3
{
    public abstract class Component
    {
        private GameObject _gameObject;

        public Component(GameObject gameObject) => _gameObject = gameObject;

        public T GetComponent<T>() where T : Component
        {
            return _gameObject.GetComponent<T>();
        }

        public bool TryGetComponent<T>(out T component) where T : Component
        {
            component = null;

            if (_gameObject.TryGetComponent(out T gameObjectComponent))
                component = gameObjectComponent;

            return component != null;
        }

        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}
