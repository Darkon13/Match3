using Match3.Core.Components;
using Match3.Core.Utils;
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
        private TimerController _timerController;

        public bool IsActive { get; private set; } = true;
        public Transform Transform { get; private set; }

        public GameObject(GameController gameController)
        {
            GameController = gameController;
            _timerController = new TimerController();
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

        public Timer CreateTimer(float seconds)
        {
            Timer timer = _timerController.CreateTimer();
            timer.SetDuration(seconds);

            return timer;
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

        protected virtual void OnUpdate() { }

        public void Update(TimeSpan tick)
        {
            if(IsActive == true)
            {
                OnUpdate();
                _timerController.Update(tick);
            }
        }

        public void Draw(SpriteBatch spriteBatch, RenderBuffer<GameObject> renderBuffer)
        {
            if(IsActive == true)
            {
                foreach(Component component in _components)
                {
                    component.Draw(spriteBatch, renderBuffer);
                }
            }
        }
    }
}
