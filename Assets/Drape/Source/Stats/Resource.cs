using System.Collections.Generic;
using Drape.Interfaces;
using Drape.Slug;

namespace Drape
{
    public class Resource : Multistat, IStat, IUpdatable
    {
        private float _qty;
        public IStat Capacity { get; private set; }
        public IStat Output { get; private set; }

        public Resource(ResourceData data, IRegistry registry) : base(data, registry)
        {
            _qty = data.value;
            this.Capacity = registry.Get<IStat>(data.capacity);
            this.Output = registry.Get<IStat>(data.output);
        }

        public override float Value { get { return _qty; } }

        public void Update(float deltaTime)
        {
            float produced = Output.Value * deltaTime;
            float newValue = _qty + produced;
            _qty = newValue > Capacity.Value ? Capacity.Value : newValue;
        }

        public void Dispose(float value = -1) 
        {
            _qty = value < 0 ? 0 : _qty - value;
            if (_qty < 0) {
                _qty = 0;
            }
        }
    }
}