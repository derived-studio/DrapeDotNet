using System.Collections.Generic;
using Drape.Interfaces;

namespace Drape
{
    public class Resource : Multistat, IStat, IUpdatable
    {
        private float _value;
        public IStat capacity { get; private set; }
        public IStat output { get; private set; }

        public Resource(string name, float value, IStat capacity, IStat output)
            : base(name, new HashSet<IStat>() { capacity, output })
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
    }
}