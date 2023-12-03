using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Match3.Core.Utils.Input
{
    public class KeyBinder
    {
        Dictionary<InputState, Dictionary<Keys, Action>> _keyboardActions;
        Dictionary<InputState, Dictionary<MouseKeys, Action>> _mouseActions;

        public KeyBinder()
        {
            _keyboardActions = new Dictionary<InputState, Dictionary<Keys, Action>>();
            _mouseActions = new Dictionary<InputState, Dictionary<MouseKeys, Action>>();

            Init(_keyboardActions);
            Init(_mouseActions);

            KeyListener.KeyDownOnce += OnKeyboardKeyDownOnce;
            KeyListener.KeyDown += OnKeyboardKeyDown;
            KeyListener.KeyUp += OnKeyboardKeyUp;

            MouseListener.KeyDownOnce += OnMouseKeyDownOnce;
            MouseListener.KeyDown += OnMouseKeyDown;
            MouseListener.KeyUp += OnMouseKeyUp;
        }

        ~KeyBinder()
        {
            KeyListener.KeyDownOnce -= OnKeyboardKeyDownOnce;
            KeyListener.KeyDown -= OnKeyboardKeyDown;
            KeyListener.KeyUp -= OnKeyboardKeyUp;

            MouseListener.KeyDownOnce -= OnMouseKeyDownOnce;
            MouseListener.KeyDown -= OnMouseKeyDown;
            MouseListener.KeyUp -= OnMouseKeyUp;
        }

        private void Init<T>(Dictionary<InputState, Dictionary<T, Action>> actions) where T : Enum
        {
            if (ValidateType<T>())
            {
                foreach(InputState inputState in Enum.GetValues(typeof(InputState)))
                {
                    actions.Add(inputState, new Dictionary<T, Action>());
                }
            }
        }

        public void Bind(InputState inputState, Keys key, Action action) => BindAction(_keyboardActions, inputState, key, action);

        public void Bind(InputState inputState, MouseKeys key, Action action) => BindAction(_mouseActions, inputState, key, action);

        private void BindAction<T>(Dictionary<InputState, Dictionary<T, Action>> actions, InputState inputState, T key, Action action) where T : Enum
        {
            if (ValidateType<T>())
            {
                if(actions[inputState].ContainsKey(key) == true)
                {
                    actions[inputState][key] = action;
                }
                else
                {
                    actions[inputState].Add(key, action);
                }
            }
        }

        public void UnbindAllKeys()
        {
            UnbindActions(_keyboardActions);
            UnbindActions(_mouseActions);
        }

        private void UnbindActions<T>(Dictionary<InputState, Dictionary<T, Action>> actions) where T : Enum
        {
            if (ValidateType<T>())
            {
                foreach (InputState inputState in Enum.GetValues(typeof(InputState)))
                {
                    actions[inputState].Clear();
                }
            }
        }

        private void CallActions<T>(Dictionary<T, Action> actions, T key) where T : Enum
        {
            if(ValidateType<T>())
            {
                if(actions.Count > 0 && actions.Keys.Contains(key))
                {
                    actions[key].Invoke();
                }
            }
        }

        private void OnKeyboardKeyDownOnce(Keys key) => CallActions(_keyboardActions[InputState.KeyDownOnce], key);

        private void OnKeyboardKeyDown(Keys key) => CallActions(_keyboardActions[InputState.KeyDown], key);

        private void OnKeyboardKeyUp(Keys key) => CallActions(_keyboardActions[InputState.KeyUp], key);

        private void OnMouseKeyDownOnce(MouseKeys key) => CallActions(_mouseActions[InputState.KeyDownOnce], key);

        private void OnMouseKeyDown(MouseKeys key) => CallActions(_mouseActions[InputState.KeyDown], key);

        private void OnMouseKeyUp(MouseKeys key) => CallActions(_mouseActions[InputState.KeyUp], key);

        private bool ValidateType<T>() where T : Enum
        {
            return typeof(T) == typeof(MouseKeys) || typeof(T) == typeof(Keys);
        }
    }
}
