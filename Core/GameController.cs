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

        public Texture2D DefaultTexture { get; private set; }

        public GameController(Game1 game, SpriteBatch spriteBatch)
        {
            _game = game;
            _spriteBatch = spriteBatch;

            _gameObjects = new List<GameObject>();

            if (TryGetContent("heart", out Texture2D texture2D))
            {
                DefaultTexture = texture2D;
            }

            _game.Updated += UpdateObjects;
            _game.Drawed += DrawObjects;
        }

        ~GameController()
        {
            _game.Updated -= UpdateObjects;
            _game.Drawed -= DrawObjects;
        }

        //public void AddObject(GameObject gameObject)
        //{
        //    _gameObjects.Add(gameObject);
        //}

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

        private void UpdateObjects()
        {
            foreach(GameObject gameObject in _gameObjects)
            {
                gameObject.Update();
            }
        }

        private void DrawObjects()
        {
            foreach (GameObject gameObject in _gameObjects)
            {
                gameObject.Draw(_spriteBatch);
            }
        }

    }
}
