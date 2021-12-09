using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSim
{
    class BackProcess
    {
        private long _simMillisInterval;
        private long _millisConstant;
        private long _millisMaxVariance;
        private long _randomInterval;
        private long _millisecondRandTimer;
        private long _millisecondTimer;
        private long _milliseconds;
        private Random _randObj;
        private bool _isAvailable;
        private bool _runRand;
        private bool _runProc;

        public delegate void BackProcessEventHandler(object sender, EventArgs e);
        public event BackProcessEventHandler RandomCallEvent;
        public event BackProcessEventHandler ProcessEvent;

        public bool IsRandRunning { get => _runRand; }
        public bool IsProcRunning { get => _runProc; }
        public long CurrentRandomInterval { get => _randomInterval; }
        public long MillisConstant
        {
            get => _millisConstant;
            set
            {
                if (!_runRand)
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
                if (!_runRand)
                {
                    _millisMaxVariance = value;
                }
            }
        }

        public BackProcess(long constSimMilliseconds, long constTimeMilliseconds, long maxVarianceMilliseconds)
        {
            _randObj = new Random();
            _runRand = false;
            _runProc = true;
            _isAvailable = true;
            _randomInterval = 0;
            _millisecondTimer = 0;
            _milliseconds = 0;
            _millisecondRandTimer = 0;
            _millisConstant = constTimeMilliseconds;
            _millisMaxVariance = maxVarianceMilliseconds;
            _simMillisInterval = constSimMilliseconds;
        }

        public void RunRandomCaller()
        {
            _runRand = true;
        }

        public void PauseRandomCaller()
        {
            _runRand = false;
        }

        public void RunProc()
        {
            _runProc = true;
        }

        public void PauseProc()
        {
            _runProc = false;
        }

        public void CloseBackProcess()
        {
            _runProc = false;
            _runRand = false;
            _isAvailable = false;
        }

        public void CallProc()
        {
            while (_isAvailable)
            {
                _milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

                if (_runRand && (RandomCallEvent != null))
                {
                    if (_milliseconds - _millisecondRandTimer > _randomInterval)
                    {
                        RandomCallEvent(this, EventArgs.Empty);
                        _millisecondRandTimer = _milliseconds;
                        _randomInterval = _millisConstant + (_randObj.Next() % _millisMaxVariance);
                    }
                }

                if (_runProc && (ProcessEvent != null))
                {
                    if (_milliseconds - _millisecondTimer > _simMillisInterval)
                    {
                        ProcessEvent(this, EventArgs.Empty);
                        _millisecondTimer = _milliseconds;
                    }
                }
            }
        }
    }
}