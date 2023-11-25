using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3
{
    public abstract class GameObject
    {
        protected Game1 Game;
        private List<Component> _components;

        public bool IsActive { get; private set; } = true;
        public Transform Transform { get; private set; }
        public Texture2D DefaultTexture { get; private set; }

        public GameObject(Game1 game)
        {
            Game = game;
            DefaultTexture = Game.DefaultTexture;
            Transform = new Transform(this);

            _components = new List<Component>();
            _components.Add(Transform);

            Awake();

            Game.Updated += OnUpdate;
            Game.Drawed += OnDraw;
        }

        ~GameObject()
        {
            Game.Updated -= OnUpdate;
            Game.Drawed -= OnDraw;
        }

        public bool TryGetComponent<T>(out T component) where T : Component
        {
            component = null;
            
            for(int i = 0; i < _components.Count; i++)
            {
                if(_components[i] is T)
                {
                    component = (T)_components[i];

                    break;
                }
            }

            return component != null;
        }

        public T GetComponent<T>() where T : Component
        {
            for (int i = 0; i < _components.Count; i++)
                if (_components[i] is T)
                    return (T)_components[i];

            return null;
        }

        public void AddComponent<T>() where T : Component
        {
            Component component = ComponentFactory.GetComponent<T>(this);

            if(component != null)
                _components.Add(component);
        }

        public bool TryGetContent<T>(string assetName, out T gameContent)
        {
            gameContent = default;

            if(Game.TryGetContent(assetName, out T content))
            {
                gameContent = content;

                return true;
            }

            return false;
        }

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

        protected virtual void Update() { }

        private void OnUpdate()
        {
            if(IsActive == true)
                Update();
        }

        private void OnDraw(SpriteBatch spriteBatch)
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
