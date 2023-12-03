using Microsoft.Xna.Framework.Input;
using System;

namespace Match3.Core.Utils.Input
{
    public class KeyListener
    {
        private static Game1 _game;
        private static Keys _lastKey = Keys.None;
        
        public static event Action<Keys> KeyDownOnce;
        public static event Action<Keys> KeyDown;
        public static event Action<Keys> KeyUp;

        public KeyListener(Game1 game)
        {
            if(_game == null)
            {
                _game = game;

                _game.Updated += Update;
            }
        }

        private static void Update(TimeSpan tick)
        {
            foreach(Keys key in Enum.GetValues(typeof(Keys)))
            {
                if (Keyboard.GetState().IsKeyDown(key))
                {
                    if(_lastKey == Keys.None)
                    {
                        _lastKey = key;

                        KeyDownOnce?.Invoke(_lastKey);
                    }

                    KeyDown?.Invoke(_lastKey);

                    break;
                }

                if (key != Keys.None && Keyboard.GetState().IsKeyUp(_lastKey))
                {
                    KeyUp?.Invoke(_lastKey);

                    _lastKey = Keys.None;
                }
            }
        }
    }
}
