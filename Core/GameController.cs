using Match3.Core.Utils;
using Match3.Core.Utils.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3.Core
{
    public class GameController
    {
        private Game1 _game;
        private SpriteBatch _spriteBatch;
        private List<GameObject> _gameObjects;
        private RenderBuffer<GameObject> _gameObjectRenderBuffer;
        private GameObject _selected;
        private Vector2 _point;

        private Point _mousePoint;
        private KeyBinder _keyBinder;

        public Texture2D DefaultTexture { get; private set; }

        public GameController(Game1 game, SpriteBatch spriteBatch, RenderBuffer<GameObject> renderBuffer)
        {
            _game = game;
            _spriteBatch = spriteBatch;
            _gameObjectRenderBuffer = renderBuffer;
            _mousePoint = MouseListener.MousePosition;

            _keyBinder = new KeyBinder();
            _keyBinder.Bind(InputState.KeyDownOnce, MouseKeys.LeftKey, OnMouseKeyDownOnce);
            _keyBinder.Bind(InputState.KeyDown, MouseKeys.LeftKey, OnMouseKeyDown);
            _keyBinder.Bind(InputState.KeyUp, MouseKeys.LeftKey, OnMouseKeyUp);

            _gameObjects = new List<GameObject>();

            if (TryGetContent("heart", out Texture2D texture2D))
            {
                DefaultTexture = texture2D;
            }

            MouseListener.MousePositionChanged += OnMousePositionChanged;
            _game.Updated += UpdateObjects;
            _game.Drawed += DrawObjects;
        }

        ~GameController()
        {
            _game.Updated -= UpdateObjects;
            _game.Drawed -= DrawObjects;
        }

        public T CreateObject<T>() where T : GameObject
        {
            T gameObject = GameObjectFactory.Create<T>(this);

            if(gameObject != null)
            {
                _gameObjects.Add(gameObject);
            }

            return gameObject;
        }

        public bool TryGetContent<T>(string assetName, out T content)
        {
            content = default;

            try
            {
                content = _game.Content.Load<T>(assetName);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void OnMouseKeyDownOnce()
        {
            if(_gameObjectRenderBuffer.TryGetObjects(_mousePoint, out List<GameObject> gameObjects))
            {
                foreach(GameObject gameObject in gameObjects)
                {
                    _selected = gameObject;
                    _point = _selected.Transform.Position - _mousePoint.ToVector2();
                }
            }
        }

        private void OnMouseKeyDown()
        {
            if (_selected != null)
            {
                _selected.Transform.Position = (_mousePoint).ToVector2() + _point;
            }
        }

        private void OnMouseKeyUp()
        {
            _selected = null;
        }

        private void OnMousePositionChanged(Point point)
        {
            _mousePoint = point;
        }

        private void UpdateObjects(TimeSpan gameTime)
        {
            foreach(GameObject gameObject in _gameObjects)
            {
                gameObject.Update(gameTime);
            }
        }

        private void DrawObjects()
        {
            foreach (GameObject gameObject in _gameObjects)
            {
                gameObject.Draw(_spriteBatch, _gameObjectRenderBuffer);
            }
        }
    }
}
