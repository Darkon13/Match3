using System;
using System.Collections.Generic;

namespace Match3.Core.Utils
{
    public class TimerController
    {
        private List<Timer> _timers;

        public TimerController()
        {
            _timers = new List<Timer>();
        }

        public Timer CreateTimer()
        {
            Timer timer = new Timer();

            _timers.Add(timer);
            return timer;
        }

        public void Update(TimeSpan tick)
        {
            foreach (Timer timer in _timers)
            {
                timer.Update(tick);
            }
        }
    }
}
