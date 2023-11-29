using Match3.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3.Core.UIElements
{
    public class Canvas
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public event Action<int, int> SizeChanged;

        public Canvas(GameController gameController, RenderBuffer<UIElement> renderBuffer)
        {
            Width = Window.WindowBound.Width;
            Height = Window.WindowBound.Height;
        }

        private void OnWindowSizeChanged()
        {
            Width = Window.WindowBound.Width;
            Height = Window.WindowBound.Height;

            SizeChanged?.Invoke(Width, Height);
        }

        public void Draw()
        {

        }
    }
}
