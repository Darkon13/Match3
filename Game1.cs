using Match3.Core;
using Match3.Core.Utils;
using Match3.Core.Utils.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Match3
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Window _window;
        private KeyListener _keyListener;
        private MouseListener _mouseListener;
        private GameController _gameController;

        private int _counter = 0;

        public event Action Updated;
        public event Action Drawed;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Window.AllowUserResizing = true;
            _window = new Window(Window);
            _keyListener = new KeyListener(this);
            _mouseListener = new MouseListener(this);

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _gameController = new GameController(this, _spriteBatch);
            //_gameController.AddObject(new TestObject(_gameController));
            int maxCount = 10;
            for(int i = 0; i < 10; i++)
            {
                TestObject TestObject = _gameController.CreateObject<TestObject>();
                TestObject.Init(360f/maxCount * i);
            }
            _gameController.CreateObject<TestObject2>();

            //base.Initialize();
        }

        protected override void LoadContent() { }

        protected override void Update(GameTime gameTime)
        {
            Updated?.Invoke();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Drawed?.Invoke();
        }
    }
}