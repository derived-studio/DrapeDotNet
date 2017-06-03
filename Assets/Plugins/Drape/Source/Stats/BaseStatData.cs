namespace Drape
{
    /// <summary>
    /// Plane data class facillitating stat serialization
    /// </summary>
    public class BaseStatData
    {
        public string code;
        public string name;
        public int value;

        public BaseStatData(string code, string name, int value)
        {
            this.code = code;
            this.name = name;
            this.value = value;
        }
    }
}