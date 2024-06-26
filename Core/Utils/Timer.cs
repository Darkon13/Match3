﻿using System;

namespace Match3.Core.Utils
{
    public class Timer
    {
        private TimeSpan _current;
        private TimeSpan _duration;
        private bool _isRunning = false;


        public event Action Ended;

        public Timer()
        {
            _duration = TimeSpan.FromSeconds(1f);
            _current = TimeSpan.Zero;
        }

        public void SetDuration(double seconds) 
        {
            if(seconds > 0)
            {
                _duration = TimeSpan.FromSeconds(seconds);
            }
        }

        public void Start() => _isRunning = true;

        public void Stop()
        {
            _current = TimeSpan.Zero;
            _isRunning = false;
        }

        public void Update(TimeSpan tick)
        {
            if(_isRunning == true && _current < _duration)
            {
                _current = _current + tick;
            }
            else if(_current >= _duration)
            {
                Stop();

                Ended?.Invoke();
            }
        }
    }
}
