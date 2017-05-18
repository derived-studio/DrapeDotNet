using Drape.Interfaces;

namespace Drape
{
    public class StatBase: IStat
    {

        private StatData _data;

        public string Code { get { return _data.code; } }
        public string Name { get { return _data.name; } }
        public int BaseValue { get { return _data.value; } }

        public virtual float Value { get { throw new System.NotImplementedException(); } }

        // StatData exposed for serializtion
        public virtual StatData Data { get { return _data; } }

        public StatBase(string code, string name, int value = 0, string info = "")
        {
            _data = new StatData(code, name, value);
            _data.code = code;
            _data.name = name;
            _data.value = value;
        }

        public struct StatData
        {
            public string code;
            public string name;
            public int value;

            public StatData(string code, string name, int value)
            {
                this.code = code;
                this.name = name;
                this.value = value;
            }
        }
    }
}