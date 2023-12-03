using Match3.Core.Utils;
using Match3.Core.Utils.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Match3.Core.UIElements
{
    public class Canvas
    {
        private GameController _gameController;
        private List<UIElement> _uiElements;
        private RenderBuffer<UIElement> _renderBuffer;
        private Point _mousePoint;
        private UIElement _choosedElement;
        private KeyBinder _keyBinder;
        private bool _leftKeyDownOnce = false;

        public int Width { get; private set; }
        public int Height { get; private set; }
        public Texture2D DefaultTexture => _gameController.DefaultTexture;
        public SpriteFont DefaultFont => _gameController.DefaultFont;

        public event Action<int, int> SizeChanged;

        public Canvas(GameController gameController, RenderBuffer<UIElement> renderBuffer)
        {
            _renderBuffer = renderBuffer;
            _gameController = gameController;
            _uiElements = new List<UIElement>();
            _keyBinder = new KeyBinder();

            Width = Window.WindowBound.Width;
            Height = Window.WindowBound.Height;

            _mousePoint = MouseListener.MousePosition;

            _keyBinder.Bind(InputState.KeyDownOnce, MouseKeys.LeftKey, OnMouseKeyDownOnce);
            _keyBinder.Bind(InputState.KeyUp, MouseKeys.LeftKey, OnMouseKeyUp);

            Window.WindowSizeChanged += OnWindowSizeChanged;
            MouseListener.MousePositionChanged += OnMousePositionChanged;
        }

        ~Canvas()
        {
            _keyBinder.UnbindAllKeys();

            Window.WindowSizeChanged -= OnWindowSizeChanged;
            MouseListener.MousePositionChanged -= OnMousePositionChanged;
        }

        public T AddElement<T>(Rectangle rectangle) where T : UIElement
        {
            if(typeof(T) == typeof(Button))
            {
                Button button = new Button(this, rectangle);
                _uiElements.Add(button);

                return button as T;
            }else if(typeof(T) == typeof(TextElement))
            {
                TextElement text = new TextElement(this, rectangle, DefaultFont);
                _uiElements.Add(text);

                return text as T;
            }else if(typeof(T) == typeof(ImageElement))
            {
                ImageElement image = new ImageElement(this, rectangle, DefaultTexture);
                _uiElements.Add(image);

                return image as T;
            }

            return null;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(_uiElements.Count > 0)
            {
                foreach(UIElement uiElement in _uiElements)
                {
                    uiElement.Draw(spriteBatch, _renderBuffer);
                }
            }
        }

        private bool RaycastOnMousePoint(out UIElement element)
        {
            element = null;

            if (_renderBuffer.TryGetObjects(_mousePoint, out List<UIElement> uiElements))
            {
                element = _uiElements[0];

                for (int i = 1; i < uiElements.Count; i++)
                {
                    if (uiElements[i].Layer < element.Layer)
                    {
                        element = uiElements[i];
                    }
                }
            }

            return element != null;
        }

        private void OnMouseKeyDownOnce()
        {
            if(_choosedElement != null)
            {
                _choosedElement.OnClick();
                _leftKeyDownOnce = true;
            }
        }

        private void OnMouseKeyUp()
        {
            if(_leftKeyDownOnce == true && _choosedElement != null)
            {
                _choosedElement.OnClickUp();
            }

            _leftKeyDownOnce = false;
        }

        private void OnMousePositionChanged(Point point)
        {
            _mousePoint = point;

            if (RaycastOnMousePoint(out UIElement uiElement))
            {
                if(_choosedElement != null)
                {
                    _choosedElement.OnMouseExit();
                }

                _choosedElement = uiElement;
                _choosedElement.OnMouseEnter();

                return;
            }

            if(_choosedElement != null)
            {
                _choosedElement.OnMouseExit();
                _choosedElement = null;
            }
        }

        private void OnWindowSizeChanged()
        {
            Width = Window.WindowBound.Width;
            Height = Window.WindowBound.Height;

            SizeChanged?.Invoke(Width, Height);
        }
    }
}
