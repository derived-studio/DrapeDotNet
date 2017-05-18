using Drape.Interfaces;

namespace Drape
{
    public class StatBase: IStat
    {
        private string _code;
        private string _name;
        private int _baseValue;

        public string Code { get { return _code; } }
        public string Name { get { return _name; } }
        public int BaseValue { get { return _baseValue; } }

        public virtual float Value { get { throw new System.NotImplementedException(); } }

        public StatBase(string code, string name, int baseValue = 0, string info = "")
        {
            _code = code;
            _name = name;
            _baseValue = baseValue;
        }
    }
}