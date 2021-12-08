using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElevatorSim
{
    class RandomCaller
    {
        private long _millisConstant;
        private long _millisMaxVariance;
        private long _randomInterval;
        private long _millisecondTimer;
        private long _milliseconds;
        private Random _randObj;
        private ElevatorLogic _logic;
        private bool _isAvailable;
        private bool _run;

        public delegate void RandomCallerEventHandler(object sender, EventArgs e);
        public event RandomCallerEventHandler RandomCallEvent;

        public bool IsRunning
        {
            get => _run;
        }

        public long CurrentRandomInterval
        {
            get => _randomInterval;
        }

        public long MillisConstant
        {
            get => _millisConstant;
            set
            {
                if (!_run)
                {
                    _millisConstant = value;
                }
            }
        }

        public long MillisMaxVariance
        {
            get => _millisMaxVariance;
            set
            {
                if (!_run)
                {
                    _millisMaxVariance = value;
                }
            }
        }

        public RandomCaller(ElevatorLogic logic, long constTimeMilliseconds, long maxVarianceMilliseconds)
        {
            _randObj = new Random();
            _logic = logic;
            _run = false;
            _isAvailable = true;
            _randomInterval = 0;
            _millisecondTimer = 0;
            _milliseconds = 0;
            _millisConstant = constTimeMilliseconds;
            _millisMaxVariance = maxVarianceMilliseconds;
        }

        public void RunRandomCaller()
        {
            _run = true;
        }

        public void PauseRandomCaller()
        {
            _run = false;
        }

        public void CloseRandomCaller()
        {
            _isAvailable = false;
        }

        public void CallRandomly()
        {
            while (_isAvailable)
            {
                while (_run)
                {
                    if (RandomCallEvent != null)
                    {
                        _milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                        if (_milliseconds - _millisecondTimer > _randomInterval)
                        {
                            RandomCallEvent(this, EventArgs.Empty);
                            _millisecondTimer = _milliseconds;
                            _randomInterval = _millisConstant + (_randObj.Next() % _millisMaxVariance);
                        }
                    }

                    if (!_isAvailable)
                    {
                        break;
                    }
                }
            }
        }
    }
}
