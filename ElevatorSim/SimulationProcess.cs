using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSim
{
    class SimulationProcess
    {
        private bool _run;
        private long _millisInterval;
        private long _millisecondTimer;
        private long _milliseconds;

        public delegate void SimulationProcessEventHandler(object sender, EventArgs e);
        public event SimulationProcessEventHandler ProcessEvent;

        public SimulationProcess(long millisecondInterval)
        {
            _run = true;
            _millisInterval = millisecondInterval;
            _milliseconds = 0;
            _millisecondTimer = 0;
        }

        public void Close()
        {
            _run = false;
        }

        public void Run()
        {
            while (_run){
                if (ProcessEvent != null)
                {
                    _milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                    if (_milliseconds - _millisecondTimer > _millisInterval)
                    {
                        ProcessEvent(this, EventArgs.Empty);
                        _millisecondTimer = _milliseconds;
                    }
                }
            }
        }

        public void RunOnce()
        {
            if (ProcessEvent != null)
            {
                _milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                if (_milliseconds - _millisecondTimer > _millisInterval)
                {
                    ProcessEvent(this, EventArgs.Empty);
                    _millisecondTimer = _milliseconds;
                }
            }
        }
    }
}
