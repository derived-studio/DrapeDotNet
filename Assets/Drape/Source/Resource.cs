using System.Collections.Generic;
using Drape.Interfaces;

namespace Drape
{
    public class Resource : Multistat, IStat, IUpdatable
    {
        private float _value;
        public Capacity capacity { get; private set; }
        public Output output { get; private set; }

        public Resource(string name, float value, Capacity capacity, Output output)
            : base(name, new HashSet<Stat>() { capacity, output })
        {
            _value = value;
            this.capacity = capacity;
            this.output = output;
        }

        public override float Value { get { return _value; } }

        public void Update(float deltaTime)
        {
            float produced = output.Value * deltaTime;
            float newValue = _value + produced;
            _value = newValue > capacity.Value ? capacity.Value : newValue;
        }

        public void Dispose(float value = -1) 
        {
            _value = value < 0 ? 0 : _value - value;
            if (_value < 0) {
                _value = 0;
            }
        }
        
        public class Capacity : Stat
        {
            public Capacity(string name, int baseValue) : base(name, baseValue) { }
        }

        public class Output : Stat
        {
            public Output(string name, int baseValue) : base(name, baseValue) { }
        }


    }
}