using System.Collections.Generic;
using Drape.Interfaces;
using Drape.Slug;

namespace Drape
{
    public class Resource : Multistat, IStat, IUpdatable
    {
        private float _value;
        public IStat Capacity { get; private set; }
        public IStat Output { get; private set; }

        public Resource(string name, float value, IStat capacity, IStat output)
            : this(name.ToSlug(), name, value, capacity, output) { }

        public Resource(string code, string name, float value, IStat capacity, IStat output)
            : base(code, name, new HashSet<IStat>() { capacity, output })
        {
            _value = value;
            this.Capacity = capacity;
            this.Output = output;
        }

        public override float Value { get { return _value; } }

        public void Update(float deltaTime)
        {
            float produced = Output.Value * deltaTime;
            float newValue = _value + produced;
            _value = newValue > Capacity.Value ? Capacity.Value : newValue;
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