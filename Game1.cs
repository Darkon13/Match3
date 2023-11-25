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
        private TestObject _testObject;

        public Texture2D DefaultTexture { get; private set; }

        public event Action Updated;
        public event Action<SpriteBatch> Drawed;

        public Game1()
        {
            Window.AllowUserResizing = true;
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            DefaultTexture = Content.Load<Texture2D>("heart");
            _testObject = new TestObject(this);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                if(_testObject.IsActive == true)
                {
                    _testObject.Disable();
                }
                else
                {
                    _testObject.Enable();
                }

            // TODO: Add your update logic here
            Updated?.Invoke();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            // TODO: Add your drawing code here
            //string text = "123";
            //if (_testObject.TryGetContent("heart", out Texture2D aaa))
            //    text = (aaa != null).ToString();

            //SpriteFont spriteFont = Content.Load<SpriteFont>("font");
            //Vector2 textMiddlePoint = spriteFont.MeasureString(text) / 2;
            //_spriteBatch.Begin();
            //Vector2 position = new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2);
            //_spriteBatch.DrawString(spriteFont, text, position, Color.White, 0, textMiddlePoint, 1.0f, SpriteEffects.None, 0.5f);
            //_spriteBatch.End();
            _spriteBatch.Begin(); 
            Drawed?.Invoke(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public bool TryGetContent<T>(string assetName, out T content)
        {
            content = default;

            try
            {
                content = Content.Load<T>(assetName);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}