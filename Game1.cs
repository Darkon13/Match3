using Match3.Core;
using Match3.Core.Components;
using Match3.Core.UIElements;
using Match3.Core.Utils;
using Match3.Core.Utils.Input;
using Match3.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private Canvas _canvas;

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
            _gameController = new GameController(this, _spriteBatch, _gameObjectRenderBuffer, _uiElementsRenderBuffer);

            _canvas = new Canvas(_gameController, _uiElementsRenderBuffer);

            //Rectangle buttonRect = new Rectangle(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2, 200, 200);
            //Button button = _canvas.AddElement<Button>(buttonRect);

            ObjectPool<Gem> objectPool = _gameController.CreateObject<ObjectPool<Gem>>();
            objectPool.Init(64);

            GemViewFactory gemViewFactory = new GemViewFactory(_gameController);

            GemFactory gemFactory = new GemFactory(gemViewFactory);

            Grid grid = _gameController.CreateObject<Grid>();
            grid.Init(8, 8, gemFactory, objectPool, 48);

            GridSelector gridSelector = _gameController.CreateObject<GridSelector>();
            gridSelector.Init(grid);
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

            _spriteBatch.Begin(SpriteSortMode.FrontToBack);
            _canvas.Draw(_spriteBatch);
            Drawed?.Invoke();
            _spriteBatch.End();
        }
    }
}