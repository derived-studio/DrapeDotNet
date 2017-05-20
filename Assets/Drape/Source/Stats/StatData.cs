namespace Drape
{
    public class StatData : BaseStatData
    {
        [System.Serializable]
        public class Dependency
        {
            public string code;
            public float value;
        }

        public Dependency[] dependencies;
        public StatData(string code, string name, int value, Dependency[] dependencies = null) : base(code, name, value)
        {
            this.dependencies = dependencies;
        }
    }
}