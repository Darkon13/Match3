using Match3.Core.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3.Core
{
    public abstract class GameObject
    {
        protected GameController GameController;
        private List<Component> _components;

        public bool IsActive { get; private set; } = true;
        public Transform Transform { get; private set; }

        public GameObject(GameController gameController)
        {
            GameController = gameController;
            Transform = new Transform(this);

            _components = new List<Component>();
            _components.Add(Transform);

            Awake();
        }

        public bool TryGetComponent<T>(out T component) where T : Component
        {
            component = GetComponent<T>();

            return component != null;
        }

        protected T GetComponent<T>() where T : Component
        {
            foreach(Component component in _components)
                if (component is T)
                    return (T)component;

            return null;
        }

        public void AddComponent<T>() where T : Component
        {
            Component component = ComponentFactory.GetComponent<T>(this, GameController);

            if(component != null)
            {
                foreach(Component componentFromGameObject in _components)
                {
                    if(componentFromGameObject is T)
                    {
                        return;
                    }
                }
            }

            _components.Add(component);
        }

        //public bool TryGetContent<T>(string assetName, out T gameContent)
        //{
        //    gameContent = default;

        //    if(Game.TryGetContent(assetName, out T content))
        //    {
        //        gameContent = content;

        //        return true;
        //    }

        //    return false;
        //}

        public void Enable() 
        { 
            IsActive = true;

            OnEnable();
        }

        public void Disable() 
        { 
            IsActive = false;

            OnDisable();
        }

        protected virtual void Awake() { }

        protected virtual void OnDisable() { }

        protected virtual void OnEnable() { }

        protected virtual void OnUpdate() { }

        public void Update()
        {
            if(IsActive == true)
                OnUpdate();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(IsActive == true)
            {
                foreach(Component component in _components)
                {
                    component.Draw(spriteBatch);
                }
            }
        }
    }
}
