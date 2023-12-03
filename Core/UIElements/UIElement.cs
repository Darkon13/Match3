using Match3.Core.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Match3.Core.UIElements
{
    public abstract class UIElement
    {
        public bool IsRenderTarget = false;
        private Canvas _canvas;
        private UIElement _parent;
        private List<UIElement> _childs;
        private float _parentWidth;
        private float _parentHeight;

        public bool IsActive = true;
        public float Layer { get; protected set; } = 0;
        public Rectangle Rectangle { get; set; }
        public Texture2D DefaultTexture { get; private set; }
        public SpriteFont DefaultFont { get; private set; }

        public event Action MouseEntered;
        public event Action MouseExited;
        public event Action Clicked;
        public event Action ClickedUp;
        public event Action<int, int> SizeChanged;

        public UIElement(Canvas canvas, Rectangle rectangle)
        {
            _canvas = canvas;
            _childs = new List<UIElement>();

            Rectangle = rectangle;

            _parentWidth = _canvas.Width;
            _parentHeight = _canvas.Height;
            DefaultTexture = _canvas.DefaultTexture;
            DefaultFont = _canvas.DefaultFont;   

            _canvas.SizeChanged += OnParentSizeChanged;

            Init();
        }

        public UIElement(UIElement uiElement, Rectangle rectangle)
        {
            _parent = uiElement;
            _childs = new List<UIElement>();

            Rectangle = rectangle;

            _parentWidth = _parent.Rectangle.Width;
            _parentHeight = _parent.Rectangle.Height;
            DefaultTexture = _parent.DefaultTexture;
            DefaultFont = _parent.DefaultFont;

            _parent.SizeChanged += OnParentSizeChanged;

            Init();
        }

        ~UIElement()
        {
            if(_parent != null)
            {
                _parent.SizeChanged -= OnParentSizeChanged;
            }
            else
            {
                _canvas.SizeChanged -= OnParentSizeChanged;
            }
        }

        protected virtual void Init() { }

        public void Enable()
        {
            if(IsActive == false)
            {
                IsActive = true;
                OnEnable();
            }
        }

        protected virtual void OnEnable() { }

        public void Disable()
        {
            if (IsActive == true)
            {
                IsActive = false;
                OnDisable();
            }
        }

        protected virtual void OnDisable() { }

        public TextElement AddText(Rectangle rectangle, SpriteFont font, string text = "")
        {
            TextElement textElement = new TextElement(this, rectangle, font);
            textElement.Text.Value = text;
            _childs.Add(textElement);

            return textElement;
        }

        public ImageElement AddImage(Rectangle rectangle, Texture2D texture)
        {
            ImageElement imageElement = new ImageElement(this, rectangle, texture);
            _childs.Add(imageElement);

            return imageElement;
        }

        public void Draw(SpriteBatch spriteBatch, RenderBuffer<UIElement> renderBuffer)
        {
            if(IsActive == true)
            {
                OnDraw(spriteBatch, renderBuffer);

                if(_childs.Count > 0)
                {
                    foreach(UIElement child in _childs)
                    {
                        child.Draw(spriteBatch, renderBuffer);
                    }
                }
            }
        }

        protected virtual void OnDraw(SpriteBatch spriteBatch, RenderBuffer<UIElement> renderBuffer) { }

        public void OnMouseEnter()
        {
            if(IsActive == true)
            {
                OnMouseEnterAction();
                MouseEntered?.Invoke();
            }
        }

        protected virtual void OnMouseEnterAction() { }

        public void OnMouseExit()
        {
            if (IsActive == true)
            {
                OnMouseExitAction();
                MouseExited?.Invoke();
            }
        }

        protected virtual void OnMouseExitAction() { }

        public void OnClick()
        {
            if (IsActive == true)
            {
                OnClickAction();
                Clicked?.Invoke();
            }
        }

        protected virtual void OnClickAction() { }

        public void OnClickUp()
        {
            if (IsActive == true)
            {
                OnClickUpAction();
                ClickedUp?.Invoke();
            }
        }

        protected virtual void OnClickUpAction() { }

        private void OnParentSizeChanged(int width, int height)
        {
            //Rectangle rectangle = Rectangle.Empty;

            //float newX = Rectangle.X / _parentWidth * width;
            //float newY = Rectangle.Y / _parentHeight * height;
            //float newWidth = Rectangle.Width / _parentWidth * width;
            //float newHeight = Rectangle.Height / _parentHeight * height;

            //_parentHeight = height;
            //_parentWidth = width;

            //rectangle = Rectangle.RectangleFromFloat(newX, newY, newWidth, newHeight);
            //Rectangle = rectangle;

            //SizeChanged?.Invoke(Rectangle.Width, Rectangle.Height);
        }
    }
}
