using Match3.Core.Components;
using Match3.Core.UIElements;
using Match3.Core.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Match3.Core
{
    public class GameController
    {
        private const string DefaultTextureName = "square";
        private const string DefaultFontName = "font";

        private Game1 _game;
        private SpriteBatch _spriteBatch;
        private List<GameObject> _gameObjects;
        private RenderBuffer<GameObject> _gameObjectRenderBuffer;
        private RenderBuffer<UIElement> _uiRenderBuffer;

        public Texture2D DefaultTexture { get; private set; }
        public SpriteFont DefaultFont { get; private set; }

        public GameController(Game1 game, SpriteBatch spriteBatch, RenderBuffer<GameObject> gameObjectRenderBuffer, RenderBuffer<UIElement> uiRenderBuffer)
        {
            _game = game;
            _spriteBatch = spriteBatch;
            _gameObjectRenderBuffer = gameObjectRenderBuffer;
            _uiRenderBuffer = uiRenderBuffer;
            _gameObjects = new List<GameObject>();

            if (TryGetContent(DefaultTextureName, out Texture2D texture2D))
            {
                DefaultTexture = texture2D;
            }

            if (TryGetContent(DefaultFontName, out SpriteFont font))
            {
                DefaultFont = font;
            }

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

        public bool Raycast(Point point, out GameObject gameObject)
        {
            gameObject = null;

            if(_uiRenderBuffer.TryGetObjects(point, out List<UIElement> uiElement) == false){
                if(_gameObjectRenderBuffer.TryGetObjects(point, out List<GameObject> objects))
                {
                    float layer = 0;

                    foreach(GameObject @object in objects)
                    {
                        if(@object.TryGetComponent(out SpriteRenderer spriteRenderer))
                        {
                            if(gameObject == null || spriteRenderer.Layer > layer)
                            {
                                gameObject = @object;
                                layer = spriteRenderer.Layer;
                            }
                        }
                    }

                    return true;
                }
            }

            return false;
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
