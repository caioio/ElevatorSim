using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSim
{
    /*
     * |   |========
     * |   |        \ > floor height
     * |   |        /
     * |   |========] > slab height
     * |---|
     * |___|
     * |   |========
     **/
    class ElevatorLogic
    {
        private uint _floorsNumber;       // número de andares
        private double _floorHeight;      // altura do andar
        private double _slabHeight;       // altura da lage
        private double _velocity;         // velocidade do elevador
        private double _position;         // posição (altura) do elevador
        private bool _isMoving;           // estado do elevador (movendo ou parado)
        private bool _isGoingUp;          // estado do elevador (subindo ou descendo)
        private uint _closerFloor;        // andar mais perto
        private uint _floorRequested;     // andar requerido
        private bool[] _pannelRequests;   // lista de chamadas do painel do elevador (PRIORIDADE)
        private bool[] _floorRequests;    // lista de chamadas de cada andar
        private long _timer;

        /*
         * O elevador prioriza as chamadas do painel, e sempre irá atender primeiro
         * o andar mais próximo pedido pelo painel interno do elevador, caso
         * não haja nenhum botão pressionado no painel interno então ele atenderá
         * as chamadas de cada andar, o elevador entende que todos que estão nos
         * andares acima do térreo querem descer, então por isso caso alguém esteja
         * descendo de um andar maior o elevador irá atender as chamadas de andares menores.
         * */

        /*
         * [ ] adicionar erros de variáveis
         * [ ] parada de emergência
         * [ ] velocidade máxima
         * [ ] sistemas de portas dos andares e do elevador
         * ==================================================================
         * [ ] modo incêndio (move elevador para o térreo e desliga elevador)
         * [ ] perfil de aceleração e desaceleração
         * [ ] motor do elevador
         */

        public uint FloorsNumber
        {
            get => _floorsNumber;
            set
            {
                if(!Object.Equals(_floorsNumber, default(int)))
                {
                    _floorsNumber = value;
                }
            }
        }

        public double FloorHeight
        {
            get => _floorHeight;
            set
            {
                if (!Object.Equals(_floorHeight, default(double)))
                {
                    _floorHeight = value;
                }
            }
        }

        public double SlabHeight
        {
            get => _slabHeight;
            set
            {
                if(!Object.Equals(_slabHeight, default(double)))
                {
                    _slabHeight = value;
                }
            }
        }

        public bool IsMoving
        {
            get => _isMoving;
        }

        public uint CloserFloor
        {
            get => _closerFloor;
        }

        public uint FloorRequested
        {
            get => _floorRequested;
        }

        public double Position
        {
            get => _position;
        }

        public long GetTimeInMilliseconds()         // mudar
        {   
            return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

        /* Função construtora*/
        public ElevatorLogic(uint floorsNumber, double floorHeight, double slabHeight)
        {
            _floorsNumber = floorsNumber;
            _floorHeight = floorHeight;
            _slabHeight = slabHeight;
            _closerFloor = 0;                            // O elevador inicia no Térreo
            _position = 0.0d;
            _velocity = 0.0d;
            _isGoingUp = true;
            _isMoving = false;
            _floorRequests = new bool[floorsNumber];     // inicializa array de chamadas dos pisos
            _pannelRequests = new bool[floorsNumber];    // inicializa array de chamadas do painel

            for(int i = 0; i < floorsNumber; i++)
            {
                _floorRequests[i] = false;
                _pannelRequests[i] = false;
            }
        }

        public void AddPannelRequest(uint floor)
        {
            if(floor <= _floorsNumber)
            {
                _pannelRequests[floor] = true;
            }
        }

        public void AddFloorRequest(uint floor)
        {
            if(floor <= _floorsNumber)
            {
                _floorRequests[floor] = true;
            }
        }

        public bool HasPannelRequest()
        {
            bool ans = false;
            foreach(bool request in _pannelRequests)
            {
                ans = ans || request;
            }
            return ans;
        }

        public bool HasFloorRequest()
        {
            bool ans = false;
            foreach(bool request in _floorRequests)
            {
                ans = ans || request;
            }
            return ans;
        }

        public void RunElevatorLogic(long millisecondsTickTime)
        {

            if((GetTimeInMilliseconds() - _timer) < millisecondsTickTime)
            {
                return;
            }
            else
            {
                _timer = GetTimeInMilliseconds();
            }

            if (_isMoving)
            {
                if ((_position < (_floorRequested * (_floorHeight + _slabHeight)+0.01d)) && (_position > (_floorRequested * (_floorHeight + _slabHeight) - 0.01d)))
                {
                    _position = _floorRequested * (_floorHeight + _slabHeight);
                    _closerFloor = _floorRequested;
                    _isMoving = false;
                    _velocity = 0;

                    if (_floorRequests[_floorRequested])
                    {
                        _floorRequests[_floorRequested] = false;
                    }

                    if (_pannelRequests[_floorRequested])
                    {
                        _pannelRequests[_floorRequested] = false;
                    }
                }
                else
                {
                    if (_isGoingUp)
                    {
                        _velocity = _velocity + 0.1 * (4 - _velocity);
                    }
                    else
                    {
                        _velocity = _velocity - 0.1 * (4 + _velocity);
                    }

                    _position += _velocity;

                    _closerFloor = (uint)(_position / (_floorHeight + _slabHeight));
                }
            }
            else
            {
                if (_isGoingUp)
                {
                    for(uint i = _closerFloor; i < _floorsNumber; i++)
                    {
                        if (_pannelRequests[i])
                        {
                            _floorRequested = i;
                            _isMoving = true;
                            goto XX;
                        }
                    }
                    _isGoingUp = false;
                    XX:;
                }
                
                if (!_isGoingUp)
                {
                    for(int i = (int)_closerFloor; i >= 0; i--)
                    {
                        if (_pannelRequests[i])
                        {
                            _floorRequested = (uint)i;
                            _isMoving = true;
                            goto KK;
                        }
                    }
                    _isGoingUp = true;
                    KK:;
                }

                if (!_isMoving)
                {
                    for(int i = (int)(_floorsNumber - 1); i >= 0; i--)
                    {
                        if (_floorRequests[i])
                        {
                            _floorRequested = (uint)i;
                            _isMoving = true;
                            break;
                        }
                    }

                    if(_floorRequested > _closerFloor)
                    {
                        _isGoingUp = true;
                    }
                    else
                    {
                        _isGoingUp = false;
                    }
                }
            }
        }



    }
}
