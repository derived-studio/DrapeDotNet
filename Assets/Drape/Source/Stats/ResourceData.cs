namespace Drape
{
    [System.Serializable]
    public class ResourceData : MultistatData
    {
        public string capacity;
        public string output;

        public ResourceData(string code, string name, int value, string capacity, string output)
            : base(code, name, value, new string[] { capacity, output })
        {
            this.capacity = capacity;
            this.output = output;
        }
    }
}