using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3.Core.Utils.Input
{
    public class MouseListener
    {
        private static Game1 _game;
        private static MouseKeys _lastKey = MouseKeys.None;
        private static Point _mousePosition = Point.Zero;

        public static Point MousePosition => _mousePosition;

        public static event Action<MouseKeys> KeyDownOnce;
        public static event Action<MouseKeys> KeyDown;
        public static event Action<MouseKeys> KeyUp;
        public static event Action<Point> MousePositionChanged;

        public MouseListener(Game1 game)
        {
            if(_game == null)
            {
                _game = game;

                _mousePosition = Mouse.GetState().Position;

                _game.Updated += Update;
            }
        }

        private static void Update()
        {
            MouseState mouseState = Mouse.GetState();

            ClickHandler(mouseState);
            PositionHandler(mouseState);
        }

        private static void ClickHandler(MouseState mouseState)
        {
            if(CheckClick(mouseState, ButtonState.Pressed, out MouseKeys keyPressed))
            {
                if(_lastKey == MouseKeys.None)
                {
                    _lastKey = keyPressed;

                    KeyDownOnce?.Invoke(_lastKey);
                }

                KeyDown?.Invoke(_lastKey);

                return;
            }

            //if(_lastKey != MouseKeys.None && CheckClick(mouseState, ButtonState.Released, out MouseKeys keyReleased))
            //{
            //    KeyUp?.Invoke(_lastKey);

            //    _lastKey = MouseKeys.None;
            //}

            if(_lastKey != MouseKeys.None)
            {
                KeyUp?.Invoke(_lastKey);

                _lastKey = MouseKeys.None;
            }
        }

        private static void PositionHandler(MouseState mouseState)
        {
            if(mouseState.Position != _mousePosition)
            {
                _mousePosition = mouseState.Position;

                MousePositionChanged?.Invoke(_mousePosition);
            }
        }

        private static bool CheckClick(MouseState mouseState, ButtonState buttonState, out MouseKeys key)
        {
            key = MouseKeys.None;

            if (mouseState.LeftButton == buttonState)
            {
                key = MouseKeys.LeftKey;
            }
            else if (mouseState.MiddleButton == buttonState)
            {
                key = MouseKeys.MiddleKey;
            }
            else if (mouseState.RightButton == buttonState)
            {
                key = MouseKeys.RightKey;
            }

            return key != MouseKeys.None; 
        }
    }
}
