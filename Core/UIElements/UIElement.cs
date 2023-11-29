using Match3.Core.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3.Core.UIElements
{
    public abstract class UIElement
    {
        private Canvas _canvas;
        private UIElement _parent;
        private float _parentWidth;
        private float _parentHeight;
        private Rectangle _localRectangle;
        private Rectangle _globalRectangle;

        public event Action MouseEntered;
        public event Action MouseExited;
        public event Action Clicked;
        public event Action ClickedUp;
        public event Action<int, int> SizeChanged;

        public UIElement(Canvas canvas)
        {
            _canvas = canvas;

            _localRectangle = Rectangle.Empty;
            _globalRectangle = _localRectangle;

            _parentWidth = _canvas.Width;
            _parentHeight = _canvas.Height;

            _canvas.SizeChanged += OnParentSizeChanged;
        }

        public UIElement(UIElement uiElement)
        {
            _parent = uiElement;

            _localRectangle = Rectangle.Empty;
            _globalRectangle = _localRectangle;

            _parentWidth = _parent._globalRectangle.Width;
            _parentHeight = _parent._globalRectangle.Height;

            _parent.SizeChanged += OnParentSizeChanged;
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

        public void SetSize(int width, int height)
        {
            Rectangle rectangle = Rectangle.Empty;
            //rectangle = rectangle.RectangleFromFloat(_globalRectangle.Width)
        }

        public void OnMouseEnter()
        {
            OnMouseEnterAction();
            MouseEntered?.Invoke();
        }

        protected virtual void OnMouseEnterAction() { }

        public void OnMouseExit()
        {
            OnMouseExitAction();
            MouseExited?.Invoke();
        }

        protected virtual void OnMouseExitAction() { }

        public void OnClick()
        {
            OnClickAction();
            Clicked?.Invoke();
        }

        protected virtual void OnClickAction() { }

        public void OnClickUp()
        {
            OnClickUpAction();
            ClickedUp?.Invoke();
        }

        protected virtual void OnClickUpAction() { }

        private void OnParentSizeChanged(int width, int height)
        {
            Rectangle rectangle = Rectangle.Empty;
            rectangle.RectangleFromFloat(_globalRectangle.X / _parentWidth * width,
                                         _globalRectangle.Y / _parentHeight * height,
                                         _globalRectangle.Width / _parentWidth * width,
                                         _globalRectangle.Height / _parentHeight * height);

            _globalRectangle = rectangle;

            _parentWidth = width;
            _parentHeight = height;

            SizeChanged?.Invoke(_globalRectangle.Width, _globalRectangle.Height);
        }
    }
}
