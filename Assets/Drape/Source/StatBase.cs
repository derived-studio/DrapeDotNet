using Drape.Interfaces;

namespace Drape
{
    public class StatBase: IStat
    {
        private string _name;
        private int _baseValue;

        public string Name { get { return _name; } }

        public virtual float Value { get { throw new System.NotImplementedException(); } }

        public int BaseValue { get { return _baseValue; } }

        public StatBase(string name, int baseValue = 0, string info = "")
        {
            _name = name;
            _baseValue = baseValue;
        }
    }
}