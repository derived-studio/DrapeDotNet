using Drape.Slug;

namespace Drape
{
    [System.Serializable]
    public class ResourceData : BaseStatData
    {
        public string capacity;
        public string output;

        public ResourceData(string name, string capacity, string output)
            : this(name, 0, capacity, output) { }

        public ResourceData(string name, int value, string capacity, string output)
            : this(name.ToSlug(), name, value, capacity, output) { }

        public ResourceData(string code, string name, int value, string capacity, string output)
            : base(code, name, value)
        {
            this.capacity = capacity;
            this.output = output;
        }
    }
}