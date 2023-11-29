using Match3.Core;
using Match3.Core.Components;
using Match3.Core.UIElements;
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
        private RenderBuffer<GameObject> _gameObjectRenderBuffer;
        private RenderBuffer<UIElement> _uiElementsRenderBuffer;

        private int _counter = 0;

        public event Action<TimeSpan> Updated;
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

            _gameObjectRenderBuffer = new RenderBuffer<GameObject>();
            _uiElementsRenderBuffer = new RenderBuffer<UIElement>();

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _gameController = new GameController(this, _spriteBatch, _gameObjectRenderBuffer);
            //_gameController.AddObject(new TestObject(_gameController));
            int maxCount = 10;
            for(int i = 0; i < 10; i++)
            {
                TestObject testObject = _gameController.CreateObject<TestObject>();
                if(testObject.TryGetComponent(out SpriteRenderer spriteRenderer))
                {
                    spriteRenderer.Color = new Color(Vector3.One / 10 * i);
                }
                testObject.Init(360f/maxCount * i);
            }
            //TestObject2 a1 = _gameController.CreateObject<TestObject2>();
            //if(a1.TryGetComponent(out SpriteRenderer b1))
            //{
            //    b1.SetPivot(1, 1);
            //}
            //TestObject2 a2 = _gameController.CreateObject<TestObject2>();
            //if (a2.TryGetComponent(out SpriteRenderer b2))
            //{
            //    b2.SetPivot(1, 0);
            //}
            //TestObject2 a3 = _gameController.CreateObject<TestObject2>();
            //if (a3.TryGetComponent(out SpriteRenderer b3))
            //{
            //    b3.SetPivot(0, 1);
            //}
            //TestObject2 a4 = _gameController.CreateObject<TestObject2>();
            //if (a4.TryGetComponent(out SpriteRenderer b4))
            //{
            //    b4.SetPivot(0, 0);
            //}

            //base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            Updated?.Invoke(gameTime.ElapsedGameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _gameObjectRenderBuffer.Clear();
            _uiElementsRenderBuffer.Clear();

            Drawed?.Invoke();
        }
    }
}