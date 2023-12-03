using Microsoft.Xna.Framework;
using System;

namespace Match3
{
    public class Window
    {
        private static GameWindow _window;

        public static Rectangle WindowBound { get; private set; } = Rectangle.Empty;

        public static event Action WindowSizeChanged;

        public Window(GameWindow window)
        {
            if(_window == null)
            {
                _window = window;

                WindowBound = _window.ClientBounds;

                _window.ClientSizeChanged += OnWindowSizeChanged;
            }
        }

        private static void OnWindowSizeChanged<TEventArgs>(object? sender, TEventArgs e)
        {
            WindowBound = _window.ClientBounds;

            WindowSizeChanged?.Invoke();
        }
    }
}
