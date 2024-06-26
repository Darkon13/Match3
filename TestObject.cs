﻿using Match3.Core;
using Match3.Core.Components;
using Match3.Core.Utils;
using Match3.Core.Utils.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3
{
    public class TestObject : GameObject
    {
        private float _radius = 200;
        private float _angle = 0;
        private Vector2 _position;
        private KeyBinder _binder;
        private bool _inited;
        //private Coroutine _coroutine;
        private TimeSpan _delay;
        private Timer _timer;

        public TestObject(GameController gameController) : base(gameController) { }

        protected override void Awake()
        {
            
            Transform.Position = new Vector2(Window.WindowBound.Width / 2, Window.WindowBound.Height / 2);
            _position = Transform.Position;
            AddComponent<SpriteRenderer>();
            //AddComponent<SpriteRenderer>();
            //AddComponent<SpriteRenderer>();
            _binder = new KeyBinder();
            //TestAnimation testAnimation = new TestAnimation();

            //Random random = new Random();
            //_angle = random.NextSingle() * 360;

            Window.WindowSizeChanged += OnWindowSizeChanged;
            //_binder.Bind(InputState.KeyDownOnce, MouseKeys.LeftKey, OnKeyDown);
            //MouseListener.KeyDownOnce += OnKeyDown;
        }

        public void Init(float startAngle)
        {
            if(_inited == false)
            {
                _angle = startAngle;
                //_coroutine = new Coroutine(Rotate());
                _delay = TimeSpan.FromSeconds(0);
                //_angle += 1;
                float posX = _position.X + MathF.Cos(_angle * (MathF.PI / 180)) * _radius;
                float posY = _position.Y + MathF.Sin(_angle * (MathF.PI / 180)) * _radius;

                Transform.Position = new Vector2(posX, posY);
                _timer = CreateTimer(new Random().NextSingle());
                _timer.Ended += Rotate;

                _inited = true;
            }
        }

        protected override void OnUpdate()
        {
            if(_inited == true)
            {

                //_coroutine.Update();
                //if(_coroutine._enumerator == null);
            }
        }

        private void Rotate()
        {
            _angle += 1;
            float posX = _position.X + MathF.Cos(_angle * (MathF.PI / 180)) * _radius;
            float posY = _position.Y + MathF.Sin(_angle * (MathF.PI / 180)) * _radius;

            Transform.Position = new Vector2(posX, posY);

            _timer.Start();
        }

        private void OnWindowSizeChanged()
        {
            Transform.Position = new Vector2(Window.WindowBound.Width / 2, Window.WindowBound.Height / 2);
            _position = Transform.Position;
        }

        private void OnKeyDown()
        {
            if(IsActive == true)
            {
                Disable();
            }
            else
            {
                Enable();
            }
        }
    }
}
