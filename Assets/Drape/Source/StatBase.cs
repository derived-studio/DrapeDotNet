using Drape.Interfaces;
using Drape.TinyJson;

namespace Drape
{
    public class StatBase: IStat
    {

        private StatData _data;

        public string Code { get { return _data.code; } }
        public string Name { get { return _data.name; } }
        public int BaseValue { get { return _data.value; } }

        public virtual float Value { get { throw new System.NotImplementedException(); } }

        public static StatBase FromJSON(string json) {
            StatData data = json.FromJson<StatData>();
            return new StatBase(data);
        }

        public virtual string ToJSON() { return _data.ToJson(); }

        public StatBase(StatData data) : this(data.code, data.name, data.value) { }
        
        public StatBase(string code, string name, int value = 0, string info = "")
        {
            _data = new StatData(code, name, value);
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